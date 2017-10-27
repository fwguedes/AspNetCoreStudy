using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public interface IGreeter
    {
        string Greet();
    }

    public class Greeter : IGreeter
    {
        public string greeting { get; set; }

        public Greeter(IConfiguration configuration)
        {
            greeting = configuration["Greeting"];

        }
        public string Greet()
        {
            return greeting;
        }
    }
}
