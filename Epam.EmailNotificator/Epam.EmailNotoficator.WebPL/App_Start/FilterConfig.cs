﻿using System.Web;
using System.Web.Mvc;

namespace Epam.EmailNotoficator.WebPL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}