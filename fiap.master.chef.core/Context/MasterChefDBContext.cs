using fiap.master.chef.core.Models;
using Microsoft.EntityFrameworkCore;

namespace fiap.master.chef.core.Context
{
    public class MasterChefDBContext : DbContext
    {
        public MasterChefDBContext()
        {

        }
        public MasterChefDBContext(DbContextOptions<MasterChefDBContext> options) 
            : base(options)
        {

        }

        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}
