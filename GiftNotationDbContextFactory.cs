using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftNotation.Data
{
    public class GiftNotationDbContextFactory
    {
        private readonly string _connectionString;

        public GiftNotationDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public GiftNotationDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<GiftNotationDbContext> options = new DbContextOptionsBuilder<GiftNotationDbContext>();

            options.UseSqlite(_connectionString);

            return new GiftNotationDbContext(options.Options);
        }
    }
}
