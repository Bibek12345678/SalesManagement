using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Services
{
    public class UtilityServices : IUtilityServices
    {
        IConfiguration config;
        public UtilityServices(IConfiguration config)
        {
            this.config = config;
        }
        public string ConnectionString
        {
            get
            {
                return config.GetConnectionString("DefaultConnection");
            }
        }
    }
}


