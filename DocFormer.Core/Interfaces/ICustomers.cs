using System;

namespace DocFormer.Core.Interfaces
{
    public interface ICustomers
    {
        string FIO { get; set; }
        Guid Organization { get; set; }
        string Post { get; set; }
        Guid Type { get; set; }
        void Update(ICustomers c);
    }
}