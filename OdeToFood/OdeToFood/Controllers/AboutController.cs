using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{

    [Route("About")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "12345-0987-12";
        }

        [Route("Adress")]
        public string Adress()
        {
            return "Brazil";
        }
    }
}
