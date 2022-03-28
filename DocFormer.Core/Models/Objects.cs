using DocFormer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class Objects : Base, IObjects
    {

        /// <summary>
        /// Наименование объекта
        /// </summary>
        public string ObjectName
        {
            get
            {
                return this._ObjectName;
            }
            set
            {
                if (this.ObjectName != value)
                {
                    this._ObjectName = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ObjectName { get; set; }

        /// <summary>
        /// Адрес объекта
        /// </summary>
        public string ObjectAdresse
        {
            get
            {
                return this._ObjectAdresse;
            }
            set
            {
                if (this.ObjectAdresse != value)
                {
                    this._ObjectAdresse = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ObjectAdresse { get; set; }


        public override void Update(IObjects o)
        {
            ObjectName = o.ObjectName;
            ObjectAdresse = o.ObjectAdresse;
        }

    }
}
