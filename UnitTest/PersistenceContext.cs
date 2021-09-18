using API.Data;
using API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class PersistenceContext : DbContext, IDbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options)
        {
           
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<FunctionalCharacteristic> FunctionalCharacteristics { get; set; }
        public DbSet<FunctionalCharacteristicByRole> FunctionalCharacteristicByRoles { get; set; }
    }
}
