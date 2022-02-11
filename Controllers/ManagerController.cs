﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class ManagerController : Controller
    {
        [Authorize(Roles = "manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
