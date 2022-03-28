using DocFormer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class TechnologyModel : Base, ITechnologyModel
    {
        /// <summary>
        /// Идентификатор типа к которой привязаны технические средства (Автоматическая пожарная сигнализация и.т.д...)
        /// </summary>
        public Guid TechnologyType
        {
            get
            {
                return this._TechnologyType;
            }
            set
            {
                if (this.TechnologyType != value)
                {
                    this._TechnologyType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Guid _TechnologyType { get; set; }

        /// <summary>
        /// Фирма-изготовитель технического средства
        /// </summary>
        public string TechnologyCreator
        {
            get
            {
                return this._TechnologyCreator;
            }
            set
            {
                if (this.TechnologyCreator != value)
                {
                    this._TechnologyCreator = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _TechnologyCreator { get; set; }

        /// <summary>
        /// Тип, Марка технического средства
        /// </summary>
        public string TechnologyMark
        {
            get
            {
                return this._TechnologyMark;
            }
            set
            {
                if (this.TechnologyMark != value)
                {
                    this._TechnologyMark = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _TechnologyMark { get; set; }


        /// <summary>
        /// Наименование технического средства
        /// </summary>
        public string TechnologyName
        {
            get
            {
                return this._TechnologyName;
            }
            set
            {
                if (this.TechnologyName != value)
                {
                    this._TechnologyName = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _TechnologyName { get; set; }

        /// <summary>
        /// Тип измерения данной технологии (штука, погонный метр и.т.д)
        /// </summary>
        public Guid CountType
        {
            get
            {
                return this._CountType;
            }
            set
            {
                if (this.CountType != value)
                {
                    this._CountType = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Guid _CountType { get; set; }

        public string Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                if (this.Count != value)
                {
                    this._Count = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Count { get; set; }

        public bool IsDeleted
        {
            get
            {
                return this._IsDeleted;
            }
            set
            {
                if (this.IsDeleted != value)
                {
                    this._IsDeleted = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool _IsDeleted { get; set; }


        public string Certificate
        {
            get
            {
                return this._Certificate;
            }
            set
            {
                if (this.Certificate != value)
                {
                    this._Certificate = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Certificate { get; set; }

        public string CertificateValidityPeriod
        {
            get
            {
                return this._CertificateValidityPeriod;
            }
            set
            {
                if (this.CertificateValidityPeriod != value)
                {
                    this._CertificateValidityPeriod = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _CertificateValidityPeriod { get; set; }





        public override void Update(ITechnologyModel t)
        {
            TechnologyType = t.TechnologyType;
            TechnologyCreator = t.TechnologyCreator;
            TechnologyMark = t.TechnologyMark;
            TechnologyName = t.TechnologyName;
            CountType = t.CountType;
            Count = t.Count;
            IsDeleted = t.IsDeleted;
            Certificate = t.Certificate;
            CertificateValidityPeriod = t.CertificateValidityPeriod;
        }


        public override bool Equals(object obj)
        {
            var item = obj as TechnologyModel;
            if (item == null)
            {
                return false;
            }
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return this.Id.ToString("B");
        }
    }
}
