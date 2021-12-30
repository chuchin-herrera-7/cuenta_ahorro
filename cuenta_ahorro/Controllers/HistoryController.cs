using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Helpers;
using cuenta_ahorro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Rotativa.AspNetCore;

namespace cuenta_ahorro.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HistoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index(string id)
        {
            return View(Get(id));
        }

        public ActionResult Report_(string id)
        {
            return PartialView("Report", Get(id));
        }

        public ActionResult Report(string id)
        {
            return new ViewAsPdf(Get(id));
        }

        private History Get(string id)
        {
            var _openingSavingAccount = new OpeningSavingAccountHelper(_dbContext).GetBy(id).Result.Value;
            var _client = new ClientHelper(_dbContext).GetBy(_openingSavingAccount.IdClient).Result.Value;
            var _managementAccount = new ManagementAccountHelper(_dbContext).Get(_openingSavingAccount.Id).Result.Value;
            var _sumDeposit = _managementAccount.Where(element => element.Type == 1).Sum(item => item.Amount);
            var _sumWithdraw = _managementAccount.Where(element => element.Type == 0).Sum(item => item.Amount);

            var _history = new History()
            {
                Deposit = _sumDeposit,
                WithDraw = _sumWithdraw,
                OpeningSavingAccount = _openingSavingAccount,
                Client = _client,
                ManagementAccount = _managementAccount
            };

            return _history;
        }
    }
}
