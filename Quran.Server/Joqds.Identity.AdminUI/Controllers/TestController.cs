using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Joqds.Identity.AdminUI.Controllers
{
    public class TestController : AdminUiBaseController
    {

        public IActionResult GetUser()
        {
            var user = User;
            return Ok(user.Claims.Select(x=>new Tuple<string,string>(x.Type,x.Value)));
        }
    }
}