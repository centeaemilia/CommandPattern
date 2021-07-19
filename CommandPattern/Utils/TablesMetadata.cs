using System.Collections.Generic;

namespace CommandPattern.Utils
{
    public class TablePair
    {
        public TableMetadata OracleTable { get; set; }
        public TableMetadata MsSqlTable { get; set; }
    }

    public class TableMetadata
    {
        public TableMetadata()
        {
            this.TableName = null;
        }

        public string TableName { get; set; }
    }

    public static class TablesMetadata
    {
        public static readonly List<TablePair> Tables = new List<TablePair>
                                                        {
                                                            new TablePair
                                                            {
                                                                OracleTable = CreateTableMetadata(DbStructure.OracleOrganizationTableName),
                                                                MsSqlTable = CreateTableMetadata(DbStructure.MsOrganizationTableName)
                                                            },
                                                            new TablePair
                                                            {
                                                                OracleTable = CreateTableMetadata(DbStructure.OracleEmployeeTableName),
                                                                MsSqlTable = CreateTableMetadata(DbStructure.MsEmployeeTableName)
                                                            }
                                                        };

        public static readonly Dictionary<string, TablePair> MsSqlTableNameDictionary =
             new Dictionary<string, TablePair>();

        public static readonly Dictionary<string, TablePair> OracleTableNameDictionary =
            new Dictionary<string, TablePair>();

        static TablesMetadata()
        {
            if (MsSqlTableNameDictionary.Count > 0
                && OracleTableNameDictionary.Count > 0)
            {
                return;
            }

            foreach (var tablePair in Tables)
            {
                MsSqlTableNameDictionary.Add(tablePair.MsSqlTable.TableName, tablePair);
                if (tablePair.OracleTable.TableName != null && !OracleTableNameDictionary.ContainsKey(tablePair.OracleTable.TableName))
                {
                    OracleTableNameDictionary.Add(tablePair.OracleTable.TableName, tablePair);
                }              
            }
        }

        public static TablePair FindByTableName(DbTypesEnum dbType, string tableName)
        {
            IDictionary<string, TablePair> tableNameDictionary = dbType == DbTypesEnum.MsSql
                                                                     ? MsSqlTableNameDictionary
                                                                     : OracleTableNameDictionary;

            if (!tableNameDictionary.ContainsKey(tableName))
            {
                throw new KeyNotFoundException();
            }

            return tableNameDictionary[tableName];
        }

        public static bool ContainsTableName(DbTypesEnum dbType, string tableName)
        {
            IDictionary<string, TablePair> tableNameDictionary = dbType == DbTypesEnum.MsSql
                                                                     ? MsSqlTableNameDictionary
                                                                     : OracleTableNameDictionary;

            return tableNameDictionary.ContainsKey(tableName);
        }

        private static TableMetadata CreateTableMetadata(string tableName)
        {
            return new TableMetadata
            {
                TableName = tableName
            };
        }
}
}