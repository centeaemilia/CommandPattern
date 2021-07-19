using System;
using System.Collections.Generic;
using System.Linq;
using CommandPattern.Interfaces;
using CommandPattern.Migration.Oracle;
using CommandPattern.Utils;

namespace CommandPattern.Commands.OracleToMsSql
{
    public abstract class PersistCommand : IPersistCommand
    {
        protected string msTableName;
        protected OracleTableSet tableSet;
        protected PersistCommand(OracleTableSet tableSet)
        {
            this.tableSet = tableSet;
            this.msTableName = TablesMetadata.FindByTableName(DbTypesEnum.Oracle, tableSet.TableName).MsSqlTable.TableName;
        }

        public void Execute<TG>() where TG : IGuidIdentity
        {
            var rowsToMigrate = this.GetInitialRows<TG>();
            var listOfRowsToMigrate = rowsToMigrate == null ? null : (rowsToMigrate as IList<TG> ?? rowsToMigrate.ToList());
            
            this.UpdateOrInsert(listOfRowsToMigrate);

            this.Delete(listOfRowsToMigrate);
        }
        protected abstract IEnumerable<TG> GetInitialRows<TG>()
            where TG : IGuidIdentity;

        protected void UpdateOrInsert<TG>(IList<TG> listOfRowsToMigrate)
            where TG : IGuidIdentity
        {
            var listOfIdToMigrate = listOfRowsToMigrate.Where(n => n.OracleId.HasValue).Select(n => n.OracleId).ToList();
            var listOfExistingIds = this.tableSet.MsRows.Where(n => n.OracleId.HasValue).Select(n => n.OracleId).ToList();

            var rowsToUpdate = listOfRowsToMigrate.Where(n => n.OracleId.HasValue && listOfExistingIds.Contains(n.OracleId));
            var result = Update(rowsToUpdate);

            if (result == false)
                throw new Exception("Update failed");


            var rowsToInsert = listOfRowsToMigrate.Where(n => n.OracleId.HasValue && !listOfExistingIds.Contains(n.OracleId));
            Insert(rowsToInsert);
        }

        protected virtual bool Update<TG>(IEnumerable<TG> rowsToUpdate)
            where TG : IGuidIdentity{
            throw new NotImplementedException($"Update method not implemented for table {this.msTableName}");
        }

        protected abstract void Insert<TG>(IEnumerable<TG> rowsToInsert)
            where TG : IGuidIdentity;

        protected virtual void Delete<TG>(IEnumerable<TG> listOfRowsToMigrate)
                  where TG : IGuidIdentity
        {
            var listOfIdToMigrate = listOfRowsToMigrate.Where(n=>n.OracleId.HasValue).Select(n => n.OracleId).ToList();
            var rowsToDelete = this.tableSet.MsRows.Where(n => n.OracleId.HasValue && !listOfIdToMigrate.Contains(n.OracleId)).Select(n => n).ToList();
            foreach(var row in rowsToDelete)
            this.tableSet.MsRows.Remove(row);
        }
    }
}