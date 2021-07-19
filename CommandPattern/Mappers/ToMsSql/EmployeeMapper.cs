using System;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Utils;

namespace CommandPattern.Mappers.ToMsSql
{
    internal static class EmployeeMapper
    {
        public static  MsEmployeeRow ToSql(this OracleEmployeeRow oraclePerson)
        {
            return new MsEmployeeRow
            {
                RegistrationNumber = oraclePerson.RegNr,
                EmploymentStartDate = oraclePerson.EmplSD,
                EmploymentEndDate = oraclePerson.EmplED,
                OracleId = oraclePerson.Id_Per,
            };
        }
    }
}