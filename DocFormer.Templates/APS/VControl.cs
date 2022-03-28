using Autofac;
using DocFormer.Core;
using DocFormer.Core.Interfaces;
using DocFormer.Core.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateEngine.Docx;
using Xceed.Words.NET;

namespace DocFormer.Templates.APS
{

    /// <summary>
    ///АКТ
    ///о проведении входного контроля
    ///(автоматической пожарной сигнализации и автоматики противодымной защиты)
    /// </summary>
    public class VControl : IVControl
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IContainer Container { get; set; }
        ICollections collections;

        bool VisibleErrorsInDoc = false;
        string DirName = DateTime.Now.ToString("dd.MM.yyyy");
        string signalization = "автоматической пожарной сигнализации и автоматики противодымной защиты";
        string evacuation = "системы оповещения и управления эвакуацией";
        string signalization_S = "автоматическая пожарная сигнализация и автоматика противодымной защиты";
        string evacuation_S = "система оповещения и управления эвакуацией";

        public VControl(ICollections c)
        {
            if (c != null)
            {
                collections = c;
            }
        }

        #region Генерация Docx (на крайний случай)
        //Formats f = new Formats();
        //ParagraphsTemplates tmp = new ParagraphsTemplates();
        /// <summary>
        /// Пошел по более простому пути - заполнение шаблонов, осталяю это на самый крайний случай
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap"></param>
        /// <returns></returns>
        //public void GenerateDocx()
        //{
        //    try
        //    {
        //        using (DocX doc = DocX.Create("simple.docx"))
        //        {
        //            Customers zakazchik = new Customers();
        //            zakazchik.FIO = "Плакунов М.А.";
        //            zakazchik.Post = "инженер-электроник";
        //            zakazchik.Organization = "ООО «Альпина Пласт»";

        //            Customers podr = new Customers();
        //            podr.FIO = "Тябликов В.В.";
        //            podr.Post = "начальник электромонтажного участка";
        //            podr.Organization = "ООО «МИС»";

        //            Objects obj = new Objects();
        //            obj.ObjectName = "\"Альпина Пласт\" Офисно-производственно-складские помещения";
        //            obj.ObjectAdresse = "Московская обл., г. Клин, Ленинградское шоссе, 88км, стр. 103";

        //            List<TechnologyList> tech = new List<TechnologyList>();
        //            TechnologyList t = new TechnologyList();
        //            t.TechnologyName = "Адресный приемно-контрольный охранно-пожарный прибор";
        //            t.TechnologyMark = "Рубеж-20П";
        //            t.TechnologyCreator = "ООО «КБПА»";
        //            t.Count = "1 шт.";
        //            tech.Add(t);
        //            tech.Add(t);
        //            tech.Add(t);
        //            tech.Add(t);
        //            tech.Add(t);


        //            var calibri = new Font("Calibri");
        //            var tnr = new Font("Times New Roman");
        //            #region настройка отступов
        //            doc.MarginTop = f.MarginConverter(0.5F);
        //            doc.MarginRight = f.MarginConverter(1.25F);
        //            doc.MarginLeft = f.MarginConverter(2.5F);
        //            doc.MarginBottom = f.MarginConverter(0.75F);
        //            #endregion


        //            #region Заголовок
        //            Paragraph h1 = doc.InsertParagraph();
        //            h1.Append("АКТ\nо проведении входного контроля")
        //                .Font(calibri)
        //                .FontSize(14)
        //                .Bold()
        //                .Alignment = Alignment.center;
        //            h1.SpacingAfter(1);

        //            Paragraph h2 = doc.InsertParagraph();
        //            h2.Append("(автоматической пожарной сигнализации и автоматики противодымной защиты)")
        //                .Font(calibri)
        //                .FontSize(12)
        //                .UnderlineStyle(UnderlineStyle.singleLine)
        //                .Italic()
        //                .Alignment = Alignment.center;
        //            h2.SpacingAfter(15);
        //            #endregion

        //            #region Дата подписания
        //            var signDataTable = doc.AddTable(1, 5);
        //            signDataTable.Alignment = Alignment.right;
        //            signDataTable.AutoFit = AutoFit.Contents;
        //            signDataTable.Rows[0].Cells[0].Paragraphs[0].Append("«", f.TNR12).Alignment = Alignment.center;
        //            signDataTable.Rows[0].Cells[2].Paragraphs[0].Append("»", f.TNR12).Alignment = Alignment.center;
        //            signDataTable.Rows[0].Cells[4].Paragraphs[0].Append("2018г.", f.TNR12).Alignment = Alignment.center;

