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
    [Route(WebApi.Identity.Users)]
    [ApiController]
    public class UsersCotroller : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreDB> _UserStore;

        public UsersCotroller(WebStoreDB Db)
        {
            _UserStore = new UserStore<User, Role, WebStoreDB>(Db);
        }

        [HttpGet("all")]
        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _UserStore.Users.ToArrayAsync();

    }
}
