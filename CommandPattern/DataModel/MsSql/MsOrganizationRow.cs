using System;
using CommandPattern.Interfaces;

namespace CommandPattern.DataModel.MsSql
{
    public class MsOrganizationRow : BaseComparer, IGuidIdentity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}