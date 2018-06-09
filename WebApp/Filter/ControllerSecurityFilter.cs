using BL.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Filter
{
    public class ControllerSecurityFilter : ActionFilterAttribute
    {
        private UserManager<ApplicationUser> _userManager;

        public ControllerSecurityFilter(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userId = _userManager.GetUserId(context.HttpContext.User);
            if (userId != null)
            {
                AuthUtil.SetUser(userId);
            }
        }
    }
}
