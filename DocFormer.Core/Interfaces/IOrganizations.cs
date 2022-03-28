namespace DocFormer.Core.Interfaces
{
    public interface IOrganizations
    {
        string Address { get; set; }
        string LicenseIssued { get; set; }
        string LicenseNumber { get; set; }
        string LicenseValidityPeriod { get; set; }
        string Name { get; set; }
        string Ogrn { get; set; }
        string Inn { get; set; }
        string Phone { get; set; }

        void Update(IOrganizations o);
    }
}