using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandPattern;
using System;
using CommandPattern.Migration;
using CommandPattern.Utils;
using System.Collections.Generic;
using CommandPattern.Test.Utils.Oracle;
using CommandPattern.DataModel.Oracle;
using CommandPattern.DataModel.MsSql;
using CommandPattern.Test.Utils.MsSql;
using System.Linq;

namespace CommandPattern.Test
{
    [TestClass]
    public class OrganizationMigration
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
        public void ShouldMigrateOrganization()
        {
            //Arrange
            var oraOrgRows = new List<OracleOrganizationRow>
            {
                OracleOrganization.GenerateRowMock(1,"01"), //delete - not implemented
                OracleOrganization.GenerateRowMock(2,"B1"), //upadate
                OracleOrganization.GenerateRowMock(9,"abc") //delete - not implemented
            };

            var msOrgRows = new List<MsOrganizationRow>
            {
                MsOrganization.GenerateRowMock(System.Guid.NewGuid(), "02", null), //insert
                MsOrganization.GenerateRowMock(System.Guid.NewGuid(), "B2", 2), //update
                MsOrganization.GenerateRowMock(System.Guid.NewGuid(), "xyz", 5), // ignore?
            };

            MigrateData migration = new MigrateData(toDbType: DbTypesEnum.Oracle, DbStructure.MsOrganizationTableName);
            migration.MsRows.UnionWith(msOrgRows);
            migration.OracleRows.UnionWith(oraOrgRows);

            //Act
            migration.Migrate();

            var newOracleOrgRows = migration.OracleRows;
            var newMsOrgRows = migration.MsRows;

            //Assert
            Assert.AreEqual(5, newOracleOrgRows.Count);
            Assert.AreEqual(msOrgRows.Count, newMsOrgRows.Count);

            var rowOrg = newOracleOrgRows.Where(n => n.Id == 2).SingleOrDefault() as OracleOrganizationRow;
            Assert.AreEqual("B2", rowOrg.OrgCd);

            var rowOrgDel = newOracleOrgRows.Where(n => n.Id == 1 || n.Id == 9).Count();
            Assert.AreEqual(2, rowOrgDel);
        }
    }
}