using Entities.Models;
using Entities.ViewModels;

namespace Services.Contracts
{
  public interface IAppointmentService
  {
    IEnumerable<Appointment> GetAppointments(bool trackChanges);
    Appointment? GetAppointment(int id, bool trackChanges);
    void AddAppointment(Appointment appointment);
    void Delete(int AppointmentId);
    void UpdateAppointment(Appointment appointment);
    bool SaveAppointment(int avaliableTimeId, int customerId);
    List<AppointmentDetailDto> GetAppointmentsByCustomer(int customerId);
    List<Appointment> GetAppointmentsByCustomerId(int customerId);
    IEnumerable<AppointmentDto> GetAppointmentDetails();
    bool DeleteAppointment(int appointmentId);
  }
}