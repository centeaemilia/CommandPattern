using System;
using CommandPattern.DataModel.Oracle;

namespace CommandPattern.Test.Utils.Oracle
{
    internal class OracleEmployee: BaseData
    {
        public static OracleEmployeeRow GenerateRowMock(int employeeId, int organizationId) => new OracleEmployeeRow
        {
            Id_Per = employeeId,
            Id_Org = organizationId,
            RegNr = $"Test_{Rnd.Next()}",
            EmplSD = DateTime.Today,
            Gender='O',
            Birthday=DateTime.Today
        };
        
    }
}