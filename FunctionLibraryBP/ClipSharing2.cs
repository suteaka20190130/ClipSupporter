using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;

namespace FunctionLibraryBP
{
    public class ClipSharing2 : BaseFunctionClass
    {
        public Timer CopyPollingTimer;
        const string DATATYPE_TEXT = "String";
        const string DATATYPE_BIN = "MemoryStream";
        const string DATATYPE_BMP = "bmp";
        const string DATATYPE_FILE = "file";
        const string DATATYPE_STR_ARRAY = "stringA";

        public DateTime LastLoadTime { get; set; }

        public ClipSharing2(string panelName) : base(panelName)
        {
            Directory.CreateDirectory(BasePath);
            // clear folder and children files
            FileControler.ClearDataDirectory(BasePath);

            //CopyPollingTimer = new Timer();
            //CopyPollingTimer.Interval = 10000;
            //CopyPollingTimer.Tick += new EventHandler(PollingTimer_Tick);
            //CopyPollingTimer.Enabled = true;

            LastLoadTime = DateTime.Now;
        }

        public void PollingTimer_Tick(object sender, EventArgs e)
        {
            // Saveファイルの有無チェック
            var inputFileList = Directory.EnumerateFileSystemEntries(BasePath).ToArray();
            if (inputFileList.Length <= 0) return;

            // 99_ENDファイルが無い場合はSave中
            if (!inputFileList.Any(f => Path.GetFileName(f) == "99_END")) return;

            //var saveFileCreateTime = inputFileList.Max(f => File.GetLastWriteTime(f));
            //if (DateTime.Compare(LastLoadTime, saveFileCreateTime) >= 0) return;

            // 00_START, 99_ENDファイルは対象外

            Clipboard.Clear();
            for (int fNo = 0; fNo < inputFileList.Count(); fNo++)
            {
                string fileName = inputFileList[fNo];
                string outputFilePath = Path.Combine(BasePath, fileName);
                if (Directory.Exists(outputFilePath)) continue;
                if (fileName.EndsWith("START") || fileName.EndsWith("END")) continue;

                string format = Path.GetFileNameWithoutExtension(fileName).Substring(3);
                string extName = Path.GetExtension(fileName).Substring(1);

                switch (extName)
                {
                    case DATATYPE_FILE:
                        Clipboard.SetData(DataFormats.FileDrop, Directory.EnumerateFileSystemEntries(outputFilePath).ToArray());
                        break;
                    case DATATYPE_BIN:
                        byte[] data = File.ReadAllBytes(outputFilePath);
                        Clipboard.SetData(format, data);
                        break;

                    case DATATYPE_BMP:
                        using (var ms = new MemoryStream(File.ReadAllBytes(outputFilePath)))
                        {
                            Bitmap bmp = new Bitmap(ms);
                            Clipboard.SetData(format, bmp);
                        }
                        break;
                    case DATATYPE_STR_ARRAY:
                        using (var sr = new StreamReader(outputFilePath))
                        {
                            Clipboard.SetData(format, sr.ReadToEnd().Split(new string[] { "\r\n" }
                            , StringSplitOptions.None));
                        }
                        break;
                    case DATATYPE_TEXT:
                        using (var sr = new StreamReader(outputFilePath))
                        {
                            Clipboard.SetData(format, sr.ReadToEnd());
                        }
                        break;
                }
            }

            ShareCompornent.NotifyControl.ShowBalloonTip(5000, "ClipBord共有", "ClipBordのコピーが完了しました", ToolTipIcon.Info);
            LastLoadTime = DateTime.Now;
        }

        public void SaveClipBoard(object sender, EventArgs e)
        {
            var dataObj = Clipboard.GetDataObject();
            if (dataObj == null || dataObj.GetFormats().Length <= 0) return;

            // 現在Save中の場合は強制実行するか確認する
            var inputFileList = Directory.EnumerateFileSystemEntries(BasePath).ToArray();
            if (inputFileList.Any(f => Path.GetFileName(f) == "00_START")
             && !inputFileList.Any(f => Path.GetFileName(f) == "99_END"))
            {
                var dialogResult = MessageBox.Show("コピー中のファイルが存在します。\r\n強制的にセーブしますか？"
                                                 , "強制セーブ確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            // clear folder amd children files
            FileControler.ClearDataDirectory(BasePath);

            File.Create(Path.Combine(BasePath, "00_START")).Close();

            string[] formats = dataObj.GetFormats();
            for (int fNo = 0; fNo < formats.Length; fNo++)
            { 
                string dataFormat = formats[fNo];
                string outputFilePath = Path.Combine(BasePath, $"{fNo+1:D2}_{dataFormat}");
                var clipData = dataObj.GetData(dataFormat);
                if (clipData == null) continue;

                // Formatに合わせて保存
                switch (clipData.GetType().Name)
                {
                    case DATATYPE_BMP:
                        var img = Clipboard.GetImage();
                        img.Save($"{outputFilePath}.{DATATYPE_BMP}");
                        break;
                    case DATATYPE_BIN:
                        var ms = (MemoryStream)dataObj.GetData(dataFormat);
                        File.WriteAllBytes($"{outputFilePath}.{DATATYPE_BIN}", ms.ToArray());
                        break;
                    case "String[]":
                        string[] outputData = (string[])dataObj.GetData(dataFormat);
                        if (dataFormat == "FileDrop")
                        {
                            Directory.CreateDirectory(outputFilePath);
                            for (int i = 0; i < outputData.Length; i++)
                            {
                                FileControler.CopyAndReplace(outputData[i], outputFilePath);
                                outputData[i] = Path.Combine(outputFilePath, Path.GetFileName(outputData[i]));
                            }
                        }
                        using (var sw = new StreamWriter($"{outputFilePath}.{DATATYPE_STR_ARRAY}"))
                        {
                            sw.Write(String.Join("\r\n", outputData));
                        }
                        break;
                    case DATATYPE_TEXT:
                        using (var sw = new StreamWriter($"{outputFilePath}.{DATATYPE_TEXT}"))
                        {
                            sw.Write(dataObj.GetData(dataFormat));
                        }
                        break;
                    default:
                        Debug.WriteLine($"{fNo}__{dataFormat}__{clipData.GetType().Name}");
                        break;
                }
            }

            File.Create(Path.Combine(BasePath, "99_END")).Close();

            LastLoadTime = DateTime.Now;
        }

        public override void ShowDegugInfo(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
