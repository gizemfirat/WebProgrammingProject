using Entities.Models;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<AppointmentViewModel> GetAppointmentByCustomerId(int customerId)
        {
          var appointments = from a in _context.Appointments
                            join w in _context.Workers on a.Worker equals w.Id
                            join p in _context.Professions on w.Profession equals p.Id
                            join at in _context.AvaliableTimes on a.Date equals at.Id
                            where a.Customer == customerId
                            select new AppointmentViewModel
                            {
                              AppointmentId = a.Id,
                              Date = at.Time,
                              WorkerName = w.Name + " " + w.Surname,
                              ProfessionName = p.Name,
                              Price = p.Price
                            };
                            
                            return appointments.ToList();
        }

    }
}