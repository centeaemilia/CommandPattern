using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Utils;

namespace CommandPattern.Mappers.ToMsSql
{
    internal static class OrganizationMapper
    {
        public static MsOrganizationRow ToSql(this OracleOrganizationRow org)
        {
            return new MsOrganizationRow
            {
                Code = org.OrgCd,
                Description = org.OrgDescr,
                IsActive = org.Active.MapCharValueToBool(),
                OracleId = org.Id_Org,
            };
        }
    }
}