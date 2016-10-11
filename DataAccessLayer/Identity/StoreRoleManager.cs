using SoccerHighlightsStore.DataAccessLayer.ORM;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerHighlightsStore.DataAccessLayer.Identity
{
    public class StoreRoleManager : RoleManager<IdentityRole>
    {
        public StoreRoleManager(RoleStore<IdentityRole> store) : base(store) { }

        public static StoreRoleManager Create(
        IdentityFactoryOptions<StoreRoleManager> options,
        IOwinContext context)
        {
            return new StoreRoleManager(new
            RoleStore<IdentityRole>(context.Get<SoccerVideoDbContext>()));
        }
    }
}