        //            signDataTable.Rows[0].Cells[1].SetBorder(TableCellBorderType.Bottom, new Border(BorderStyle.Tcbs_single, 0, 0, System.Drawing.Color.Black));
        //            signDataTable.Rows[0].Cells[1].Width = 35;
        //            signDataTable.Rows[0].Cells[3].SetBorder(TableCellBorderType.Bottom, new Border(BorderStyle.Tcbs_single, 0, 0, System.Drawing.Color.Black));
        //            signDataTable.Rows[0].Cells[3].Width = 100;
        //            h2.InsertTableAfterSelf(signDataTable);
        //            #endregion

        //            #region Состав комиссии
        //            Paragraph k1 = doc.InsertParagraph();
        //            k1.Append("Комиссия в составе представителей:", f.TNR12Bold)
        //                .Alignment = Alignment.left;
        //            k1.SpacingBefore(15);
        //            k1.SpacingAfter(8);

        //            Paragraph k2 = doc.InsertParagraph();
        //            k2.Append("Заказчика:", f.TNR12)
        //                .Alignment = Alignment.left;
        //            tmp.SignersTemplate(doc, zakazchik);

        //            Paragraph k5 = doc.InsertParagraph();
        //            k5.Append("Подрядной (монтажной) организации:", f.TNR12)
        //            .Alignment = Alignment.left;
        //            tmp.SignersTemplate(doc, podr);

        //            #endregion

        //            #region Осмотр и проверка
        //            Paragraph o1 = doc.InsertParagraph();
        //            o1.Append("Произвели осмотр и проверку", f.TNR12)
        //                .Alignment = Alignment.left;
        //            o1.Append(" ", f.TNR12);
        //            o1.Append("оборудования системы автоматической пожарной сигнализации и автоматики противодымной защиты", f.TNR12Bold)
        //                .Italic()
        //                .UnderlineStyle(UnderlineStyle.singleLine);
        //            o1.Append(" ", f.TNR12);
        //            o1.Append("на объекте:", f.TNR12);
        //            o1.Append(" ", f.TNR12);
        //            o1.Append(obj.ObjectName, f.TNR12)
        //                .UnderlineStyle(UnderlineStyle.singleLine);
        //            o1.Append(" ", f.TNR12);
        //            o1.Append("по адресу:", f.TNR12);
        //            o1.Append(" ", f.TNR12);
        //            o1.Append(obj.ObjectAdresse, f.TNR12)
        //                .UnderlineStyle(UnderlineStyle.singleLine);
        //            o1.SpacingBefore(20);

        //            Paragraph o2 = doc.InsertParagraph();
        //            o2.Append("(наименование и адрес объекта)", f.TNR8Italic)
        //               .Alignment = Alignment.center;
        //            #endregion

        //            #region Таблица с печечнем технических средств
        //            Paragraph per1 = doc.InsertParagraph();
        //            per1.Append("И составили настоящий акт о том, что технические средства, перечисленные ниже:", f.TNR12Bold)
        //               .Alignment = Alignment.left;
        //            per1.SpacingAfter(5);

        //            //задать число строк равным размеру списка оборудования
        //            var columnsWidth = new float[] { f.TableSizeConverter(1.3F),
        //                                             f.TableSizeConverter(7.0F),
        //                                             f.TableSizeConverter(3.50F),
        //                                             f.TableSizeConverter(1.5F),
        //                                             f.TableSizeConverter(4.0F) };
        //            var techTable = doc.InsertTable(2 + tech.Count, columnsWidth.Length);
        //            techTable.Alignment = Alignment.center;
        //            techTable.AutoFit = AutoFit.Contents;

        //            var blackBorder = new Border(BorderStyle.Tcbs_single, 0, 0, System.Drawing.Color.Black);
        //            techTable.SetBorder(TableBorderType.Bottom, blackBorder);
        //            techTable.SetBorder(TableBorderType.InsideH, blackBorder);
        //            techTable.SetBorder(TableBorderType.InsideV, blackBorder);
        //            techTable.SetBorder(TableBorderType.Top, blackBorder);
        //            techTable.SetBorder(TableBorderType.Left, blackBorder);
        //            techTable.SetBorder(TableBorderType.Right, blackBorder);



