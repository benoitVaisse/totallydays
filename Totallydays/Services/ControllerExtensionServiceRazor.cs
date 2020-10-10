using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Totallydays.Services
{
    public class ControllerExtenstionServiceRazor
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ControllerExtenstionServiceRazor(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            this._razorViewEngine = razorViewEngine;
            this._tempDataProvider = tempDataProvider;
            this._serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Render a partial view to string.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="controller"></param>
        /// <param name="viewNamePath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(viewName, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var dictionnary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };
                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    dictionnary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        /// <summary>
        /// Render a partial view to string, without a model present.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="controller"></param>
        /// <param name="viewNamePath"></param>
        /// <returns></returns>
        public async Task<string> RenderViewToStringAsync(string viewName)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(viewName, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }


                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()),
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

    }
}
