using CommandPattern.Interfaces;
using CommandPattern.Utils;

namespace CommandPattern.DataModel.Oracle
{
    public class OracleOrganizationRow : BaseComparer, IIntIdentity
    {
        private int idOrg;

        public int Id_Org { get { return this.idOrg; } set { this.Id = this.idOrg = value; } }
        public string OrgCd { get; set; }
        public string OrgDescr { get; set; }
        public char Active { get; set; }

    }
}