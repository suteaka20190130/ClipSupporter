using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;

namespace FunctionLibraryBP
{
    public class ClipSharing : BaseFunctionClass
    {
        public Timer CopyPollingTimer;

        const string DATATYPE_TEXT = "text";
        const string DATATYPE_BIN = "bin";
        const string DATATYPE_BMP = "bmp";
        const string DATATYPE_FILE = "file";
        const string DATATYPE_STR_ARRAY = "string[]";

        public DateTime LastLoadTime { get; set; }

        readonly string[][] ChoiceFormatPriorities = {
            new string[]{"mmatsubara_A5M2_GRIDDATA", DATATYPE_BIN },
             new string[]{DataFormats.StringFormat, DATATYPE_TEXT },
            new string[]{DataFormats.UnicodeText, DATATYPE_TEXT },
            new string[]{DataFormats.Text, DATATYPE_TEXT },
            new string[]{"SAKURAClipW", DATATYPE_BIN },
            new string[]{DataFormats.Locale, DATATYPE_BIN },
            new string[]{DataFormats.OemText, DATATYPE_TEXT },

            //new string[]{ "Shell IDList Array", DATATYPE_BIN },
            //new string[]{ "DataObjectAttributes", DATATYPE_BIN },
            //new string[]{ "DataObjectAttributesRequiringElevation", DATATYPE_BIN },
            //new string[]{ "Shell Object Offsets", DATATYPE_BIN },
            //new string[]{ "Preferred DropEffect", DATATYPE_BIN },
            //new string[]{ "AsyncFlag", DATATYPE_BIN },

            new string[]{DataFormats.FileDrop, DATATYPE_FILE },
            //new string[]{"FileNameW", DATATYPE_STR_ARRAY },
            //new string[]{"FileName", DATATYPE_STR_ARRAY },

            new string[]{DataFormats.EnhancedMetafile, DATATYPE_TEXT },
            new string[]{DataFormats.Html, DATATYPE_TEXT },
            new string[]{DataFormats.Bitmap, DATATYPE_BMP },
            new string[]{DataFormats.CommaSeparatedValue, DATATYPE_TEXT },
        };
        enum DataSaveType
        {
            Text,
            File,
            Binary,
        }

        public ClipSharing(string panelName) : base(panelName)
        {
            Directory.CreateDirectory(BasePath);
            // clear folder and children files
            FileControler.ClearDataDirectory(BasePath);

            CopyPollingTimer = new Timer();
            CopyPollingTimer.Interval = 10000;
            CopyPollingTimer.Tick += new EventHandler(PollingTimer_Tick);
            CopyPollingTimer.Start();

            LastLoadTime = DateTime.Now;
        }

        public void PollingTimer_Tick(object sender, EventArgs e)
        {
            // Saveファイルの有無チェック
            var inputFileList = Directory.EnumerateFileSystemEntries(BasePath).ToArray();
            if (inputFileList.Length <= 0) return;

            // 99_ENDファイルが無い場合はSave中
            if (!inputFileList.Any(f => Path.GetFileName(f) == "99_END")) return;

            var saveFileCreateTime = inputFileList.Max(f => File.GetLastWriteTime(f));
            if (DateTime.Compare(LastLoadTime, saveFileCreateTime) >= 0) return;

            // 00_START, 99_ENDファイルは対象外

            Clipboard.Clear();
            foreach (string[] targetFormats in ChoiceFormatPriorities.Where(f => inputFileList.Select(fl => Path.GetFileName(fl)).Contains(f[0])))
            {
                string dataFormat = targetFormats[0];
                string dataType = targetFormats[1];
                string outputFilePath = Path.Combine(BasePath, dataFormat);

                switch (dataType)
                {
                    case DATATYPE_FILE:
                        Clipboard.SetData(DataFormats.FileDrop, Directory.EnumerateFileSystemEntries(outputFilePath).ToArray());
                        break;
                    case DATATYPE_BIN:
                        byte[] data = File.ReadAllBytes(outputFilePath);
                        Clipboard.SetData(dataFormat, data);
                        break;
                    case DATATYPE_BMP:
                        using (var ms = new MemoryStream(File.ReadAllBytes(outputFilePath)))
                        {
                            Bitmap bmp = new Bitmap(ms);
                            Clipboard.SetData(dataFormat, bmp);
                        }
                        break;
                    case DATATYPE_STR_ARRAY:
                        using (var sr = new StreamReader(outputFilePath))
                        {
                            Clipboard.SetData(dataFormat, sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.None));
                        }
                        break;
                    case DATATYPE_TEXT:
                        using (var sr = new StreamReader(outputFilePath))
                        {
                            Clipboard.SetData(dataFormat, sr.ReadToEnd());
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

            // 現在Save中の場合は中断
            var inputFileList = Directory.EnumerateFileSystemEntries(BasePath).ToArray();
            if (inputFileList.Any(f => Path.GetFileName(f) == "00_START")
             && !inputFileList.Any(f => Path.GetFileName(f) == "99_END")) return;

            // clear folder amd children files
            FileControler.ClearDataDirectory(BasePath);

            File.Create(Path.Combine(BasePath, "00_START")).Close();

            string[] formats = dataObj.GetFormats();
            foreach (string[] targetFormats in ChoiceFormatPriorities.Where(f => formats.Contains(f[0])))
            {
                string dataFormat = targetFormats[0];
                string dataType = targetFormats[1];
                string outputFilePath = Path.Combine(BasePath, dataFormat);

                // Formatに合わせて保存
                switch (dataType)
                {
                    case DATATYPE_BMP:
                        var img = Clipboard.GetImage();
                        img.Save(outputFilePath);
                        break;
                    case DATATYPE_BIN:
                        var ms = (MemoryStream)dataObj.GetData(dataFormat);
                        File.WriteAllBytes(outputFilePath, ms.ToArray());
                        break;
                    case DATATYPE_FILE:
                        Directory.CreateDirectory(outputFilePath);
                        foreach (var path in (string[])dataObj.GetData(dataFormat))
                        {
                            FileControler.CopyAndReplace(path, outputFilePath);
                        }
                        break;
                    case DATATYPE_STR_ARRAY:
                        using (var sw = new StreamWriter(outputFilePath))
                        {
                            sw.Write(String.Join("\r\n", (string[])dataObj.GetData(dataFormat)));
                        }
                        break;
                    case DATATYPE_TEXT:
                        using (var sw = new StreamWriter(outputFilePath))
                        {
                            sw.Write(dataObj.GetData(dataFormat));
                        }
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
