using System;
using Microsoft.EntityFrameworkCore;

namespace Store.DataModel
{
    public class Session 
    {
        private readonly DbContextOptions _options;
        public Session(string connectionString) {
            var optionsBuilder = new DbContextOptionsBuilder(); // Project1Context 
            optionsBuilder.UseSqlServer(connectionString); // optionsBuilder.UseSqlite(connectionString);
            _options = optionsBuilder.Options;
        }
        public void AddCustomer(ICustomer customer) {
            using var context = new Project1Context();
        }
        public void Save() {
            using var context = new Project1Context();
            context.SaveChanges();
        }
        
    }
}

