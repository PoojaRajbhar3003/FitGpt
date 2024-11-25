using FitGpt.Models.DataModels;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FitGpt.Data
{
    public class FitGptDbContext : DbContext
    {
        public FitGptDbContext(DbContextOptions<FitGptDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; } = default!;
        public DbSet<Meal> Meal { get; set; } = default!;
        public DbSet<FoodItem> FoodItem { get; set; } = default!;
        public DbSet<DietitianMeal> DietitianMeal { get; set; } = default!;
        public DbSet<DietitianDetails> DietitianDetails { get; set; } = default!;
        public DbSet<DietitianClients> DietitianClients { get; set; } = default!;
        public DbSet<ClientDetails> ClientDetails { get; set; } = default!;
        public DbSet<ClientMealAssignByDietitian> ClientMealAssignByDietitian { get; set; } = default!;
        public DbSet<FunctionResult> FunctionResult { get; set; }
        public DbSet<DietitianDetailsResult> DietitianDetailsResult { get; set; }
        public DbSet<ClientDetailsResult> ClientDetailsResult { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FunctionResult>().HasNoKey();
            modelBuilder.Entity<DietitianDetailsResult>().HasNoKey();
            modelBuilder.Entity<ClientDetailsResult>().HasNoKey();
        }
    }
}
