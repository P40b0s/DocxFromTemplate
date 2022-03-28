using DocFormer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.ErrorsValidation
{
    public sealed class CustomErrorType : PropertyChangedRealization
    {
        public CustomErrorType(string validationMessage, ErrorType errortype)
        {
            ValidationMessage = validationMessage;
            MessageErrorType = errortype;
        }
        //[DataMember]
        //public string ValidationMessage { get; private set; }
        private string _ValidationMessage { get; set; }
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ValidationMessage
        {
            get
            {
                return this._ValidationMessage;
            }
            private set
            {
                if (this.ValidationMessage != value)
                {
                    this._ValidationMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        //[DataMember]
        //public ErrorType MessageErrorType { get; private set; }
        private ErrorType _MessageErrorType { get; set; }
        /// <summary>
        /// Enum ошибки
        /// </summary>
        public ErrorType MessageErrorType
        {
            get
            {
                return this._MessageErrorType;
            }
            private set
            {
                if (this.MessageErrorType != value)
                {
                    this._MessageErrorType = value;
                    this.OnPropertyChanged();
                }
            }
        }


        public override bool Equals(object obj)
        {
            var item = obj as CustomErrorType;
            if (item == null)
            {
                return false;
            }
            return this.ValidationMessage.Equals(item.ValidationMessage);
        }
        public override int GetHashCode()
        {
            return this.ValidationMessage.GetHashCode();
        }
        public override string ToString()
        {
            return ValidationMessage;
        }

    }
}
