using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OpenAuth.Mvc.Utils
{
    public class ExportWord
    {
        public XWPFDocument Doc { get; set; }

        public ExportWord(string FilePath)
        {
            using (FileStream file = File.Open(FilePath, FileMode.Open, FileAccess.Read))
            {
                Doc = new XWPFDocument(file);
            }
        }

      
        public void FillTable<T>(T obj)
        {
            try
            {
                Type type = typeof(T);
                var props = obj.GetType().GetProperties().Select(t => t.Name);

                //遍历表格
                var table = Doc.Tables[0];
                foreach (var row in table.Rows)
                {
                    foreach (var cell in row.GetTableCells())
                    {
                        string text = cell.GetText();
                        if (props.Contains(text))
                        {
                            var value = type.GetProperty(text).GetValue(obj, null);
                            cell.Paragraphs[0].ReplaceText(text, value == null ? "" : value.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        

        public void ReplaceKey(string key, string value)
        {
            //遍历段落
            foreach (var para in Doc.Paragraphs)
            {
                foreach (var run in para.Runs)
                {
                    string text = run.ToString();
                    if (text.Contains(key))
                    {
                        try
                        {
                            text = text.Replace(key, value);
                        }
                        catch (Exception ex)
                        {
                            //可能有空指针异常
                            text = text.Replace(key, "");
                        }
                    }
                    run.SetText(text, 0);
                }
            }
        }

        public byte[] GetWord()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Doc.Write(ms);
                return ms.ToArray();
            }
        }

        public void Close()
        {
            Doc.Close();
        }
    }
}