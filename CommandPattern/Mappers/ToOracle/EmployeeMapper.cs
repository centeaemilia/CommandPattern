using System;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Utils;

namespace CommandPattern.Mappers.ToOracle
{
    internal static class EmployeeMapper
    {
        public static OracleEmployeeRow ToOracle(this MsEmployeeRow employee)
        {
            return new OracleEmployeeRow
            {
                Id_Per = employee.OracleId ?? 0,
                RegNr = employee.RegistrationNumber,
                EmplSD = employee.EmploymentStartDate,
                EmplED = employee.EmploymentEndDate,
                LastName = string.Empty,
                FirstName = string.Empty,
                PPSN = string.Empty,
                Birthday = DateTime.MinValue,
                Gender = 'O'
            };
        }
    }
}