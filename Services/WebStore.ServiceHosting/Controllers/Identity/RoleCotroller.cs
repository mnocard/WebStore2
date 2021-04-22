using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebApi.Identity.Roles)]
    [ApiController]
    public class RoleCotroller : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;
        public RoleCotroller(WebStoreDB Db)
        {
            _RoleStore = new RoleStore<Role>(Db); 
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Role>> GetAllUsersAsync() => await _RoleStore.Roles.ToArrayAsync();
    }
}
