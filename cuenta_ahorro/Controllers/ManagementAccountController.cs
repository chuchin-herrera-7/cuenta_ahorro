using cuenta_ahorro.EF;
using cuenta_ahorro.EF.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace cuenta_ahorro.Controllers
{
    public class ManagementAccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagementAccountController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public ActionResult Index(int id)
        {
            return View(new ManagementAccountHelper(_dbContext).Get(id).Result.Value);
        }
    }
}
