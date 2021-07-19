using System;
using CommandPattern.DataModel.MsSql;

namespace CommandPattern.Test.Utils.MsSql
{
    internal class MsOrganization : BaseData
    {
        public static MsOrganizationRow GenerateRowMock(Guid organizationId, string code, int? oracleId) => new MsOrganizationRow
        {
            Id=organizationId,
            Code = code,
            IsActive = true,
            Description = $"Test_{Rnd.Next()}",
            OracleId=oracleId
        };
    }
}