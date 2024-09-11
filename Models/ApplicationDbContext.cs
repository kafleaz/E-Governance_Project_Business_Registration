using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace OCR_E_gov.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company_Table> Company { get; set; }
        public DbSet<Capital_Table> Capital { get; set; }
        public DbSet<Witness_Table> Witness { get; set; }

    }

}
