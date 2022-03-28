using DocFormer.Core.Enums;
using DocFormer.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Interfaces
{
    public interface ICollections
    {
        List<Organizations> Organizations { get; set; }
        Dictionary<Guid, string> CustomerType { get; set; }
        Dictionary<Guid, string> TechnologyType { get; set; }
        Dictionary<Guid, string> CountType { get; set; }
        List<TechnologyModel> Technologies { get; set; }
        List<Customers> Customers { get; set; }
        List<Objects> Objects { get; set; }
        List<DocumentsNames> DocNames { get; set; }
        List<DocumentsTemplates> DocTemplates { get; set; }

        void SyncCollection(ObservableCollection<Organizations> col);
        void SyncCollection(ObservableCollection<Customers> col);
        void SyncCollection(ObservableCollection<Objects> col);
        void SyncCollection(ObservableCollection<TechnologyModel> col);

        string InsertOrUpdate(Organizations org);
        string InsertOrUpdate(Customers c);
        string InsertOrUpdate(Objects obj);
        string InsertOrUpdate(TechnologyModel tech);


        string Delete(Organizations org);
        string Delete(Customers c);
        string Delete(Objects obj);
        string Delete(TechnologyModel tech);


    }
}