        //            techTable.Rows[0].Cells[0].Paragraphs[0].Append("№\nп/п", f.TNR12).Alignment = Alignment.center;
        //            techTable.Rows[1].Cells[0].Paragraphs[0].Append("1", f.TNR12Italic).Alignment = Alignment.center;


        //            techTable.Rows[0].Cells[1].Paragraphs[0].Append("Наименование", f.TNR12).Alignment = Alignment.center;
        //            techTable.Rows[1].Cells[1].Paragraphs[0].Append("2", f.TNR12Italic).Alignment = Alignment.center;

        //            techTable.Rows[0].Cells[2].Paragraphs[0].Append("Тип, марка", f.TNR12).Alignment = Alignment.center;
        //            techTable.Rows[1].Cells[2].Paragraphs[0].Append("3", f.TNR12Italic).Alignment = Alignment.center;

        //            techTable.Rows[0].Cells[3].Paragraphs[0].Append("Кол-\nво", f.TNR12).Alignment = Alignment.center;
        //            techTable.Rows[1].Cells[3].Paragraphs[0].Append("4", f.TNR12Italic).Alignment = Alignment.center;

        //            techTable.Rows[0].Cells[4].Paragraphs[0].Append("Завод-изготовитель", f.TNR12).Alignment = Alignment.center;
        //            techTable.Rows[1].Cells[4].Paragraphs[0].Append("5", f.TNR12Italic).Alignment = Alignment.center;



        //            for (int i = 0; i < tech.Count; i++)
        //            {
        //                techTable.Rows[2 + i].Cells[0].Paragraphs[0].Append((i + 1).ToString(), f.TNR12).Alignment = Alignment.center;
        //                techTable.Rows[2 + i].Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //                techTable.Rows[2 + i].Cells[1].Paragraphs[0].Append(tech[i].TechnologyName, f.TNR12).Alignment = Alignment.left;
        //                techTable.Rows[2 + i].Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //                techTable.Rows[2 + i].Cells[2].Paragraphs[0].Append(tech[i].TechnologyMark, f.TNR12).Alignment = Alignment.center;
        //                techTable.Rows[2 + i].Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //                techTable.Rows[2 + i].Cells[3].Paragraphs[0].Append(tech[i].Count, f.TNR12).Alignment = Alignment.center;
        //                techTable.Rows[2 + i].Cells[3].VerticalAlignment = VerticalAlignment.Center;
        //                techTable.Rows[2 + i].Cells[4].Paragraphs[0].Append(tech[i].TechnologyCreator, f.TNR12).Alignment = Alignment.center;
        //                techTable.Rows[2 + i].Cells[4].VerticalAlignment = VerticalAlignment.Center;
        //            }

        //            //Тест заполнения
        //            //techTable.Rows[2].Cells[0].Paragraphs[0].Append("1", f.TNR12).Alignment = Alignment.center;
        //            //techTable.Rows[2].Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //            //techTable.Rows[2].Cells[1].Paragraphs[0].Append("Адресный приемно-контрольный охранно-пожарный прибор", f.TNR12).Alignment = Alignment.left;
        //            //techTable.Rows[2].Cells[1].VerticalAlignment = VerticalAlignment.Center;
        //            //techTable.Rows[2].Cells[2].Paragraphs[0].Append("Рубеж-20П", f.TNR12).Alignment = Alignment.center;
        //            //techTable.Rows[2].Cells[2].VerticalAlignment = VerticalAlignment.Center;
        //            //techTable.Rows[2].Cells[3].Paragraphs[0].Append("1", f.TNR12).Alignment = Alignment.center;
        //            //techTable.Rows[2].Cells[3].VerticalAlignment = VerticalAlignment.Center;
        //            //techTable.Rows[2].Cells[4].Paragraphs[0].Append("ООО «КБПА»", f.TNR12).Alignment = Alignment.center;
        //            //techTable.Rows[2].Cells[4].VerticalAlignment = VerticalAlignment.Center;


        //            techTable.Rows[0].Cells[0].Width = columnsWidth[0];
        //            techTable.Rows[0].Cells[1].Width = columnsWidth[1];
        //            techTable.Rows[0].Cells[2].Width = columnsWidth[2];
        //            techTable.Rows[0].Cells[3].Width = columnsWidth[3];
        //            techTable.Rows[0].Cells[4].Width = columnsWidth[4];


        //            #endregion

