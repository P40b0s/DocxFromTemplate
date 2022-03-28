using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.ErrorsValidation
{
    public class Errors
    {
        /// <summary>
        /// Не введен принявший орган!
        /// </summary>
        public static readonly CustomErrorType ORGAN_ERROR = new CustomErrorType("Не введен принявший орган!", ErrorType.ERROR);
        /// <summary>
        /// Возможно не изменен принявший орган!
        /// </summary>
        public static readonly CustomErrorType ORGAN_MJ_WARNING = new CustomErrorType("Возможно не изменен принявший орган!", ErrorType.WARNING);
        /// <summary>
        /// Не введен вид документа!
        /// </summary>
        public static readonly CustomErrorType ACTTYPE_ERROR = new CustomErrorType("Не введен вид документа!", ErrorType.ERROR);
        /// <summary>
        /// Не введен номер документа!
        /// </summary>
        public static readonly CustomErrorType ACTNUMBER_ERROR = new CustomErrorType("Не введен номер документа!", ErrorType.ERROR);
        /// <summary>
        /// Для такого типа документов номер не заполняется
        /// </summary>
        public static readonly CustomErrorType ACTNUMBER_WARNING = new CustomErrorType("Для такого типа документов номер не заполняется", ErrorType.WARNING);
        /// <summary>
        /// Не введена дата подписания документа!
        /// </summary>
        public static readonly CustomErrorType SIGNDATE_ERROR = new CustomErrorType("Не введена дата подписания документа!", ErrorType.ERROR);
        /// <summary>
        /// Дата подписания документа не может быть больше dd.MM.yyyy
        /// </summary>
        public static readonly CustomErrorType SIGNDATEFUTURE_ERROR = new CustomErrorType("Дата подписания документа не может быть больше " + DateTime.Now.ToShortDateString(), ErrorType.ERROR);
        /// <summary>
        /// Не введена дата регистрации документа в Министерстве юстиции!
        /// </summary>
        public static readonly CustomErrorType MJREGDATE_ERROR = new CustomErrorType("Не введена дата регистрации документа в Министерстве юстиции!", ErrorType.ERROR);
        /// <summary>
        /// Для такого типа документов дата регистрации в Министерстве юстиции не заполняется!
        /// </summary>
        public static readonly CustomErrorType MJREGDATENOTALLOWED_ERROR = new CustomErrorType("Для такого типа документов дата регистрации в Министерстве юстиции не заполняется!", ErrorType.ERROR);
        /// <summary>
        /// Дата регистрации в Министерстве юстиции не может быть больше dd.MM.yyyy
        /// </summary>
        public static readonly CustomErrorType MJREGDATEFUTURE_ERROR = new CustomErrorType("Дата регистрации в Министерстве юстиции не может быть больше " + DateTime.Now.ToShortDateString(), ErrorType.ERROR);
        /// <summary>
        /// Не введен номер регистрации документа в Министерстве юстиции!
        /// </summary>
        public static readonly CustomErrorType MJREGNUMBER_ERROR = new CustomErrorType("Не введен номер регистрации документа в Министерстве юстиции!", ErrorType.ERROR);
        /// <summary>
        /// Для такого типа документов номер регистрации в Министерстве юстиции не заполняется!
        /// </summary>
        public static readonly CustomErrorType MJREGNUMBERNOTALLOWED_ERROR = new CustomErrorType("Для такого типа документов номер регистрации в Министерстве юстиции не заполняется!", ErrorType.ERROR);
        /// <summary>
        /// У документов Мида номер не заполняется
        /// </summary>
        public static readonly CustomErrorType MIDNUMBERNOTALLOWED_INFO = new CustomErrorType("У документов Мида номер не заполняется", ErrorType.INFO);
        /// <summary>
        /// Не забудьте после загрузки в издание удалить кавычки в наименовании документа
        /// </summary>
        public static readonly CustomErrorType KSACTNAME_INFO = new CustomErrorType("Не забудьте после загрузки в издание удалить кавычки в наименовании документа", ErrorType.INFO);
        /// <summary>
        /// Внимательно проверьте соотвествие кавычек с PDF документом, они иногда отсуствуют
        /// </summary>
        public static readonly CustomErrorType ACTPREZ_INFO = new CustomErrorType("Внимательно проверьте соотвествие кавычек с PDF документом, они иногда отсуствуют", ErrorType.INFO);
        /// <summary>
        /// Не забудьте изменить наименование документа согласно названию в PDF образе
        /// </summary>
        public static readonly CustomErrorType MJNAME_INFO = new CustomErrorType("Не забудьте изменить наименование документа согласно названию в PDF образе", ErrorType.INFO);
        /// <summary>
        /// После загрузки в издание не забудьте изменить наименование. Наименование формируемое в Издании некорректно
        /// </summary>
        public static readonly CustomErrorType MIDNAME_INFO = new CustomErrorType("После загрузки в издание не забудьте изменить наименование. Наименование формируемое в Издании некорректно.", ErrorType.INFO);
    }
}
