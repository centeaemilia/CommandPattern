using System;
using System.Collections.Generic;
using System.Linq;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Mappers.ToOracle;
using CommandPattern.Migration.MsSql;

namespace CommandPattern.Commands.MsSqlToOracle
{
    public class EmployeePersistCommand: PersistCommand
    {
        private readonly List<MsEmployeeRow> trowList;

        public EmployeePersistCommand(MsSqlTableSet tableSet)
            : base(tableSet)
        {
            this.trowList = tableSet.MsRows.Select(r => r as MsEmployeeRow).ToList();
        }

        protected override Dictionary<Guid, TI> GetInitialRows<TI, TG>()
        {
            var oracleRows = new Dictionary<Guid, OracleEmployeeRow>();

            foreach (var sqlRow in this.trowList)
            {
                var oracleRow = sqlRow.ToOracle();

                oracleRows.Add(sqlRow.Id, oracleRow);
            }

            return oracleRows as Dictionary<Guid, TI>;
        }

        protected override void Insert<TI>(IEnumerable<TI> rowsToInsert)
        {
            
        }

        protected override bool Update<TI>(IEnumerable<TI> rowsToUpdate)
        {


            return true;
        }

        protected override bool Delete<TI, TG>(Dictionary<Guid, TI> initialRows)
        {
            return true; // Delete is not done for this case
        }
    }
}