        //            #region Заключение
        //            Paragraph z1 = doc.InsertParagraph();
        //            z1.Append("прошли входной контроль и соответствуют технической документации предприятий-изготовителей", f.TNR12Bold)
        //               .Alignment = Alignment.left;
        //            z1.SpacingBefore(20);

        //            Paragraph z2 = doc.InsertParagraph();
        //            z2.Append("Приложения:", f.TNR12)
        //               .Alignment = Alignment.left;
        //            z2.Append(" ", f.TNR12);
        //            z2.Append("технические паспорта и сертификаты на оборудование", f.TNR12).UnderlineStyle(UnderlineStyle.singleLine);
        //            z2.SpacingAfter(20);
        //            #endregion

        //            #region Подписи
        //            Paragraph pod1 = doc.InsertParagraph();
        //            pod1.Append("ПРЕДСТАВИТЕЛИ:", f.TNR12Bold)
        //                .Alignment = Alignment.left;
        //            pod1.SpacingBefore(15);
        //            pod1.SpacingAfter(8);

        //            Paragraph pod2 = doc.InsertParagraph();
        //            pod2.Append("Заказчика:", f.TNR12)
        //                .Alignment = Alignment.left;
        //            pod2.SpacingAfter(2);

        //            tmp.SignersTableTemplate(doc, zakazchik);

        //            Paragraph pod5 = doc.InsertParagraph();
        //            pod5.Append("Подрядной (монтажной) организации:", f.TNR12)
        //            .Alignment = Alignment.left;
        //            pod5.SpacingAfter(2);

        //            tmp.SignersTableTemplate(doc, podr);


        //            #endregion


        //            doc.Save();
        //        }
        //    }
        //    catch (Exception ex) { logger.Fatal(ex); }
        //}
        #endregion
         
        
        public void TEST()
        {




            string nullValues = "";
            try
            {
                List<IContentItem> fields = new List<IContentItem>();
                    TableContent TechTable = new TableContent("testtable");
                
                    FieldContent ctype = new FieldContent("customerType", "Лицо осуществляющее строительные действия");
                    FieldContent name = new FieldContent("organization", "ООО «Компания СЛГ» 143081, Московская обл, Одинцовский р-н, д.Солослово, тер.КИЗ Горки-8, д.275, пом.9, ИНН 5032257713, ОГРН 1125032009167, тел: 8 -(495)775-44-44");
                    FieldContent type = new FieldContent("type", "(наименование, номер и дата выдачи свидетельства о государственной регистрации, ОГРН, ИНН, почтовые реквизиты, телефон/факс)");
                TechTable.AddRow(ctype, name, type);
                TechTable.AddRow(ctype, name, type);

                fields.Add(TechTable);

                #region Заполняем Doc файл
             
                var fill = new Content(fields.ToArray());
                var n = fill.Fields.Where(f => f.Value == null).Select(s => s.Name);
                foreach (var nil in n)
                {
                    nullValues += nil + " ";
                }
                using (var outputDocument = new TemplateProcessor("Шаблоны\\Вентиляция\\TEST.docx")
                    .SetRemoveContentControls(true))
                {
                    outputDocument.SetNoticeAboutErrors(VisibleErrorsInDoc);
                    outputDocument.FillContent(fill);
                    outputDocument.SaveChanges();

                }
                #endregion
            }
            catch (Exception ex)
            {

                logger.Fatal(ex);
                logger.Fatal("Какое то из необходимых для заполнения значений не заполнено: " + nullValues);
            }
        }


