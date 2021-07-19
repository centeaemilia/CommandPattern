using System;
using CommandPattern.DataModel.MsSql;

namespace CommandPattern.Test.Utils.MsSql
{
    internal class MsEmployee: BaseData
    {
        public static MsEmployeeRow GenerateRowMock(Guid employeeId,Guid organizationId, int? oracleId) => new MsEmployeeRow
        {
            Id = employeeId,
            OrganizationId=organizationId,
            RegistrationNumber = $"Test_{Rnd.Next()}",
            EmploymentStartDate = DateTime.Today,
            OracleId = oracleId
        };
    }
}