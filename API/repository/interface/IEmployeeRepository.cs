using API.ViewModel;

namespace API.Repository.Interface
{
    public interface IEmployeeRepository
    {
    IEnumerable<Employee> Get(); // read semua data (select * from)       cocok powerfull untuk get, better daripada IList)

    Employee Get(string NIK); //read sesuai NIK nya aja  (select * from .. where)
    int Insert(Employee employee); //create
    int Update(Employee employee); //update
    int Delete(string NIK); //delete

    int Register(viewModel register);

    IEnumerable<GetEmployeeVm> GetAll(); //menampilkan semua data pada get employee

    GetEmployeeVm getEmploy(string NIK); //menampilkan data Get Employee sesuai dengan NIK yang dimasukkan
    }
}
