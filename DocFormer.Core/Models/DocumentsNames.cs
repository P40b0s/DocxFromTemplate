using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class DocumentsNames : Base
    {
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (this.Name != value)
                {
                    this._Name = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Name { get; set; }

        public string SingleName
        {
            get
            {
                return this._SingleName;
            }
            set
            {
                if (this.SingleName != value)
                {
                    this._SingleName = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _SingleName { get; set; }
    }
}
