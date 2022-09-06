using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Web;
using System.Web.Mvc;

namespace Risen
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
        
    }
}
