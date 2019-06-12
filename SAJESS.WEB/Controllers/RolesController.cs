using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SAJESS.WEB.Models;

namespace SAJESS.WEB.Controllers
{
    [Authorize(Roles = "sa")]
    public class RolesController : Controller
    {
        ApplicationDbContext context;

        public RolesController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Role
        public ActionResult Index()
        {
            var roles = context.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            var roles = new IdentityRole();
            return View(roles);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole roles)
        {
            context.Roles.Add(roles);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}