using Entities.Models;

namespace Repositories.Contracts
{
  public interface IAppointmentRepository : IRepositoryBase<Appointment>
  {
    IQueryable<Appointment> GetAllAppointments(bool trackChanges);
    Appointment? GetAppointment(int id, bool trackChanges);
    void AddAppointment(Appointment appointment);
    void DeleteAppointment(Appointment appointment);
    void UpdateAppointment(Appointment appointment);
  }
}