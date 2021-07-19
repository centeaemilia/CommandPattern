using System;
using CommandPattern.Interfaces;

namespace CommandPattern.DataModel.Oracle
{/// <summary>
 ///     Corresponds to Employee & Person tables from MsSql
 /// </summary>
    public class OracleEmployeeRow : BaseComparer, IIntIdentity
    {
        private int idPer;

        public int Id_Per
        {
            get { return this.idPer; }
            set
            {
                this.Id = value;
                this.idPer = value;
            }
        }

        public int Id_Org { get; set; }
        public string RegNr { get; set; }
        public DateTime EmplSD { get; set; }
        public DateTime? EmplED { get; set; }
        public string PPSN { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public char Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}