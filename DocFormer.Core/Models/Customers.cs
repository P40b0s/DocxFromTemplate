using DocFormer.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
   
    public class Customers : Base, ICustomers
    {
        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string FIO
        {
            get
            {
                return this._FIO;
            }
            set
            {
                if (this.FIO != value)
                {
                    this._FIO = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _FIO { get; set; }
        /// <summary>
        /// Должность пользователя
        /// </summary>
        public string Post
        {
            get
            {
                return this._Post;
            }
            set
            {
                if (this.Post != value)
                {
                    this._Post = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _Post { get; set; }

        /// <summary>
        /// Id организации
        /// </summary>
        public Guid Organization
        {
            get
            {
                return this._Organization;
            }
            set
            {
                if (this.Organization != value)
                {
                    this._Organization = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private Guid _Organization { get; set; }

        /// <summary>
        /// Вид пользователя (Заказчик, подрядчик итд.)
        /// </summary>
        public Guid Type
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
        private Guid _Type { get; set; }

        public override void Update(ICustomers c)
        {
            FIO = c.FIO;
            Post = c.Post;
            Organization = c.Organization;
            Type = c.Type;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Customers;
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
