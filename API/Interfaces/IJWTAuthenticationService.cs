using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    interface IJWTAuthenticationService
    {
        string CreateJWTToken(User user);
    }
}
