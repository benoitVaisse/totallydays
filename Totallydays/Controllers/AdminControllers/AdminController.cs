using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.Repositories;

namespace Totallydays.Controllers.AdminControllers
{
    [Authorize(Roles = "admin")]
    [Route("admin")]
    public class AdminController : MyController
    {
        
    }
}