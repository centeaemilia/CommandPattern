using CommandPattern.DataModel.Oracle;

namespace CommandPattern.Test.Utils.Oracle
{
    internal class OracleOrganization : BaseData
    {
        public static OracleOrganizationRow GenerateRowMock(int id, string code) => new OracleOrganizationRow
        {
            Id_Org=id,
            OrgCd = code,
            Active = '1',
            OrgDescr = $"Test_{Rnd.Next()}"
        };
    }
}