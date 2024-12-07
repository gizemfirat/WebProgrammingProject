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
      if(appointment is null) {
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
            if(appointment != null) {
              var avaliableTime = _manager.AvaliableTime.GetAvaliableTime(appointment.Date, false);
              if(avaliableTime != null) {
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

        public IEnumerable<AppointmentViewModel> GetAppointmentsByCustomerId(int customerId)
        {
          return _manager.Appointment.GetAppointmentByCustomerId(customerId);
        }

        public async Task<bool> DeleteAppointmentAysnc(int appointmentId)
        {
            var appointment = await _manager.Appointment.GetByIdAsync(appointmentId);
            if(appointment == null) {
              return false;
            }

            var avaliableTime = await _manager.AvaliableTime.GetByIdAsync(appointment.Date);
            if(avaliableTime != null) {
              avaliableTime.IsAvaliable = 1;
              await _manager.AvaliableTime.UpdateAsync(avaliableTime);
            }

            await _manager.Appointment.DeleteAsync(appointment);
            return true;
        }

        public bool BookAppointment(int customerId, int workerId, int dateId)
        {
          var avaliableTime = _manager.AvaliableTime.GetByIdAsync(dateId).Result;
          if(avaliableTime == null || avaliableTime.IsAvaliable == 0) {
            return false;
          }

          var appointment = new Appointment {
            Customer = customerId,
            Worker = workerId,
            Date = dateId
          };

          _manager.Appointment.Create(appointment);
          avaliableTime.IsAvaliable = 0;
          _manager.AvaliableTime.Update(avaliableTime);

          return true;
        }
    }
}