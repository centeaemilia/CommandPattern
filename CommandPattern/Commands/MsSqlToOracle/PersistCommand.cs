using System;
using System.Collections.Generic;
using System.Linq;
using CommandPattern.Interfaces;
using CommandPattern.Migration.MsSql;
using CommandPattern.Utils;

namespace CommandPattern.Commands.MsSqlToOracle
{
    public abstract class PersistCommand : IPersistCommand
    {
        protected IList<IdMapping> idMapping;
        protected string oraTableName;
        protected MsSqlTableSet tableSet;

        public PersistCommand(MsSqlTableSet tableSet)
        {
            this.oraTableName = TablesMetadata.FindByTableName(DbTypesEnum.MsSql, tableSet.TableName).OracleTable.TableName;
            this.tableSet = tableSet;
        }

        public void Execute<TI, TG>()
                   where TI : IIntIdentity
                   where TG : IGuidIdentity
        {
            var rowsToMigrate = this.GetInitialRows<TI, TG>();

            this.UpdateOrInsert<TI, TG>(rowsToMigrate);

            this.Delete<TI, TG>(rowsToMigrate);
        }

        protected virtual bool Update<TI>(IEnumerable<TI> rowsToUpdate)
                   where TI : IIntIdentity
        {
            throw new NotImplementedException();
        }

        protected virtual void Insert<TI>( IEnumerable<TI> rowsToInsert)
            where TI : IIntIdentity
        {
            throw new NotImplementedException();
        }

        protected virtual bool Delete<TI, TG>(Dictionary<Guid, TI> initialRows)
            where TI : IIntIdentity
            where TG : IGuidIdentity
        {
            throw new NotImplementedException();
        }

        protected abstract Dictionary<Guid, TI> GetInitialRows<TI, TG>()
            where TI : IIntIdentity
            where TG : IGuidIdentity;
        
        private void UpdateOrInsert<TI, TG>( Dictionary<Guid, TI> listOfRowsToMigrate)
            where TI : IIntIdentity
            where TG : IGuidIdentity
        {
            var listOfExistingIds = this.tableSet.OraRows.Select(n => n.Id).ToList();
            var rowsToUpdate = listOfRowsToMigrate.Where(n => n.Value.Id !=0 && listOfExistingIds.Contains(n.Value.Id)).Select(n=>n.Value);
            
            var result = Update(rowsToUpdate);

            if (result == false)
                throw new Exception("Update failed");

            var rowsToInsert = listOfRowsToMigrate.Where(n => n.Value.Id==0 || !listOfExistingIds.Contains(n.Value.Id)).Select(n => n.Value);
            Insert(rowsToInsert);
        }
    }
}