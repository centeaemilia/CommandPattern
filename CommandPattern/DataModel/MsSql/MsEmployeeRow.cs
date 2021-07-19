using System;
using CommandPattern.Interfaces;

namespace CommandPattern.DataModel.MsSql
{
    public class MsEmployeeRow: BaseComparer, IGuidIdentity
    {
        public Guid PersonId { get; set; }
        public Guid OrganizationId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime EmploymentStartDate { get; set; }
        public DateTime? EmploymentEndDate { get; set; }
    }
}