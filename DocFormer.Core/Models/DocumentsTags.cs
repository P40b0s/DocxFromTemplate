using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class DocumentsTags
    {
        /// <summary>
        /// Наименование акта
        /// </summary>
        public const string ActType = "ActType";
        /// <summary>
        /// Дата подписания акта
        /// </summary>
        public const string SignDate = "SignDate";
        /// <summary>
        /// Дата начала работ
        /// </summary>
        public const string WorkStartYear = "WorkStartYear";
        /// <summary>
        /// Дата окончания работ
        /// </summary>
        public const string WorkEndYear = "WorkEndYear";
        /// <summary>
        /// Шифр проекта
        /// </summary>
        public const string ChNumber = "ChNumber";
        /// <summary>
        /// Наименование акта в единственном числе, например: системы - система
        /// </summary>
        public const string ActType_Single = "ActType_Single";
        /// <summary>
        /// Убираем наслоение в некоторых строках получается системы система... итд
        /// </summary>
        public const string ActType_Single_W_S = "ActType_Single_W_S";
        #region Организации
        public struct Prefix
        {
            /// <summary>
            /// Проектировщик
            /// </summary>
            public const string Проектировщик = "Proj_";
            /// <summary>
            /// Заказчик
            /// </summary>
            public const string Заказчик = "Zak_";
            /// <summary>
            /// Подрядчик
            /// </summary>
            public const string Подрядчик = "Podr_";
            /// <summary>
            /// Эксплуатирующая организация
            /// </summary>
            public const string Эксплуатирующий = "Eksp_";
            /// <summary>
            /// Технидзор
            /// </summary>
            public const string Технадзор = "Nadzor_";
            /// <summary>
            /// Составитель
            /// </summary>
            public const string Создатель = "Creator_";
        }

        public struct Customer
        {
            /// <summary>
            /// Должность субъекта
            /// </summary>
            public const string Post = "Post";
            /// <summary>
            /// ФИО субьекта
            /// </summary>
            public const string FIO = "FIO";
          
        }

        public struct Object
        {
            /// <summary>
            /// Наименование объекта
            /// </summary>
            public const string ObjectName = "Customer_Object";
            /// <summary>
            /// Адрес объекта
            /// </summary>
            public const string ObjectAddresse = "Customer_Object_Adresse";

        }

        public struct Organization
        {
            /// <summary>
            /// Проектная организация
            /// </summary>
            public const string Name = "Organization";
            /// <summary>
            /// Номер лицензии проектной организации
            /// </summary>
            public const string LicenseNumber = "Organization_LicenseNumber";
            /// <summary>
            /// Срок действия лицензии проектной организации
            /// </summary>
            public const string LicenseValidityPeriod = "Organization_LicenseValidityPeriod";
            /// <summary>
            /// Кем выдана лицензия проектной организации
            /// </summary>
            public const string LicenseIssued = "Organization_LicenseIssued";
            /// <summary>
            /// Адрес проектной организации
            /// </summary>
            public const string Address = "Organization_Address";
            /// <summary>
            /// Телефоны организации
            /// </summary>
            public const string Phones = "Organization_Phones";
        }


        public struct Table
        {
            /// <summary>
            /// Наименование таблицы
            /// </summary>
            public const string TableName = "TechTable";
            /// <summary>
            /// Индекс записи
            /// </summary>
            public const string Index = "TableCount";
            /// <summary>
            /// Наименование оборудования
            /// </summary>
            public const string Name = "TableName";
            /// <summary>
            /// Марка оборудования
            /// </summary>
            public const string Mark = "TableMark";
            /// <summary>
            /// Количество оборудования
            /// </summary>
            public const string Count = "TableTechCount";
            /// <summary>
            /// Единица измерения
            /// </summary>
            public const string CountType = "TableTechCountType";
            /// <summary>
            /// Создатель оборудования
            /// </summary>
            public const string Creator = "TableCreator";
        }
        #endregion
    }
}
