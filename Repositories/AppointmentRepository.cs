using Entities.Models;
using Repositories.Contracts;

namespace Repositories
{
  public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
  {
    public AppointmentRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Appointment> GetAllAppointments(bool trackChanges) => GetAll(trackChanges);

    public Appointment? GetAppointment(int id, bool trackChanges)
    {
      return Get(a => a.Id.Equals(id), trackChanges);
    }

    public void AddAppointment(Appointment appointment)
    {
      Add(appointment);
      _context.SaveChanges();
    }

    public void DeleteAppointment(Appointment appointment)
    {
      Delete(appointment);
      _context.SaveChanges();
    }

    public void UpdateAppointment(Appointment appointment)
    {
      Update(appointment);
      _context.SaveChanges();
    }
  }
}