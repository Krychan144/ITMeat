using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using ITMeat.BusinessLogic.Configuration.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITMeat.WEB.Controllers
{
    [Route("[controller]")]
    public class SeedController : BaseController
    {
        private readonly IDbContextSeeder _dbContextSeeder;

        public SeedController(IDbContextSeeder dbContextSeeder)
        {
            _dbContextSeeder = dbContextSeeder;
        }

        [HttpGet("seedall")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            _dbContextSeeder.Seed();
            return Json("Database seeded");
        }
    }
}