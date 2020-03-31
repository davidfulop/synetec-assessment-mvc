using System.Web.Mvc;
using InterviewTestTemplatev2.ControllerServices;
using InterviewTestTemplatev2.Models;

namespace InterviewTestTemplatev2.Controllers
{
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolIndexService _indexService;
        private readonly IBonusPoolCalculatorService _calculatorService;

        public BonusPoolController(IBonusPoolIndexService indexService, IBonusPoolCalculatorService calculatorService)
        {
            _indexService = indexService;
            _calculatorService = calculatorService;
        }

        public ActionResult Index()
        {
            var model = _indexService.GenerateIndexModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(BonusPoolCalculatorModel model)
        {
            var resultModel = _calculatorService.CalculateBonusForEmployee(model);
            return View(resultModel);
        }
    }
}
