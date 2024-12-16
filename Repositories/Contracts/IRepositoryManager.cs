namespace Repositories.Contracts
{
  public interface IRepositoryManager
  {
    ICustomerRepository Customer { get; }
    IWorkerRepository Worker { get; }
    IProfessionRepository Profession { get; }
    IAvaliableTimeRepository AvaliableTime { get; }
    IAppointmentRepository Appointment { get; }
    IProcessRepository Process { get; }
    IWorkerProcessRepository WorkerProcess { get; }

    void Save();
  }
}