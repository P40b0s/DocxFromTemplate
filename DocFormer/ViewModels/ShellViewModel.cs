using Autofac;
using DocFormer.Core;
using DocFormer.Core.Interfaces;
using DocFormer.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.ViewModels
{
    class ShellViewModel : PropertyChangedRealization
    {
        private static IContainer Container { get; set; }

        ICollections collections;
        IVControl VhControlActs;

        public ShellViewModel(ICollections c, IVControl v)
        {

            collections = c;
            VhControlActs = v;
            GetVersion();
            
        }

        private void GetVersion()
        {
            Assembly exe = Assembly.Load(File.ReadAllBytes("DocFormer.exe"));
            string LaunchDir = new Uri(exe.CodeBase).LocalPath.Replace("DocFormer.exe", null);
            ShellTitle = string.Format("Система подготовки актов \"DocFormer\" {0} alpha", exe.FullName.Split(',')[1].Replace("Version=", ""));
        }

        public string ShellTitle
        {
            get
            {
                return this._ShellTitle;
            }
            set
            {
                if (this.ShellTitle != value)
                {
                    this._ShellTitle = value;
                    this.OnPropertyChanged();
                }
            }
        }
        private string _ShellTitle { get; set; }
    }
}
