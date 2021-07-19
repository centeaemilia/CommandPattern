using System;
using CommandPattern.Commands.OracleToMsSql;
using CommandPattern.DataModel.MsSql;
using CommandPattern.Interfaces;
using CommandPattern.Utils;

namespace CommandPattern.Migration.Oracle
{
    public class OracleTableSet: TableSet, ITableSet
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
                case DbStructure.OracleOrganizationTableName:
                    {
                        return this.InvokeCommandPatternForSave<MsOrganizationRow>(new OrganizationPersistCommand(this),
                                                       out command);                        
                    }
                default:
                    return this.InvokeCommandPatternForSave<IGuidIdentity>(null, out command);
            };

        }

        private bool InvokeCommandPatternForSave<TG>(PersistCommand persistCommand,
                                                    out PersistCommand command)
           where TG : IGuidIdentity
        {
            if (persistCommand == null)
            {
                command = null;
                return false;
            }

            command = persistCommand;

            command.Execute<TG>();
            return true;
        }
    }
}