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
    public class ClipSharing
    {
        public Timer CopyPollingTimer;

        public string ClipDataFolderPath = String.Empty;
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

        public ClipSharing()
        {
            Directory.CreateDirectory(ClipDataFolderPath);
            // clear folder and children files
            FileControler.ClearDataDirectory(ClipDataFolderPath);

            CopyPollingTimer = new Timer();
            CopyPollingTimer.Interval = 2000;
            CopyPollingTimer.Tick += new EventHandler(PollingTimer_Tick);
            CopyPollingTimer.Start();

            LastLoadTime = DateTime.Now;
        }

        private void PollingTimer_Tick(object sender, EventArgs e)
        {
            // Saveファイルの有無チェック
            var inputFileList = Directory.EnumerateFileSystemEntries(ClipDataFolderPath).ToArray();
            if (inputFileList.Length <= 0) return;

            var saveFileCreateTime = inputFileList.Max(f => File.GetLastWriteTime(f));
            if (DateTime.Compare(LastLoadTime, saveFileCreateTime) >= 0) return;

            Clipboard.Clear();
            foreach (string[] targetFormats in ChoiceFormatPriorities.Where(f => inputFileList.Select(fl => Path.GetFileName(fl)).Contains(f[0])))
            {
                string dataFormat = targetFormats[0];
                string dataType = targetFormats[1];
                string outputFilePath = Path.Combine(ClipDataFolderPath, dataFormat);

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
            LastLoadTime = DateTime.Now;
        }

        public void SaveClipBoard()
        {
            //
            FileControler.ClearDataDirectory(ClipDataFolderPath);

            // Saveファイルの有無チェック
            var inputFileList = Directory.EnumerateFileSystemEntries(ClipDataFolderPath).ToArray();
            if (inputFileList.Length <= 0) return;

            var saveFileCreateTime = inputFileList.Max(f => File.GetLastWriteTime(f));
            if (DateTime.Compare(LastLoadTime, saveFileCreateTime) >= 0) return;

            Clipboard.Clear();
            foreach (string[] targetFormats in ChoiceFormatPriorities.Where(f => inputFileList.Select(fl => Path.GetFileName(fl)).Contains(f[0])))
            {
                string dataFormat = targetFormats[0];
                string dataType = targetFormats[1];
                string outputFilePath = Path.Combine(ClipDataFolderPath, dataFormat);

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
            LastLoadTime = DateTime.Now;
        }
    }
}
