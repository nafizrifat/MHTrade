using SAJESS.WEB;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SAJESS.WEB.Models;

[assembly: OwinStartup(typeof(Startup))]
namespace SAJESS.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // creating first Admin Role and creating a default Admin User 
            if (!roleManager.RoleExists("sa"))
            {

                // create Admin rool
                var role = new IdentityRole();
                role.Name = "sa";
                roleManager.Create(role);

                //create a Admin super user who will maintain the website				

                var user = new ApplicationUser();
                user.UserName = "admin";
                string userPWD = "sj123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "sa");

                }
            }
            if (!roleManager.RoleExists("admin"))
            {
                var role = new IdentityRole();
                role.Name = "admin";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("user"))
            {
                var role = new IdentityRole();
                role.Name = "user";
                roleManager.Create(role);

            }
            if (!roleManager.RoleExists("accountant"))
            {
                var role = new IdentityRole();
                role.Name = "accountant";
                roleManager.Create(role);

            }

        }
    }
}
