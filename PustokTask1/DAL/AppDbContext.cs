using Microsoft.EntityFrameworkCore;
using PustokTask1.Models;

namespace PustokTask1.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
    }
    
}
