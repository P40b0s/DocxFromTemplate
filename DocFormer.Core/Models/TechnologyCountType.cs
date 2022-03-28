using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class TechnologyCountType :Base
    {
        /// <summary>
        /// Тип измерения данной технологии (штука, погонный метр и.т.д)
        /// </summary>
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if (this.Type != value)
                {
                    this._Type = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Type { get; set; }
    }
}
