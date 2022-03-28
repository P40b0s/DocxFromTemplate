using DocFormer.Core;
using DocFormer.Core.Enums;
using DocFormer.Core.Interfaces;
using DocFormer.Core.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer
{
    public class Collections : SQLMethods, ICollections
    {
       
        public Collections()
        {
            Organizations = new List<Organizations>();
            CustomerType = new Dictionary<Guid, string>();
            TechnologyType = new Dictionary<Guid, string>();
            Technologies = new List<TechnologyModel>();
            CountType = new Dictionary<Guid, string>();
            Customers = new List<Customers>();
            Objects = new List<Objects>();
            DocNames = new List<DocumentsNames>();
            DocTemplates = new List<DocumentsTemplates>();
            SelectCollections();
        }
       
    }
}
