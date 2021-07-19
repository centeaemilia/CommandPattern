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

namespace CommandPattern.Test.Oracle
{
    [TestClass]
    public class OrganizationMigration
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldThrowExceptionWhenTableNameUnknown()
        {
            //Arrange
            MigrateData migration = new MigrateData(toDbType: DbTypesEnum.MsSql, "Test");

            //Act
            migration.Migrate();
        
            //Assert  - no need      
        }

        [TestMethod]
        public void ShouldMigrateOrganization()
        {
            //Arrange
            var oraOrgRows = new List<OracleOrganizationRow>
            {
                OracleOrganization.GenerateRowMock(1,"01"), // insert
                OracleOrganization.GenerateRowMock(2,"B1"), // update
                OracleOrganization.GenerateRowMock(9,"abc") // insert
            };

            var msOrgRows = new List<MsOrganizationRow>
            {
                MsOrganization.GenerateRowMock(System.Guid.NewGuid(), "02", null), //ignored 
                MsOrganization.GenerateRowMock(System.Guid.NewGuid(), "B2", 2), //update
                MsOrganization.GenerateRowMock(System.Guid.NewGuid(), "xyz", 5), //delete
            };

            MigrateData migration = new MigrateData(toDbType: DbTypesEnum.MsSql, DbStructure.OracleOrganizationTableName);
            migration.MsRows.UnionWith(msOrgRows);
            migration.OracleRows.UnionWith(oraOrgRows);

            //Act
            migration.Migrate();

            var newOracleOrgRows = migration.OracleRows;
            var newMsOrgRows = migration.MsRows;

            //Assert
            Assert.AreEqual(4, newMsOrgRows.Count);
            Assert.AreEqual(oraOrgRows.Count, newOracleOrgRows.Count);

            var row = newMsOrgRows.Where(n => !n.OracleId.HasValue ).ToList();
            Assert.IsTrue(row.Count==1);

            row = newMsOrgRows.Where(n => n.OracleId.HasValue && n.OracleId.Value==5).ToList();
            Assert.IsTrue(row.Count == 0);

            var rowOrg = newMsOrgRows.Single(n => n.OracleId.HasValue && n.OracleId.Value == 2) as MsOrganizationRow;
            Assert.AreEqual("B1", rowOrg.Code);

            rowOrg = newMsOrgRows.Single(n => n.OracleId.HasValue && n.OracleId.Value == 9) as MsOrganizationRow;
            Assert.AreEqual("abc", rowOrg.Code);

            rowOrg = newMsOrgRows.Single(n => n.OracleId.HasValue && n.OracleId.Value == 1) as MsOrganizationRow;
            Assert.AreEqual("01", rowOrg.Code);
        }
    }
}