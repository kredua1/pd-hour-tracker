using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Thanks to: https://www.thereformedprogrammer.net/using-in-memory-databases-for-unit-testing-ef-core-applications/

namespace PDHourTracker.Tests
{
    public static class SqliteInMemory
    {
        public static DbContextOptions<T> CreateOptions<T>()
            where T : DbContext
        {
            // This creates the SQLite string to in-memory data
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();

            // This creates a SQLiteConnection with that string
            var connection = new SqliteConnection(connectionString);

            // The connection MUST be opened here
            connection.Open();

            // Now we have the EF core commands to create SQLite options
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlite(connection);
            builder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);

            return builder.Options;
        }

        public static AppDbContext GetSqliteDbContext()
        {
            return new AppDbContext(
                SqliteInMemory.CreateOptions<AppDbContext>());
        }
    }
}
