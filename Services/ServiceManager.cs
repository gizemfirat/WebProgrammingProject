using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
      private readonly IAdminService _adminService;
      private readonly ICustomerService _customerService;
      private readonly IWorkerService _workerService;
      private readonly IProfessionService _professionService;
      private readonly IAvaliableTimeService _avaliableTimeService;
      private readonly IAppointmentService _appointmentService;

        public ServiceManager(IAdminService adminService, ICustomerService customerService, IWorkerService workerService, IProfessionService professionService, IAvaliableTimeService avaliableTimeService, IAppointmentService appointmentService)
        {
            _adminService = adminService;
            _customerService = customerService;
            _workerService = workerService;
            _professionService = professionService;
            _avaliableTimeService = avaliableTimeService;
            _appointmentService = appointmentService;
        }

        public IAdminService AdminService => _adminService;
        public ICustomerService CustomerService => _customerService;
        public IWorkerService WorkerService => _workerService;
        public IProfessionService ProfessionService => _professionService;
        public IAvaliableTimeService AvaliableTimeService => _avaliableTimeService;
        public IAppointmentService AppointmentService => _appointmentService;

    }
}