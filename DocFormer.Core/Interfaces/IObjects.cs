using System;

namespace DocFormer.Core.Interfaces
{
    public interface IObjects
    {
        string ObjectAdresse { get; set; }
        string ObjectName { get; set; }

        void Update(IObjects o);
    }
}