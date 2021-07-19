using System;
using System.Collections.Generic;
using System.Linq;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Mappers.ToOracle;
using CommandPattern.Migration.MsSql;

namespace CommandPattern.Commands.MsSqlToOracle
{
    public class OrganizationPersistCommand: PersistCommand
    {
        private readonly List<MsOrganizationRow> trowList;

        public OrganizationPersistCommand(MsSqlTableSet tableSet)
            : base(tableSet)
        {
            this.trowList = tableSet.MsRows.Select(r => r as MsOrganizationRow).ToList();
        }

        protected override Dictionary<Guid, TI> GetInitialRows<TI, TG>()
        {
            var oracleRows = new Dictionary<Guid, OracleOrganizationRow>();

            foreach (var sqlRow in this.trowList)
            {
                var oracleRow = sqlRow.ToOracle();

                oracleRows.Add(sqlRow.Id,oracleRow);
            }

            return oracleRows as Dictionary<Guid, TI>;
        }

         protected override void Insert<TI>(IEnumerable<TI> rowsToInsert)
        {
            foreach (var row in rowsToInsert)
            {
                var rowToInsert = row as OracleOrganizationRow;
                if (rowToInsert.Id == 0)
                {
                    var nextId = this.tableSet.OraRows.Max(n => n.Id) + 1;

                    rowToInsert.Id_Org = nextId;
                }
                tableSet.OraRows.Add(rowToInsert);
            }
        }

        protected override bool Update<TI>(IEnumerable<TI> rowsToUpdate)
        {
            var rows = rowsToUpdate.Select(n => n as OracleOrganizationRow).ToList();
            foreach (var row in rows)
            {
                if (row.Id == 0)
                    continue;

                var rowToUpdate = tableSet.OraRows.SingleOrDefault(n => n.Id == row.Id) as OracleOrganizationRow;
                if (rowToUpdate is null)
                    continue;

                this.tableSet.OraRows.Remove(rowToUpdate);
                rowToUpdate.OrgCd = row.OrgCd;
                rowToUpdate.OrgDescr = row.OrgDescr;
                rowToUpdate.Active = row.Active;

                this.tableSet.OraRows.Add(rowToUpdate);
            }

            return true;
        }

        protected override bool Delete<TI, TG>(Dictionary<Guid, TI> initialRows){
            return true; // Delete is not done for this case
        } 
    }
}