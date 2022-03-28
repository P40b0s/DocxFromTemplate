using DocFormer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Models
{
    public class DocumentsTemplates : Base
    {
        public Guid Name
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
        private Guid _Name { get; set; }

        public string TemplateName
        {
            get
            {
                return this._TemplateName;
            }
            set
            {
                if (this.TemplateName != value)
                {
                    this._TemplateName = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _TemplateName { get; set; }

        public TemplatesActTypeEnum Type
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
        private TemplatesActTypeEnum _Type { get; set; }

        public bool IsTable
        {
            get
            {
                return this._IsTable;
            }
            set
            {
                if (this.IsTable != value)
                {
                    this._IsTable = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private bool _IsTable { get; set; }

        public string OutputFileNamePrefix
        {
            get
            {
                return this._OutputFileNamePrefix;
            }
            set
            {
                if (this.OutputFileNamePrefix != value)
                {
                    this._OutputFileNamePrefix = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _OutputFileNamePrefix { get; set; }
    }
}
