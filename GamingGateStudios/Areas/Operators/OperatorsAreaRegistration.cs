using System.Web.Mvc;

namespace GamingGateStudios.Areas.Operators
{
    public class OperatorsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Operators";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Operators_default",
                "Operators/{controller}/{action}/{id}",
                new { controller="OGames",action = "SignIn", id = UrlParameter.Optional }
            );
        }
    }
}