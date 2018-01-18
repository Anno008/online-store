using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Backend.WebApi.Validation
{
    public class ModelStateValidatorConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controllerModel in application.Controllers)
            {
                controllerModel.Filters.Add(new ModelStateValidationFilterAttribute());
            }
        }
    }
}
