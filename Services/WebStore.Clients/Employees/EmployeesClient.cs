using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration config) : base (config, WebApi.Employees) { }

        public IEnumerable<Employee> Get()
        {

        }
        public Employee Get(int id) => throw new System.NotImplementedException();
        public Employee GetByName(string LastName, string FirstName, string Patronymic) => throw new System.NotImplementedException();
        public int Add(Employee employee) => throw new System.NotImplementedException();
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) => throw new System.NotImplementedException();
        public void Update(Employee employee) => throw new System.NotImplementedException();
        public bool Delete(int id) => throw new System.NotImplementedException();
    }
}
