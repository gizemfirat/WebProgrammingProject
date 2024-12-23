using Entities.Models;
using Entities.ViewModels;

namespace Repositories.Contracts
{
  public interface IAppointmentRepository : IRepositoryBase<Appointment>
  {
    IQueryable<Appointment> GetAllAppointments(bool trackChanges);
    Appointment? GetAppointment(int id, bool trackChanges);
    void AddAppointment(Appointment appointment);
    void DeleteAppointment(Appointment appointment);
    void UpdateAppointment(Appointment appointment);
    Task<Appointment> GetByIdAsync(int id);
    Task DeleteAsync(Appointment appointment);
    void Create(Appointment appointment);
    bool SaveAppointment(int avaliableTimeId, int customerId);
    List<AppointmentDetailDto> GetAppointmentsByCustomer(int customerId);
    List<Appointment> GetAppointmentsByCustomerId(int customerId);
    IEnumerable<AppointmentDto> GetAppointmentDetails();
    List<AppointmentForWorkerDto> GetAppointmentForWorker(int workerId); 
  }
}