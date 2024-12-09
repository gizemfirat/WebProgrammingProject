using Repositories.Contracts;

namespace Repositories
{
  public class RepositoryManager : IRepositoryManager
  {

    private readonly RepositoryContext _context;
    private readonly IAdminRepository _adminRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IProfessionRepository _professionRepository;
    private readonly IAvaliableTimeRepository _avaliableTimeRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IProcessRepository _processRepository;
    private readonly IWorkerProcessRepository _workerProcessRepository;

    public RepositoryManager(RepositoryContext context, IAdminRepository adminRepository, ICustomerRepository customerRepository, IWorkerRepository workerRepository, IProfessionRepository professionRepository, IAvaliableTimeRepository avaliableTimeRepository, IAppointmentRepository appointmentRepository, IProcessRepository processRepository, IWorkerProcessRepository workerProcessRepository)
    {
      _context = context;
      _adminRepository = adminRepository;
      _customerRepository = customerRepository;
      _workerRepository = workerRepository;
      _professionRepository = professionRepository;
      _avaliableTimeRepository = avaliableTimeRepository;
      _appointmentRepository = appointmentRepository;
      _processRepository = processRepository;
      _workerProcessRepository = workerProcessRepository;
    }

    public IAdminRepository Admin => _adminRepository;

    public ICustomerRepository Customer => _customerRepository;

    public IWorkerRepository Worker => _workerRepository;

    public IProfessionRepository Profession => _professionRepository;

    public IAvaliableTimeRepository AvaliableTime => _avaliableTimeRepository;

    public IAppointmentRepository Appointment => _appointmentRepository;

    public IProcessRepository Process => _processRepository;
    public IWorkerProcessRepository WorkerProcess => _workerProcessRepository;

        public void Save()
    {
      _context.SaveChanges();
    }
  }
}