using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        private readonly ILogger<EmployeesClient> _Logger;

        public EmployeesClient(IConfiguration config, ILogger<EmployeesClient> Logger) : base (config, WebApi.Employees)
        {
            _Logger = Logger;
        }

        public IEnumerable<Employee> Get()
        {
            _Logger.LogInformation("Клиент. Получение списка сотрудников.");
            return Get<IEnumerable<Employee>>(Address);
        }

        public Employee Get(int id)
        {
            _Logger.LogInformation("Клиент. Получение сотрудника с ID {0}.", id);
            return Get<Employee>($"{Address}/{id}");
        }

        public Employee GetByName(string LastName, string FirstName, string Patronymic)
        {
            _Logger.LogInformation("Клиент. Получение сотрудника: {0} {1} {2}.", LastName, FirstName, Patronymic);
            return Get<Employee>($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}");
        }

        public int Add(Employee employee)
        {
            _Logger.LogInformation("Клиент. Получение сотрудника: {0}.", employee);
            return Post(Address, employee).Content.ReadAsAsync<int>().Result;
        }

        public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            _Logger.LogInformation("Клиент. Добавление сотрудника: {0} {1} {2}, возраст {3}.", LastName, FirstName, Patronymic, Age);
            return Post($"{Address}/employee?LastName={LastName}&FirstName={FirstName}&Patronymic={Patronymic}", "").Content.ReadAsAsync<Employee>().Result;
        }

        public void Update(Employee employee)
        {
            _Logger.LogInformation("Клиент. Изменение данных сотрудника: {0}.", employee);
            Put(Address, employee);
        }

        public bool Delete(int id)
        {
            _Logger.LogInformation("Клиент. Удаление сотрудника с ID {0}.", id);
            return Delete($"{Address}/{id}").IsSuccessStatusCode;
        }
    }
}
