using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class ActsProperties : PropertyChangedRealization
    {
        public ActsProperties()
        {
            WorkStartYear = DateTime.Now.Year;
            WorkEndYear = DateTime.Now.Year;
            SignDate = DateTime.Now.Year;
            ActType = "Вид акта не определен";
            ActType_S = "";

        }

        /// <summary>
        /// Номер шифра
        /// </summary>
        public string ChNumber
        {
            get
            {
                return this._ChNumber;
            }
            set
            {
                if (this.ChNumber != value)
                {
                    this._ChNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ChNumber { get; set; }


        /// <summary>
        /// Дата начала работы
        /// </summary>
        public int WorkStartYear
        {
            get
            {
                return this._WorkStartYear;
            }
            set
            {
                if (this.WorkStartYear != value)
                {
                    this._WorkStartYear = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private int _WorkStartYear { get; set; }

        public int WorkEndYear
        {
            get
            {
                return this._WorkEndYear;
            }
            set
            {
                if (this.WorkEndYear != value)
                {
                    this._WorkEndYear = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private int _WorkEndYear { get; set; }

        public int SignDate
        {
            get
            {
                return this._SignDate;
            }
            set
            {
                if (this.SignDate != value)
                {
                    this._SignDate = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private int _SignDate { get; set; }


        public string ActType
        {
            get
            {
                return this._ActType;
            }
            set
            {
                if (this.ActType != value)
                {
                    this._ActType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ActType { get; set; }

        public string ActType_S
        {
            get
            {
                return this._ActType_S;
            }
            set
            {
                if (this.ActType_S != value)
                {
                    this._ActType_S = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ActType_S { get; set; }


        public Organizations ProjectOrganization
        {
            get
            {
                return this._ProjectOrganization;
            }
            set
            {
                if (this.ProjectOrganization != value)
                {
                    this._ProjectOrganization = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Organizations _ProjectOrganization { get; set; }

        public Objects Object
        {
            get
            {
                return this._Object;
            }
            set
            {
                if (this.Object != value)
                {
                    this._Object = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Objects _Object { get; set; }
    }
}
