using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipSupporter
{
    public partial class ClipSupporterForm : Form
    {
        public string ClipDataFolderPath = String.Empty;
        const string DATATYPE_TEXT = "text";
        const string DATATYPE_BIN = "bin";
        const string DATATYPE_FILE = "file";
        readonly string[][] ChoiceFormatPriorities = {
            new string[]{DataFormats.FileDrop, DATATYPE_FILE },
            new string[]{DataFormats.EnhancedMetafile, DATATYPE_TEXT },
            new string[]{DataFormats.OemText, DATATYPE_TEXT },
            new string[]{DataFormats.Html, DATATYPE_TEXT },
            new string[]{DataFormats.Bitmap, DATATYPE_BIN },
            new string[]{DataFormats.UnicodeText, DATATYPE_TEXT },
            new string[]{DataFormats.CommaSeparatedValue, DATATYPE_TEXT },
            new string[]{DataFormats.Text, DATATYPE_TEXT },
        };
        enum DataSaveType
        {
            Text,
            File,
            Binary,
        }

        public ClipSupporterForm()
        {
            InitializeComponent();

            // 初期設定の取り込み
            ClipDataFolderPath = Path.Combine(ConfigurationManager.AppSettings["BasePath"]
                , ConfigurationManager.AppSettings["AppName"]);

            Directory.CreateDirectory(ClipDataFolderPath);
            // clear folder and children files
            ClearDataDirectory();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var dataObj = Clipboard.GetDataObject();
            if (dataObj == null) return;

            string dataType = string.Empty;
            string dataFormat = ChoiceFormat(dataObj.GetFormats(), ref dataType);
            if (dataFormat == string.Empty) return;

            // clear folder amd children files
            ClearDataDirectory();

            //MessageBox.Show(dataFormat + "\r\n" + dataObj.GetData(dataFormat));
            Console.WriteLine(dataFormat + "\r\n" + dataObj.GetData(dataFormat));
            string outputFilePath = Path.Combine(ClipDataFolderPath, dataFormat);

            this.Text = "Save中...";
            // Formatに合わせて保存
            switch (dataType)
            {
                case DATATYPE_BIN:
                    var img = Clipboard.GetImage();
                    img.Save(outputFilePath);
                    break;
                case DATATYPE_FILE:
                    if (!CopyDropFile((string[])dataObj.GetData(dataFormat)))
                    {
                        notifyIcon1.ShowBalloonTip(1000, "エラー", $"ファイルコピーに失敗しました。", ToolTipIcon.Info);
                        ClearDataDirectory();
                    }
                    break;
                case DATATYPE_TEXT:
                    using (var sw = new StreamWriter(outputFilePath))
                    {
                        sw.Write(dataObj.GetData(dataFormat));
                    }
                    break;
                default:
                    notifyIcon1.ShowBalloonTip(1000, "情報", $"コピーしているデータには対応しておりません。{dataFormat}", ToolTipIcon.Info);
                    return;
            }
            this.Text = "ClipSupporter";

            // 動作確認の為、クリア
            // Clipboard.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var inputFileList = Directory.EnumerateFileSystemEntries(ClipDataFolderPath).ToArray();
            string dataFormat = string.Empty;
            string dataType = string.Empty;
            if (inputFileList.Length > 1 
                || (dataFormat = ChoiceFormat(inputFileList.Select(f => Path.GetFileName(f)).ToArray()
                                             , ref dataType)) == string.Empty)
            {
                // Fileコピーの場合
                Clipboard.SetData(DataFormats.FileDrop, inputFileList);
            }
            else
            {
                switch (dataType)
                {
                    case DATATYPE_BIN:
                        using (var ms = new MemoryStream(File.ReadAllBytes(inputFileList[0])))
                        {
                            Bitmap bmp = new Bitmap(ms);
                            Clipboard.SetData(dataFormat, bmp);
                        }
                        break;
                    case DATATYPE_TEXT:
                        using (var sr = new StreamReader(inputFileList[0]))
                        {
                            Clipboard.SetData(dataFormat, sr.ReadToEnd());
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        private string ChoiceFormat(string[] formats, ref string dataType)
        {
            foreach (var fmt in formats)
            {
                if (ChoiceFormatPriorities.Any(f => f[0] == fmt))
                {
                    var hitFormat = ChoiceFormatPriorities.Where(f => f[0] == fmt).First();
                    dataType = hitFormat[1];
                    return hitFormat[0];
                }
            }

            return string.Empty;
        }

        private void ClearDataDirectory()
        {
            foreach (var item in Directory.EnumerateFiles(ClipDataFolderPath))
            {
                File.Delete(item);
            }

            foreach (var dir in Directory.EnumerateDirectories(ClipDataFolderPath))
            {
                Directory.Delete(dir, true);
            }
        }

        private bool CopyDropFile(string[] dropFileArray)
        {
            foreach (var path in dropFileArray)
            {
                //ファイルをコピー
                if (File.Exists(path))
                {
                    File.Copy(path, Path.Combine(ClipDataFolderPath, Path.GetFileName(path)));
                }
                else
                {
                    CopyAndReplace(path, Path.Combine(ClipDataFolderPath, Path.GetFileName(path)));
                }
            }

            if (Directory.EnumerateFileSystemEntries(ClipDataFolderPath).Count()
                == dropFileArray.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// ディレクトリとその中身を上書きコピー
        /// </summary>
        public void CopyAndReplace(string sourcePath, string copyPath)
        {
            Directory.CreateDirectory(copyPath);

            //ファイルをコピー
            foreach (var file in Directory.GetFiles(sourcePath))
            {
                File.Copy(file, Path.Combine(copyPath, Path.GetFileName(file)));
            }

            //ディレクトリの中のディレクトリも再帰的にコピー
            foreach (var dir in Directory.GetDirectories(sourcePath))
            {
                CopyAndReplace(dir, Path.Combine(copyPath, Path.GetFileName(dir)));
            }
        }
    }
}
