using Services.Contracts;
using Entities.Models;
using Repositories.Contracts;
using Entities.ViewModels;

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
      if (appointment is null)
      {
        throw new Exception("Appointment not found");
      }

      return appointment;
    }

    public void AddAppointment(Appointment appointment)
    {
      _manager.Appointment.AddAppointment(appointment);
    }

    public void DeleteAppointment(int appointmentId)
    {
      var appointment = _manager.Appointment.GetAppointment(appointmentId, false);
      if (appointment != null)
      {
        var avaliableTime = _manager.AvaliableTime.GetAvaliableTime(appointment.AvaliableTimeId, false);
        if (avaliableTime != null)
        {
          avaliableTime.IsAvaliable = 1;
          _manager.AvaliableTime.UpdateAvaliableTime(avaliableTime);
        }

        _manager.Appointment.DeleteAppointment(appointment);
      }
    }

    public void UpdateAppointment(Appointment appointment)
    {
      _manager.Appointment.UpdateAppointment(appointment);
    }

    public bool SaveAppointment(int avaliableTimeId, int customerId)
    {
      var result = _manager.Appointment.SaveAppointment(avaliableTimeId, customerId);

      if (!result)
      {
        return false;
      }

      return true;
    }

    public List<AppointmentDetailDto> GetAppointmentsByCustomer(int customerId)
    {
      return _manager.Appointment.GetAppointmentsByCustomer(customerId);
    }
  }
}