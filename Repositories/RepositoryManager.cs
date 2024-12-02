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

    public RepositoryManager(RepositoryContext context, IAdminRepository adminRepository, ICustomerRepository customerRepository, IWorkerRepository workerRepository, IProfessionRepository professionRepository, IAvaliableTimeRepository avaliableTimeRepository, IAppointmentRepository appointmentRepository)
    {
      _context = context;
      _adminRepository = adminRepository;
      _customerRepository = customerRepository;
      _workerRepository = workerRepository;
      _professionRepository = professionRepository;
      _avaliableTimeRepository = avaliableTimeRepository;
      _appointmentRepository = appointmentRepository;
    }

    public IAdminRepository Admin => _adminRepository;

    public ICustomerRepository Customer => _customerRepository;

    public IWorkerRepository Worker => _workerRepository;

    public IProfessionRepository Profession => _professionRepository;

    public IAvaliableTimeRepository AvaliableTime => _avaliableTimeRepository;

    public IAppointmentRepository Appointment => _appointmentRepository;

    public void Save()
    {
      _context.SaveChanges();
    }
  }
}