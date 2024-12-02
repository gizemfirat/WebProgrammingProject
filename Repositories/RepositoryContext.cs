using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repositories
{
  public class RepositoryContext : DbContext
  {
    public DbSet<Admin> Admins {get; set;}
    public DbSet<Customer> Customers {get; set;}
    public DbSet<Worker> Workers {get; set;}
    public DbSet<Profession> Professions {get; set;}
    public DbSet<AvaliableTime> AvaliableTimes {get; set;}
    public DbSet<Appointment> Appointments {get; set;}

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) {
      
    }
  }
}