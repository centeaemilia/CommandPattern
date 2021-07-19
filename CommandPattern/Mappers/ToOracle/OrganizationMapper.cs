using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Utils;

namespace CommandPattern.Mappers.ToOracle
{
    internal static class OrganizationMapper
    {
        public static OracleOrganizationRow ToOracle(this MsOrganizationRow org)
        {
            return new OracleOrganizationRow
            {
                Id_Org = org.OracleId ?? 0,
                OrgCd = org.Code,
                OrgDescr = org.Description,
                Active = org.IsActive.MapBoolToCharValue(),
            };
        }
    }
}