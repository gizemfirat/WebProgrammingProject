using Entities.Models;
using Entities.ViewModels;

namespace Services.Contracts
{
  public interface IAppointmentService
  {
    IEnumerable<Appointment> GetAppointments(bool trackChanges);
    Appointment? GetAppointment(int id, bool trackChanges);
    void AddAppointment(Appointment appointment);
    void DeleteAppointment(Appointment appointment);
    void UpdateAppointment(Appointment appointment);
    IEnumerable<AppointmentViewModel> GetAppointmentsByCustomerId(int customerId);
  }
}