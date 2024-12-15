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

    public async Task<Appointment> GetByIdAsync(int id)
    {
      return await _context.Set<Appointment>().FindAsync(id);
    }

    public async Task DeleteAsync(Appointment appointment)
    {
      _context.Set<Appointment>().Remove(appointment);
      await _context.SaveChangesAsync();
    }

    public void Create(Appointment appointment)
    {
      _context.Appointments.Add(appointment);
      _context.SaveChanges();
    }

    public bool SaveAppointment(int avaliableTimeId, int customerId)
    {
      var avaliableTime = _context.AvaliableTimes.FirstOrDefault(a => a.Id == avaliableTimeId);

      if (avaliableTime == null || avaliableTime.IsAvaliable == 0)
      {
        return false;
      }

      var appointment = new Appointment
      {
        AvaliableTimeId = avaliableTimeId,
        CustomerId = customerId
      };

      _context.Appointments.Add(appointment);

      avaliableTime.IsAvaliable = 0;
      _context.SaveChanges();

      return true;
    }

    public List<AppointmentDetailDto> GetAppointmentsByCustomer(int customerId)
    {
      var appointments = (from a in _context.Set<Appointment>()
                          join at in _context.Set<AvaliableTime>() on a.AvaliableTimeId equals at.Id
                          join wp in _context.Set<WorkerProcess>() on at.WorkerProcessId equals wp.Id
                          join w in _context.Set<Worker>() on wp.WorkerId equals w.Id
                          join p in _context.Set<Process>() on wp.ProcessId equals p.Id
                          where a.CustomerId == customerId
                          select new AppointmentDetailDto
                          {
                            AppointmentId = a.Id,
                            ProcessName = p.Name,
                            WorkerFullName = w.Name + " " + w.Surname,
                            Date = at.Time,
                            EstablishedTime = p.Time,
                            Price = p.Price
                          }).ToList();

      return appointments;
    }
  }
}