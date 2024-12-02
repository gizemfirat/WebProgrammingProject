using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;

namespace Services
{
  public class AppointmentManager : IAppointmentService
  {
    private readonly IRepositoryManager _manager;

    public AppointmentManager(IRepositoryManager manager)
    {
      _manager = manager;
    }
    public IEnumerable<Appointment> GetAppointments(bool trackChanges)
    {
      return _manager.Appointment.GetAllAppointments(trackChanges);
    }
    public Appointment? GetAppointment(int id, bool trackChanges)
    {
      var appointment = _manager.Appointment.GetAppointment(id, trackChanges);
      if(appointment is null) {
        throw new Exception("Appointment not found");
      }

      return appointment;
    }

     public void AddAppointment(Appointment appointment)
        {
            _manager.Appointment.AddAppointment(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            _manager.Appointment.DeleteAppointment(appointment);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            _manager.Appointment.UpdateAppointment(appointment);
        }
  }
}