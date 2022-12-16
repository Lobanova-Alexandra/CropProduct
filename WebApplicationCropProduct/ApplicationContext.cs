using Microsoft.EntityFrameworkCore;
using WebApplicationCropProduct;

namespace WebApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<t_crops> t_crop { get; set; } = null!;
        public DbSet<t_soil_indicators_ref> t_soil_indicator_ref { get; set; } = null!; //таблица показатели справочникв
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}