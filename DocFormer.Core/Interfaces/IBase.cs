using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocFormer.Core.Interfaces
{
    public interface IBase
    {
        Guid Id { get; set; }
        void Update(ICustomers c);
        void Update(IObjects obj);
        void Update(IOrganizations org);
        void Update(ITechnologyModel tech);
    }
}
