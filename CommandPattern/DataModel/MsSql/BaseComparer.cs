using System;

namespace CommandPattern.DataModel.MsSql
{
    public class BaseComparer : IComparable
    {
        public Guid Id { get; set; }
        public int? OracleId { get; set; }

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