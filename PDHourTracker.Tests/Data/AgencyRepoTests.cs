using NUnit.Framework;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Tests.Data
{
    [TestFixture]
    public class AgencyRepoTests
    {
        [Test]
        public void CanAddAgency()
        {
            using (var dbContext = SqliteInMemory.GetSqliteDbContext())
            {
                dbContext.Database.EnsureCreated();
                var agencyRepo = new AgencyRepo<Agency>(dbContext);

                // Attempt
                agencyRepo.Add(new Agency
                {
                    AgencyName = "Jones School"
                });

                var agency = agencyRepo.Get(1);

                // Verify
                Assert.AreEqual("Jones School", agency.AgencyName);
            }
        }

        [Test]
        public void CanFindAgency()
        {
            using (var dbContext = SqliteInMemory.GetSqliteDbContext())
            {
                dbContext.Database.EnsureCreated();
                var agencyRepo = new AgencyRepo<Agency>(dbContext);

                var agencyName = "Jones School";

                agencyRepo.Add(new Agency
                {
                    AgencyName = agencyName
                });

                var agencies = agencyRepo.Search(agencyName);

                Assert.AreEqual(agencyName, agencies.First().AgencyName);
            }
        }
    }
}
