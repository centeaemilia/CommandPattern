using System;
using System.Collections.Generic;
using System.Linq;
using CommandPattern.DataModel.MsSql;
using CommandPattern.DataModel.Oracle;
using CommandPattern.Migration;
using CommandPattern.Test.Utils.MsSql;
using CommandPattern.Test.Utils.Oracle;
using CommandPattern.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandPattern.Test.MsSql
{
    [TestClass]
    public class EmployeeMigration
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowExceptionWhenTableNameUnknown()
        {
            //Arrange
            MigrateData migration = new MigrateData(toDbType: DbTypesEnum.Oracle, "Test");

            //Act
            migration.Migrate();

            //Assert  - no need      
        }

        [TestMethod]
        public void ShouldMigrateEmployee()
        {
            //Arrange
            var oraRows = new List<OracleEmployeeRow>
            {
                OracleEmployee.GenerateRowMock(1,1), 
                OracleEmployee.GenerateRowMock(2,3) 
            };

            var orgId1 = System.Guid.NewGuid();
            var orgId2 = System.Guid.NewGuid();

            var msRows = new List<MsEmployeeRow>
            {
                MsEmployee.GenerateRowMock(System.Guid.NewGuid(), orgId1, null), //insert
                MsEmployee.GenerateRowMock(System.Guid.NewGuid(), orgId1, 2), //update
                MsEmployee.GenerateRowMock(System.Guid.NewGuid(), orgId2, 5), // insert
            };

            MigrateData migration = new MigrateData(toDbType: DbTypesEnum.Oracle, DbStructure.MsEmployeeTableName);
            migration.MsRows.UnionWith(msRows);
            migration.OracleRows.UnionWith(oraRows);

            //Act
            migration.Migrate();

            var newOracleRows = migration.OracleRows;
            var newMsOrgRows = migration.MsRows;

            //Assert
           // Assert.AreEqual(5, newOracleRows.Count);
            //Assert.AreEqual(msRows.Count, newMsOrgRows.Count);
        }
    }
}