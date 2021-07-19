using System;
using System.Collections.Generic;
using CommandPattern.Interfaces;
using CommandPattern.Migration.MsSql;
using CommandPattern.Migration.Oracle;
using CommandPattern.Utils;

namespace CommandPattern.Migration
{
    public class MigrateData
    {
        public SortedSet<IGuidIdentity> MsRows { get; set; } = new SortedSet<IGuidIdentity>();
        public SortedSet<IIntIdentity> OracleRows { get; set; } = new SortedSet<IIntIdentity>();
        private TableSet tableSet;

        public MigrateData(DbTypesEnum toDbType, string tableName)
        {
            tableSet = toDbType == DbTypesEnum.Oracle ? new MsSqlTableSet() : new OracleTableSet();
            tableSet.MsRows = this.MsRows;
            tableSet.OraRows = this.OracleRows;
            tableSet.TableName = tableName;
        }

        public void Migrate()
        {
            tableSet.SaveToDb();
        }
    }
}
