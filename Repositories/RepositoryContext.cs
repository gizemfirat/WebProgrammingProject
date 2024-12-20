using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Repositories
{
  public class RepositoryContext : IdentityDbContext<IdentityUser>
  {
    public DbSet<Customer> Customers {get; set;}
    public DbSet<Worker> Workers {get; set;}
    public DbSet<Profession> Professions {get; set;}
    public DbSet<AvaliableTime> AvaliableTimes {get; set;}
    public DbSet<Appointment> Appointments {get; set;}
    public DbSet<Process> Processes {get; set;}
    public DbSet<WorkerProcess> WorkerProcesses {get; set;}

    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) {
      
    }
  }
}