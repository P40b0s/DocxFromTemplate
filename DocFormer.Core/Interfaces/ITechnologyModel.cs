using System;

namespace DocFormer.Core.Interfaces
{
    public interface ITechnologyModel
    {
        string Count { get; set; }
        Guid CountType { get; set; }
        string TechnologyCreator { get; set; }
        string TechnologyMark { get; set; }
        string TechnologyName { get; set; }
        Guid TechnologyType { get; set; }
        bool IsDeleted { get; set; }
        string Certificate { get; set; }
        string CertificateValidityPeriod { get; set; }

        void Update(ITechnologyModel t);
    }
}