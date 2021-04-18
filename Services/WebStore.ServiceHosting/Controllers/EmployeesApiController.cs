using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IEmployeesData EmployeesData, ILogger<EmployeesApiController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            _Logger.LogInformation("Api. Получение списка сотрудников");
            return _EmployeesData.Get();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            _Logger.LogInformation("Api. Получение сотрудника c ID {0}", id);
            return _EmployeesData.Get(id);
        }

        [HttpPost("{employee}")]        // http://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Иван&Patronymic=Иванович
        public Employee GetByName(string LastName, string FirstName, string Patronymic)
        {
            _Logger.LogInformation("Api. Получение сотрудника с именем {0} {1} {2}", LastName, FirstName, Patronymic);
            return _EmployeesData.GetByName(LastName, FirstName, Patronymic);
        }

        [HttpPost]
        public int Add(Employee employee)
        {
            _Logger.LogInformation("Api. Добавление сотрудника: {0}", employee);
            return _EmployeesData.Add(employee);
        }

        [HttpPost("employee")]        // http://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Иван&Patronymic=Иванович&Age=25
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            _Logger.LogInformation("Api. Получение сотрудника с именем {0} {1} {2}", LastName, FirstName, Patronymic);
            return _EmployeesData.Add(LastName, FirstName, Patronymic, Age);
        }

        [HttpPut]
        public void Update(Employee employee)
        {
            _Logger.LogInformation("Api. Изменение данных сотрудника: {0}", employee);
            _EmployeesData.Update(employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _Logger.LogInformation("Api. Удаление сотрудника c ID {0}", id);
            return _EmployeesData.Delete(id);
        }
    }
}
