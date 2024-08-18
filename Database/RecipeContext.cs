using Microsoft.EntityFrameworkCore;
using kitchen_api.Database.Entities;

namespace kitchen_api.Database
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options)
            : base (options)
        { 
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
