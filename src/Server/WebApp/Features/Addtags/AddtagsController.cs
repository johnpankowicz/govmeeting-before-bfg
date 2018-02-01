using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Features.Shared;
using WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AddtagsController : Controller
    {
        private readonly IAuthorizationService _authz;

        // JP: ### Conversion to ASP.NET Core ###
        // JP: FromServices attribute is no longer valid on a property. I moved this DI service to constructor
        // [FromServices]
        public IAddtagsRepository addtags { get; set; }
        private readonly TypedOptions _options;

        public AddtagsController(IAuthorizationService authz, IAddtagsRepository addtags,
            IOptions<TypedOptions> optionsAccessor)
        {
            _authz = authz;
            this.addtags = addtags;
            _options = optionsAccessor.Value;
        }

        /*
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        */

        // GET: api/addtags
        [HttpGet]
        public Addtags Get()

        // public string Get()
        {
            // Normally Get() would receive the paramaters that we are passing below to addtags.Get(). For now, this is hard-coded.
            //Addtags ret = addtags.Get("johnpank", "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17");
            Addtags ret = addtags.Get("johnpank", "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2014-09-25");
            return ret;
        }

        // POST api/addtags
        [Authorize(Policy = "Editor")]
        [HttpPost]
        //public void Post([FromForm]string value)
        public void Post([FromBody]Addtags value)
        //public void Post(Addtags value)
        {
            //Todo-g Add authorization check that user's location matches that of the government entity.
            // We need to read the location from the user's claims.

            //addtags.Put("johnpank", "USA", "PA", "Philadelphia", "CityCouncil", "2016-03-17");
            //string path = @"USA_PA_Philadelphia_CityCouncil/2016-03-17\T3-ToBeTagged.pdf";
            //addtags.PutByPath(System.IO.Path.Combine(Common.getDataPath(), path), value);
            addtags.Put(value, "johnpank", "USA", "PA", "Philadelphia", "Philadelphia", "CityCouncil", "en", "2016-03-17");
        }
    }
}
