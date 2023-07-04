﻿namespace PrintingHouse.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Core.Constants.RoleNamesConstants;

    [Area("Admin")]
    [Authorize(Roles = Admin)]
    public class BaseController : Controller
    {
       
    }
}