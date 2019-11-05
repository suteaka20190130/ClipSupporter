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
    public class CreateInsertSQL
    {
        public CreateInsertSQL()
        {

        }
        /// <summary>
        /// Recordデータのトリム実施有無
        /// </summary>
        public bool IsTrim { get; set; }
        /// <summary>
        /// NULL割り当て文字
        /// </summary>
        public string NullString { get; set; }

        public void SaveClipBoard(object sender, EventArgs e)
        {
            #region 暫定設定処理
            IsTrim = true;
            NullString = "NULL";
            #endregion

            string inputData = Clipboard.GetText();

            // 表形式であるか確認(カンマ・タブ文字、改行コード)
            if (String.IsNullOrEmpty(inputData.Trim())) return;
            string separateLine = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None).Length > 1 ? "\r\n" : "\r";
            string separateCell = inputData.Split(new string[] { "\t" }, StringSplitOptions.None).Length > 1 ? "\t" : ",";

            if (inputData.Split(new string[] { separateLine }, StringSplitOptions.None).Length <= 1) return;
            if (inputData.Split(new string[] { separateCell }, StringSplitOptions.None).Length <= 1) return;
            List<List<String>> tableData = inputData.Split(new string[] { separateLine }, StringSplitOptions.None)
                                          .Select(l => l.Split(new string[] { separateCell }, StringSplitOptions.None).ToList()).ToList();
            // title & record無し
            if (tableData[0].Any(c => String.IsNullOrEmpty(c))) return;
            if (tableData.Count() <= 1) return;

            StringBuilder sbFormat = new StringBuilder();
            // header
            sbFormat.Append("INSERT INTO ");
            sbFormat.Append("てーぶるめい ");
            sbFormat.Append("(" + String.Join(",", tableData[0]) + ") ");
            sbFormat.Append("VALUES {0}");

            StringBuilder sbRecord = new StringBuilder();
            // record
            for (int y = 1; y < tableData.Count(); y++)
            {
                if (tableData[y].Count() <= 1) return;

                string recordData = string.Empty;
                for (int x = 0; x < tableData[y].Count(); x++)
                {
                    bool isNumber = false;
                    bool isNull = false;
                    string cellData = tableData[y][x];
                    if (cellData == NullString)
                    {
                        isNull = true;
                    }
                    else
                    {
                        if (IsTrim)
                        {
                            cellData = cellData.Trim();
                        }
                        if (cellData == tableData[y][x])
                        {
                            decimal tmpData = new decimal();
                            if (Decimal.TryParse(cellData, out tmpData))
                            {
                                if (cellData == tmpData.ToString())
                                {
                                    isNumber = true;
                                }
                            }
                        }
                    }
                    if (isNull)
                    {
                        recordData += ",Null";
                    }
                    else if (isNumber)
                    {
                        recordData += "," + cellData;
                    }
                    else
                    {
                        recordData += $",'{cellData}'";
                    }
                }
                sbRecord.AppendLine(String.Format(sbFormat.ToString(), "(" + recordData.Substring(1) + ");"));
            }

            Clipboard.Clear();
            Clipboard.SetText(sbRecord.ToString());
        }
    }
}