        #region Заполнение шаблона Docx
        private List<IContentItem> getWordfields(List<TechnologyModel> tech, ActsProperties ap)
        {
            try
            {
                #region Заполняем документ
                List<IContentItem> fields = new List<IContentItem>();
                List<Customers> customers = collections.Customers;

                #region Заполнение класса ActsProperties
                if (ap != null)
                {
                    fields.Add(new FieldContent(DocumentsTags.ActType, ap.ActType));
                    fields.Add(new FieldContent(DocumentsTags.SignDate, ap.SignDate.ToString()));
                    fields.Add(new FieldContent(DocumentsTags.WorkStartYear, ap.WorkStartYear.ToString()));
                    fields.Add(new FieldContent(DocumentsTags.WorkEndYear, ap.WorkEndYear.ToString()));
                    fields.Add(new FieldContent(DocumentsTags.ChNumber, ap.ChNumber));
                    //Необходимо другое склонение
                    fields.Add(new FieldContent(DocumentsTags.ActType_Single, ap.ActType_S));
                    //Тавтология с словосочетанием Система системы...
                    fields.Add(new FieldContent(DocumentsTags.ActType_Single_W_S, ap.ActType.Replace("системы ", "")));


                   

                }
                #endregion

                if (customers != null)
                {
                    foreach (Customers c in customers)
                    {

                        if (c.Type == Core.IdIdentification.CustomerType.Заказчик)
                        {
                            var organization = collections.Organizations.Where(o => o.Id == c.Organization).FirstOrDefault();
                            if (organization == null)
                                throw new Exception($"У субъекта {c.FIO} нет связанной с ним организации");

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Заказчик +
                                                        DocumentsTags.Organization.Name, organization.Name));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Заказчик +
                                                        DocumentsTags.Customer.Post, c.Post));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Заказчик +
                                                        DocumentsTags.Customer.FIO, c.FIO));

                        }
                        if (c.Type == Core.IdIdentification.CustomerType.Проектировщик)
                        {
                            var organization = collections.Organizations.Where(o => o.Id == c.Organization).FirstOrDefault();
                            fields.Add(new FieldContent(DocumentsTags.Prefix.Проектировщик +
                                               DocumentsTags.Organization.Name, organization.Name));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Проектировщик +
                                                        DocumentsTags.Organization.LicenseNumber, organization.LicenseNumber));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Проектировщик +
                                                        DocumentsTags.Organization.LicenseValidityPeriod, organization.LicenseValidityPeriod));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Проектировщик +
                                                        DocumentsTags.Organization.LicenseIssued, organization.LicenseIssued));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Проектировщик +
                                                        DocumentsTags.Organization.Address, organization.Address));
                        }

                        if (c.Type == Core.IdIdentification.CustomerType.Подрядчик)
                        {
                            var organization = collections.Organizations.Where(o => o.Id == c.Organization).FirstOrDefault();
                            if (organization == null)
                                throw new Exception($"У субъекта {c.FIO} нет связанной с ним организации");

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                       DocumentsTags.Organization.Name, organization.Name));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                        DocumentsTags.Customer.Post, c.Post));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                        DocumentsTags.Customer.FIO, c.FIO));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                        DocumentsTags.Organization.LicenseNumber, organization.LicenseNumber));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                        DocumentsTags.Organization.LicenseValidityPeriod, organization.LicenseValidityPeriod));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                        DocumentsTags.Organization.LicenseIssued, organization.LicenseIssued));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Подрядчик +
                                                        DocumentsTags.Organization.Address, organization.Address));
                        }

                        if (c.Type == Core.IdIdentification.CustomerType.Экспуатирующая)
                        {
                            var organization = collections.Organizations.Where(o => o.Id == c.Organization).FirstOrDefault();
                            if (organization == null)
                                throw new Exception($"У субъекта {c.FIO} нет связанной с ним организации");

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Эксплуатирующий +
                                                       DocumentsTags.Organization.Name, organization.Name));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Эксплуатирующий +
                                                        DocumentsTags.Customer.Post, c.Post));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Эксплуатирующий +
                                                        DocumentsTags.Customer.FIO, c.FIO));
                        }

                        if (c.Type == Core.IdIdentification.CustomerType.Технадзор)
                        {
                            var organization = collections.Organizations.Where(o => o.Id == c.Organization).FirstOrDefault();
                            if (organization == null)
                                throw new Exception($"У субъекта {c.FIO} нет связанной с ним организации");

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Технадзор +
                                                       DocumentsTags.Organization.Name, organization.Name));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Технадзор +
                                                        DocumentsTags.Customer.Post, c.Post));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Технадзор +
                                                        DocumentsTags.Customer.FIO, c.FIO));
                        }

                        if (c.Type == Core.IdIdentification.CustomerType.Составитель)
                        {
                            fields.Add(new FieldContent(DocumentsTags.Prefix.Создатель +
                                                       DocumentsTags.Customer.Post, c.Post));

                            fields.Add(new FieldContent(DocumentsTags.Prefix.Создатель +
                                                        DocumentsTags.Customer.FIO, c.FIO));
                        }
                    }
                }
                if (ap.Object != null)
                {
                    fields.Add(new FieldContent(DocumentsTags.Object.ObjectName, ap.Object.ObjectName));
                    fields.Add(new FieldContent(DocumentsTags.Object.ObjectAddresse, ap.Object.ObjectAdresse));
                }
                #endregion

                #region Заполняем таблицу
                if (tech != null)
                {
                    TableContent TechTable = new TableContent(DocumentsTags.Table.TableName);
                    for (int i = 0; i < tech.Count; i++)
                    {
                        FieldContent tcount = new FieldContent(DocumentsTags.Table.Index, (i + 2).ToString());
                        FieldContent name = new FieldContent(DocumentsTags.Table.Name, tech[i].TechnologyName);
                        FieldContent mark = new FieldContent(DocumentsTags.Table.Mark, string.IsNullOrEmpty(tech[i].TechnologyMark) ? "" : tech[i].TechnologyMark);
                        FieldContent count = new FieldContent(DocumentsTags.Table.Count, tech[i].Count);
                        FieldContent counttype = new FieldContent(DocumentsTags.Table.CountType, collections.CountType[tech[i].CountType]);
                        FieldContent creator = new FieldContent(DocumentsTags.Table.Creator, string.IsNullOrEmpty(tech[i].TechnologyCreator) ? "" : tech[i].TechnologyCreator);
                        TechTable.AddRow(tcount, name, mark, count, creator);
                    }
                    fields.Add(TechTable);
                }
                #endregion
                return fields;
            }
            catch (Exception ex)
            {
                logger.Fatal(ex);
                return new List<IContentItem>();
            }
        }


        private void FillWordTemplate(List<TechnologyModel> tech, ActsProperties ap, string docPath)
        {
            string nullValues = "";
            try
            {
                #region Заполняем Doc файл
                var fields = getWordfields(tech, ap);
                var fill = new Content(fields.ToArray());
                var n = fill.Fields.Where(f => f.Value == null).Select(s => s.Name);
                foreach (var nil in n)
                {
                    nullValues += nil + " ";
                }
                using (var outputDocument = new TemplateProcessor(docPath)
                    .SetRemoveContentControls(true))
                {
                    outputDocument.SetNoticeAboutErrors(VisibleErrorsInDoc);
                    outputDocument.FillContent(fill);
                    outputDocument.SaveChanges();

                }
                #endregion
            }
            catch (Exception ex)
            {

                logger.Fatal(ex);
                logger.Fatal("Какое то из необходимых для заполнения значений не заполнено: " + nullValues);
                logger.Fatal(docPath);
            }
        }
        #endregion



        public void GenerateActs(ActsProperties ap, DocumentsTemplates template)
        {
            try
            {
                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                               , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                               , DirName));

                string templatePath = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , template.TemplateName);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , template.OutputFileNamePrefix + template.TemplateName);

                File.Copy(templatePath, formed, true);
                List<TechnologyModel> tech = null;
                if(template.IsTable)
                {
                    if(template.Type == Core.Enums.TemplatesActTypeEnum.АПС)
                    {
                        tech = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваАПС && !t.IsDeleted).ToList();
                    }
                    if (template.Type == Core.Enums.TemplatesActTypeEnum.СОУЭ)
                    {
                        tech = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваСОУЭ && !t.IsDeleted).ToList();
                    }
                }
                FillWordTemplate(tech, ap, formed);
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }




        #region Акты приемки оборудования
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктПриемкиОборудования_АПС(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = signalization;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                               , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                               , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктПриемки);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксАПС + ActTypes.СписокШаблоновАПС.АктПриемки);

                File.Copy(template, formed, true);

                var technologies = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваАПС).ToList();
                FillWordTemplate(technologies, ap, formed);
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктПриемкиОборудования_СОУЭ(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = evacuation;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                               , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                               , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктПриемки);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксСОУЭ + ActTypes.СписокШаблоновАПС.АктПриемки);

                File.Copy(template, formed, true);

                var technologies = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваСОУЭ).ToList();
                FillWordTemplate(technologies, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        #endregion

        #region Акты приемки смонтированного оборудования
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктПриемкиСмонтированногоОборудования_АПС(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = signalization;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                               , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                               , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктПриемкиСмонтированногоОборудования);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксАПС + ActTypes.СписокШаблоновАПС.АктПриемкиСмонтированногоОборудования);

                File.Copy(template, formed, true);

                var technologies = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваАПС).ToList();
                FillWordTemplate(technologies, ap, formed);
            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктПриемкиСмонтированногоОборудования_СОУЭ(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = evacuation;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктПриемкиСмонтированногоОборудования);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксСОУЭ + ActTypes.СписокШаблоновАПС.АктПриемкиСмонтированногоОборудования);

                File.Copy(template, formed, true);

                var technologies = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваСОУЭ).ToList();
                FillWordTemplate(technologies, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }


        #endregion

        #region Акты приемки в эксплуатацию
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктПриемаВЭксплуатацию_АПС(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = signalization;
                ap.ActType_S = signalization_S;
                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктПриемаВЭкспуатацию);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксАПС + ActTypes.СписокШаблоновАПС.АктПриемаВЭкспуатацию);

                File.Copy(template, formed, true);

                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктПриемаВЭксплуатацию_СОУЭ(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = evacuation; //формируется локально
                ap.ActType_S = evacuation_S; //формируется локально

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктПриемаВЭкспуатацию);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксСОУЭ + ActTypes.СписокШаблоновАПС.АктПриемаВЭкспуатацию);

                File.Copy(template, formed, true);

                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion

        #region Акты об окончании пуско-наладочных работ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктОбОкончанииПНР_АПС(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ;
                ap.ActType = signalization;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктОбОкончанииПНР);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксАПС + ActTypes.СписокШаблоновАПС.АктОбОкончанииПНР);

                File.Copy(template, formed, true);

                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктОбОкончанииПНР_СОУЭ(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = evacuation_S.Replace("система ", "");

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктОбОкончанииПНР);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксСОУЭ + ActTypes.СписокШаблоновАПС.АктОбОкончанииПНР);

                File.Copy(template, formed, true);

                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion

        #region Акт скрытых работ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктСкрытыхРабот_АПС(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = signalization;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктОсвидетельствованияСкрытыхРабот);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксАПС + ActTypes.СписокШаблоновАПС.АктОсвидетельствованияСкрытыхРабот);

                File.Copy(template, formed, true);


                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void АктСкрытыхРабот_СОУЭ(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = evacuation_S.Replace("система ", "");

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.АктОсвидетельствованияСкрытыхРабот);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксСОУЭ + ActTypes.СписокШаблоновАПС.АктОсвидетельствованияСкрытыхРабот);

                File.Copy(template, formed, true);


                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }

        #endregion

        #region Ведомость смонтированного оборудования
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void ВедомостьОборудования_АПС(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = signalization;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ВедомостьОборудования);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксАПС + ActTypes.СписокШаблоновАПС.ВедомостьОборудования);

                File.Copy(template, formed, true);

                // проверить тип технических средств до добавления их в метод формирования шаблонов

                var technologies = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваАПС).ToList();

                FillWordTemplate(technologies, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="tech"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void ВедомостьОборудования_СОУЭ(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = evacuation;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ВедомостьОборудования);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПрефиксСОУЭ + ActTypes.СписокШаблоновАПС.ВедомостьОборудования);

                File.Copy(template, formed, true);

                var technologies = collections.Technologies.Where(t => t.TechnologyType == IdIdentification.TechnologyType.СредстваСОУЭ).ToList();
                FillWordTemplate(technologies, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion

        #region Протокол заземления и изоляции
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void ПротоколЗаземления(ActsProperties ap, bool IsNeedTable = false)
        {
            try
            {
                ap.ActType = signalization;

                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ПротоколЗаземления);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.ПротоколЗаземления);

                File.Copy(template, formed, true);

                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion

        #region Перечень актов xlsx
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="obj"></param>
        /// <param name="ap">Необходимые данные: SignDate, ChNumber, WorkStartYear, WorkEndYear</param>
        public void Перечень(ActsProperties ap)
        {
            try
            {
                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                ///Создать эксель на EPPLUS

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion

        #region Титульный лист
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void ТитульныйЛист(ActsProperties ap)
        {
            try
            {
                var dir = Directory.CreateDirectory(Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                                                , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                                                , DirName));

                string template = Path.Combine(ActTypes.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                             , ActTypes.СписокШаблоновАПС.Титульный);

                string formed = Path.Combine(ActTypes.ДиректорияСформированныхДокументов
                                           , ActTypes.СписокШаблоновАПС.ДиректорияШаблонов
                                           , DirName
                                           , ActTypes.СписокШаблоновАПС.Титульный);

                File.Copy(template, formed, true);


                FillWordTemplate(null, ap, formed);

            }
            catch (Exception ex) { logger.Fatal(ex); }
        }
        #endregion


    }
}
