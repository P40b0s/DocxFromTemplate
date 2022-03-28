using DocFormer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class Organizations : Base, IOrganizations
    {
        /// <summary>
        /// Наименование организации
        /// </summary>
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
        /// <summary>
        /// Юридический адрес организации
        /// </summary>
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                if (this.Address != value)
                {
                    this._Address = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Address { get; set; }

        /// <summary>
        /// Номер лицензии
        /// </summary>
        public string LicenseNumber
        {
            get
            {
                return this._LicenseNumber;
            }
            set
            {
                if (this.LicenseNumber != value)
                {
                    this._LicenseNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _LicenseNumber { get; set; }

        /// <summary>
        /// Срок действия лицензии
        /// </summary>
        public string LicenseValidityPeriod
        {
            get
            {
                return this._LicenseValidityPeriod;
            }
            set
            {
                if (this.LicenseValidityPeriod != value)
                {
                    this._LicenseValidityPeriod = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _LicenseValidityPeriod { get; set; }

        /// <summary>
        /// Кем выдана лицензия
        /// </summary>
        public string LicenseIssued
        {
            get
            {
                return this._LicenseIssued;
            }
            set
            {
                if (this.LicenseIssued != value)
                {
                    this._LicenseIssued = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _LicenseIssued { get; set; }

        public string Ogrn
        {
            get
            {
                return this._Ogrn;
            }
            set
            {
                if (this.Ogrn != value)
                {
                    this._Ogrn = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Ogrn { get; set; }

        public string Inn
        {
            get
            {
                return this._Inn;
            }
            set
            {
                if (this.Inn != value)
                {
                    this._Inn = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Inn { get; set; }

        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if (this.Phone != value)
                {
                    this._Phone = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Phone { get; set; }

        public override void Update(IOrganizations o)
        {
            Name = o.Name;
            Address = o.Address;
            LicenseNumber = o.LicenseNumber;
            LicenseValidityPeriod = o.LicenseValidityPeriod;
            LicenseIssued = o.LicenseIssued;
            Ogrn = o.Ogrn;
            Inn = o.Inn;
            Phone = o.Phone;
        }
    }
}
