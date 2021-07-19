using System;
using CommandPattern.Interfaces;

namespace CommandPattern.DataModel.Oracle
{
    public class BaseComparer: IComparable
    {
        public int Id { get; set; }

        public int CompareTo(object other)
        {
            var otherRow = other as BaseComparer;
            if ((other == null)
                || (otherRow == null))
            {
                return 0;
            }
            return string.CompareOrdinal(this.Id.ToString(), otherRow.Id.ToString());
        }

        public override bool Equals(object other)
        {
            var otherRow = other as BaseComparer;

            if ((other == null)
                || (otherRow == null))
            {
                return false;
            }

            if (this == other)
            {
                return true;
            }

            return this.Id == otherRow.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}