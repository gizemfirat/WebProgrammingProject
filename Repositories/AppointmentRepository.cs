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

      var custumerAppointments = _context.Appointments
                                         .Where(a => a.CustomerId == customerId)
                                         .Join(
                                          _context.AvaliableTimes,
                                          appointment => appointment.AvaliableTimeId,
                                          time => time.Id,
                                          (appointment, time) => time
                                         ).ToList();

      bool isConflict = custumerAppointments.Any(existingTime =>
           avaliableTime.Time < existingTime.EndTime && avaliableTime.EndTime > existingTime.Time);

      if (isConflict)
      {
        return false;
      }

      if (avaliableTime.Time < DateTime.Now)
      {
        throw new InvalidOperationException("You cannot make an appointment for a past date.");
      }

      var appointment = new Appointment
      {
        AvaliableTimeId = avaliableTimeId,
        CustomerId = customerId,
        IsApproved = 0
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
                            WorkerProcessId = wp.Id,
                            AvaliableTimeId = at.Id,
                            IsAvaliable = at.IsAvaliable,
                            Date = at.Time,
                            EndTime = at.EndTime,
                            EstablishedTime = p.Time,
                            Price = p.Price,
                            IsApproved = a.IsApproved
                          }).ToList();

      return appointments;
    }

    public List<Appointment> GetAppointmentsByCustomerId(int customerId)
    {
      return _context.Appointments.Where(a => a.CustomerId == customerId).ToList();
    }

    public IEnumerable<AppointmentDto> GetAppointmentDetails()
    {
      var appointmentDetails = from appointment in _context.Appointments
                               join time in _context.AvaliableTimes on appointment.AvaliableTimeId equals time.Id
                               join workerProcess in _context.WorkerProcesses on time.WorkerProcessId equals workerProcess.Id
                               join worker in _context.Workers on workerProcess.WorkerId equals worker.Id
                               join process in _context.Processes on workerProcess.ProcessId equals process.Id
                               join customer in _context.Customers on appointment.CustomerId equals customer.Id
                               select new AppointmentDto
                               {
                                 AppointmentId = appointment.Id,
                                 ProcessName = process.Name,
                                 WorkerFullName = $"{worker.Name} {worker.Surname}",
                                 WorkerProcessId = workerProcess.Id,
                                 AvaliableTimeId = time.Id,
                                 IsAvaliable = time.IsAvaliable,
                                 Date = time.Time,
                                 EndTime = time.EndTime,
                                 EstablishedTime = process.Time,
                                 Price = process.Price,
                                 CustomerFullName = $"{customer.Name} {customer.Surname}"
                               };

      return appointmentDetails.ToList();
    }

    public List<AppointmentForWorkerDto> GetAppointmentForWorker(int workerId)
    {
      var appointmentDetails = (from appointment in _context.Appointments
                                join time in _context.AvaliableTimes on appointment.AvaliableTimeId equals time.Id
                                join workerProcess in _context.WorkerProcesses on time.WorkerProcessId equals workerProcess.Id
                                join process in _context.Processes on workerProcess.ProcessId equals process.Id
                                join customer in _context.Customers on appointment.CustomerId equals customer.Id
                                where workerProcess.WorkerId == workerId
                                select new AppointmentForWorkerDto
                                {
                                  AppointmentId = appointment.Id,
                                  AvaliableTimeId = time.Id,
                                  ProcessName = process.Name,
                                  CustomerFullName = $"{customer.Name} {customer.Surname}",
                                  Time = time.Time,
                                  EndTime = time.EndTime,
                                  IsApproved = appointment.IsApproved,
                                  WorkerProcessId = workerProcess.Id
                                }).ToList();

      return appointmentDetails;
    }
  }
}