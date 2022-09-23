using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MAQUINAVENDAS.Models
{
    public class MaquinaVendasContext : DbContext
    {
        public MaquinaVendasContext(DbContextOptions<MaquinaVendasContext> options) : base(options){}
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}