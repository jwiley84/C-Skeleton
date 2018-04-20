using Microsoft.EntityFrameworkCore;
 
namespace skeleton.Models
{
    public class skeletonContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public skeletonContext(DbContextOptions<skeletonContext> options) : base(options) { }

	public DbSet<User> users {get; set;}

    public DbSet<AttendeesList> attendeesList {get; set;}
    public DbSet<Activity> activities {get; set;}


    }
}
