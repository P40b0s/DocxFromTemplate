using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Templates
{
    public class ActTypes
    {
        public const string ДиректорияШаблонов = "Шаблоны";
        public const string ДиректорияСформированныхДокументов = "Сформированные документы";
        /// <summary>
        /// Шаблоны АПС
        /// </summary>
        public struct СписокШаблоновАПС
        {
            public const string ПрефиксАПС = "(АПС) ";
            public const string ПрефиксСОУЭ = "(СОУЭ) ";
            public const string ДиректорияШаблонов = "АПС";
            public const string АктПриемки = "АКТ приемки оборудования.docx";
            public const string АктПриемкиСмонтированногоОборудования = "АКТ приемки смонтированного оборудования.docx";
            public const string АктПриемаВЭкспуатацию = "АКТ приема в эксплуатацию.docx";
            public const string АктОбОкончанииПНР = "АОСР акт об окончании ПНР.docx";
            public const string АктОсвидетельствованияСкрытыхРабот = "АОСР по прокладке кабеля.docx";
            public const string ВедомостьОборудования = "Ведомость смонтированного оборудования.docx";
            public const string Титульный = "Титульный.docx";
            public const string ПротоколЗаземления = "Протокол Заземления и изоляции.docx";
        }



    }
}
