using System;
using System.Collections.Generic;
using CommandPattern.Interfaces;
using CommandPattern.Utils;

namespace CommandPattern.Migration
{
    public class TableSet: ITableProperties
    {       
         public string TableName { get; set; }
        public SortedSet<IGuidIdentity> MsRows { get; set; } = new SortedSet<IGuidIdentity>();
        public SortedSet<IIntIdentity> OraRows { get; set; } = new SortedSet<IIntIdentity>();

        public virtual void SaveToDb()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object other)
        {
            var otherTableSet = other as TableSet;

            if ((other == null)
                || (otherTableSet == null))
            {
                return false;
            }

            if (this == other)
            {
                return true;
            }

            return this.TableName == otherTableSet.TableName;
        }

        public override int GetHashCode() => this.TableName.GetHashCode();

    }
}