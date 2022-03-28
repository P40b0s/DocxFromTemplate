using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class Exceptions : PropertyChangedRealization
    {
        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if (this.Id != value)
                {
                    this._Id = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private int _Id { get; set; }

        public string Exception
        {
            get
            {
                return this._Exception;
            }
            set
            {
                if (this.Exception != value)
                {
                    this._Exception = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Exception { get; set; }
    }
}
