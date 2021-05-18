using NUnit.Framework;
using PDHourTracker.Core.Entities;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Tests.Data
{
    [TestFixture]
    public class ProviderCodeRepoTests
    {
        [Test]
        public void GetsCurrentProviderCode()
        {
            using (var dbContext = SqliteInMemory.GetSqliteDbContext())
            {
                dbContext.Database.EnsureCreated();
                var providerCodeRepo = new ProviderCodeRepo<ProviderCode>(dbContext);

                var providerCode1 = new ProviderCode
                {
                    Code = "414",
                    StartDate = new DateTime(2018, 7, 1),
                    EndDate = new DateTime(2019, 6, 30)
                };

                var providerCode2 = new ProviderCode
                {
                    Code = "525",
                    StartDate = new DateTime(2019, 7, 1),
                    EndDate = new DateTime(2020, 6, 30)
                };
                providerCodeRepo.AddRange(providerCode1, providerCode2);

                var currentProviderCode = providerCodeRepo.GetCurrent();

                Assert.AreEqual("525", currentProviderCode.Code);
            }
        }
    }
}
