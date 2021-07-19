using System;
using System.Collections.Generic;
using CommandPattern.Commands.MsSqlToOracle;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Interfaces;
using CommandPattern.Utils;

namespace CommandPattern.Migration.MsSql
{
    public partial class MsSqlTableSet: TableSet, ITableSet
    {
        public override void SaveToDb()
        {
            PersistCommand command;

            var isSaved = Save(out command);

            if (!isSaved)
            {
                throw new NotImplementedException($"Table {this.TableName} has no implementation in the OracleTableSet class, SaveToDb methos! If you see this error, please informe the development team.");
            }
        }

        private bool Save(out PersistCommand command)
        {
            switch (this.TableName)
            {
                case DbStructure.MsOrganizationTableName:
                    {
                        return this.InvokeCommandPatternForSave<OracleOrganizationRow, MsOrganizationRow>(new OrganizationPersistCommand(this),
                                                       out command);
                    }
                case DbStructure.MsEmployeeTableName:
                    {
                        return this.InvokeCommandPatternForSave<OracleEmployeeRow, MsEmployeeRow>(new EmployeePersistCommand(this),
                                                       out command);
                    }
                default:
                    return this.InvokeCommandPatternForSave<IIntIdentity,IGuidIdentity>(null, out command);
            };

        }

        private bool InvokeCommandPatternForSave<TI,TG>(PersistCommand persistCommand,
                                                    out PersistCommand command)
           where TI : IIntIdentity
           where TG : IGuidIdentity
        {
            if (persistCommand == null)
            {
                command = null;
                return false;
            }

            command = persistCommand;

            command.Execute<TI,TG>();
            return true;
        }
    }
}