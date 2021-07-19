using System;
using System.Collections.Generic;
using System.Linq;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Interfaces;
using CommandPattern.Mappers.ToMsSql;
using CommandPattern.Migration.Oracle;

namespace CommandPattern.Commands.OracleToMsSql
{
    public class OrganizationPersistCommand: PersistCommand
    {
        private readonly List<OracleOrganizationRow> trowList;

        public OrganizationPersistCommand(OracleTableSet tableSet)
            : base(tableSet)
        {
            this.trowList = tableSet.OraRows.Select(r => r as OracleOrganizationRow).ToList();
        }

        protected override IEnumerable<TG> GetInitialRows<TG>()
        {
            var sqlRows = new List<MsOrganizationRow>();

            foreach (var tRow in this.trowList)
            {
                var sqlRow = tRow.ToSql();
               
                sqlRows.Add(sqlRow);
            }

            return sqlRows as IEnumerable<TG>;
        }

        protected override void Insert<TG>(IEnumerable<TG> rowsToInsert)
        {         
            foreach(var row in rowsToInsert)
            {
                var rowToInsert = row as MsOrganizationRow;
                if( rowToInsert.Id == Guid.Empty)
                    rowToInsert.Id = Guid.NewGuid();

                tableSet.MsRows.Add(row);
            }
                    }

        protected override bool Update<TG>(IEnumerable<TG> rowsToUpdate)
        {
            var rows = rowsToUpdate.Select(n=>n as MsOrganizationRow ).ToList();
            foreach (var row in rows)
            {
                if (row.OracleId == null)
                    continue;

                var rowToUpdate = tableSet.MsRows.SingleOrDefault(n => n.OracleId == row.OracleId) as MsOrganizationRow;
                if (rowToUpdate is null)
                    continue;

                this.tableSet.MsRows.Remove(rowToUpdate);
                rowToUpdate.Code = row.Code;
                rowToUpdate.Description = row.Description;
                rowToUpdate.IsActive = row.IsActive;

                this.tableSet.MsRows.Add(rowToUpdate);
            }       

            return true;
        }
    }
}