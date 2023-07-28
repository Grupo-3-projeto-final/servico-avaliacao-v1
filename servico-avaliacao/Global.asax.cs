using servico_avaliacao.Application.Services;
using servico_avaliacao.ServicoSQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace servico_avaliacao
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static SqsConsumerService _sqsConsumer;

        protected void Application_Start()
        {
           
            _sqsConsumer = new SqsConsumerService();
            _sqsConsumer.StartListeningAsync();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           // RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
