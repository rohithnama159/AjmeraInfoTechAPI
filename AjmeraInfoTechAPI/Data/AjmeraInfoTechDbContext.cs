using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AjmeraInfoTechAPI.Models.Domain;

namespace AjmeraInfoTechAPI.Data
{
    public class AjmeraInfoTechDbContext : DbContext
    {
        public AjmeraInfoTechDbContext(DbContextOptions<AjmeraInfoTechDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
