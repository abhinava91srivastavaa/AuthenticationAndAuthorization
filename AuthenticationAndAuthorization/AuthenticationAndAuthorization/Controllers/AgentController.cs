using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationAndAuthorization;

namespace AuthenticationAndAuthorization.Controllers
{
    public class AgentController : Controller
    {
        private AuthandAuthorizationEntities db = new AuthandAuthorizationEntities();

        // GET: Agent
        public ActionResult Index()
        {
            //int agentId = Convert.ToInt32(Session[" "]);
            int agentId = 4;
            return View(db.UserprofileandRegs.Where(x=>x.UserId==agentId));
        }

        // GET: Agent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserprofileandReg userprofileandReg = db.UserprofileandRegs.Find(id);
            if (userprofileandReg == null)
            {
                return HttpNotFound();
            }
            return View(userprofileandReg);
        }
        
  
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
