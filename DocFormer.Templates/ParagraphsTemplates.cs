using DocFormer.Core;
using DocFormer.Core.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace DocFormer.Templates
{
    class ParagraphsTemplates
    {
        public readonly Logger logger = LogManager.GetCurrentClassLogger();
        Formats f = new Formats();


        public void SignersTemplate(DocX doc, Customers c)
        {
            try
            {
                if (c != null)
                {
                    string s = "(наименование организации, должность, инициалы, фамилия)";
                    {
                        Paragraph p = doc.InsertParagraph();
                        p.Append($"{c.Organization}, {c.Post} {c.FIO}", f.TNR12Italic)
                        .Alignment = Alignment.center;
                        p.InsertHorizontalLine();
                        p.SpacingAfter(0);





                        Paragraph p1 = doc.InsertParagraph();
                        p1.Append(s, f.TNR8Italic)
                            .Alignment = Alignment.center;
                    }

                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        public void SignersTableTemplate(DocX doc, Customers c)
        {
            try
            {
                if (c != null)
                {
                    var blackBorder = new Border(BorderStyle.Tcbs_single, 0, 0, System.Drawing.Color.Black);
                    string s = "(наименование организации, должность, подпись)";
                    {
                        var s1 = doc.InsertTable(1, 2);
                        s1.Alignment = Alignment.center;
                        s1.AutoFit = AutoFit.Window;
                        s1.SetBorder(TableBorderType.Bottom, blackBorder);
                        s1.Rows[0].Cells[0].Paragraphs[0].Append($"{c.Organization}\n{c.Post}", f.TNR12).Alignment = Alignment.left;
                        s1.Rows[0].Cells[1].Paragraphs[0].Append($"{c.FIO}", f.TNR12).Alignment = Alignment.right;
                        s1.Rows[0].Cells[1].VerticalAlignment = VerticalAlignment.Bottom;


                        Paragraph p1 = doc.InsertParagraph();
                        p1.Append(s, f.TNR8Italic)
                            .Alignment = Alignment.center;
                    }

                }
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
    }
}
