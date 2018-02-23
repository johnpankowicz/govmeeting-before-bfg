using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using WebApp.Features.Shared;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

// Todo-g #### Change all namespace names from "Models", "Controllers", etc to feature name.
namespace WebApp.Features.Fixasr
{
    [Route("api/[controller]")]
    public class FixasrController : Controller
    {
        private readonly IAuthorizationService authz;
        private readonly IHostingEnvironment env;

        public IFixasrRepository fixasr { get; set; }

        public FixasrController(IAuthorizationService _authz, IFixasrRepository _fixasr, IHostingEnvironment _env)
        {
            authz = _authz;
            fixasr = _fixasr;
            env = _env;
        }

        // We haven't yet implemented the calls on the client side to provide us the arguments that are hard-coded below.

        // GET: api/fixasr
        [HttpGet]
        public FixasrView Get()
        {
            FixasrView ret = fixasr.Get("johnpank", "USA", "ME", "LincolnCounty", "BoothbayHarbor", "Selectmen", "en", "2017-02-15", 1);
            return ret;
        }

        // POST api/fixasr
        [Authorize(Policy = "Proofreader")]
        [HttpPost]
        public void Post([FromBody]FixasrView value)
        {
            bool success = fixasr.Put(value, "johnpank", "USA", "ME", "LincolnCounty", "BoothbayHarbor", "Selectmen", "en", "2017-02-15", 1);
        }
    }
}
