using System.Web.Mvc;
using InterviewTestTemplatev2.ControllerServices;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolControllerService _controllerService;

        public BonusPoolController(IBonusPoolControllerService controllerService)
        {
            _controllerService = controllerService;
        }

        public ActionResult Index()
        {
            var model = _controllerService.GenerateIndexModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(BonusPoolCalculatorModel model)
        {
            var resultModel = _controllerService.CalculateBonusForEmployee(model);
            return View(resultModel);
        }
    }
}
