﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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

        public ClipSupporterForm()
        {
            InitializeComponent();

            // 初期設定の取り込み
            ClipDataFolderPath = Path.Combine(ConfigurationManager.AppSettings["BasePath"]
                , ConfigurationManager.AppSettings["AppName"]);

            // フォームデザインの設定
            switch (ConfigurationManager.AppSettings["FormDesignPattern"])
            {
                case "system":
                    this.BackColor = Color.Gray;
                    break;
                case "blue":
                    this.BackColor = Color.Blue;
                    break;
                case "yellow":
                    this.BackColor = Color.Yellow;
                    break;
                case "green":
                    this.BackColor = Color.Green;
                    break;
                default:
                    MessageBox.Show($"起動に失敗しました。FormDesignPattern={ConfigurationManager.AppSettings["FormDesignPattern"]}");
                    break;
            }

            Directory.CreateDirectory(ClipDataFolderPath);
            // clear folder and children files
            ClearDataDirectory();

            LastLoadTime = DateTime.Now;

            timer1.Enabled = true;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var dataObj = Clipboard.GetDataObject();
            if (dataObj == null || dataObj.GetFormats().Length <= 0) return;

            // clear folder amd children files
            ClearDataDirectory();

            this.Text = "Save中...";
            string[] formats = dataObj.GetFormats();
            foreach (var f in formats)
            {
                Debug.WriteLine(f+"\t\t\t\t\t"+dataObj.GetData(f));
            }
            foreach (string[] targetFormats in ChoiceFormatPriorities.Where(f => formats.Contains(f[0])))
            {
                string dataFormat = targetFormats[0];
                string dataType = targetFormats[1];
                string outputFilePath = Path.Combine(ClipDataFolderPath, dataFormat);

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
                            CopyAndReplace(path, outputFilePath);
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
            this.Text = "ClipSupporter";

            // 動作確認の為、クリア
            Clipboard.Clear();

            LastLoadTime = DateTime.Now;

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

        /// <summary>
        /// ディレクトリとその中身を上書きコピー
        /// </summary>
        public void CopyAndReplace(string sourcePath, string copyPath)
        {
            // ファイルのコピー
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, Path.Combine(copyPath, Path.GetFileName(sourcePath)));
            }
            else
            {
                // ディレクトリ内エントリのコピー
                string targetPath = Path.Combine(copyPath, Path.GetFileName(sourcePath));
                Directory.CreateDirectory(targetPath);
                foreach (var path in Directory.EnumerateFileSystemEntries(sourcePath))
                {
                    CopyAndReplace(Path.Combine(sourcePath, path), targetPath);
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
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
    }
}
