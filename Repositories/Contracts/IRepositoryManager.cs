namespace Repositories.Contracts
{
  public interface IRepositoryManager
  {
    IAdminRepository Admin { get; }
    ICustomerRepository Customer { get; }
    IWorkerRepository Worker { get; }
    IProfessionRepository Profession { get; }
    IAvaliableTimeRepository AvaliableTime { get; }
    IAppointmentRepository Appointment { get; }

    void Save();
  }
}