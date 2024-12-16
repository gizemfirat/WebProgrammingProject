namespace Services.Contracts
{
  public interface IServiceManager
  {
    ICustomerService CustomerService { get; }
    IWorkerService WorkerService { get; }
    IProfessionService ProfessionService { get; }
    IAvaliableTimeService AvaliableTimeService { get; }
    IAppointmentService AppointmentService { get; }
    IProcessService ProcessService { get; }
    IWorkerProcessService WorkerProcessService { get; }
  }
}