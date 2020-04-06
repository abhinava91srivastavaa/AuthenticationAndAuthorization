using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AuthenticationAndAuthorization.Models;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;


namespace AuthenticationAndAuthorization.Controllers
{
    public class LogONController : Controller
    {
        private AuthandAuthorizationEntities db = new AuthandAuthorizationEntities();
        // GET: LogON
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIN(string returnUrl = "")
        {

            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIN([Bind(Include ="EmailId,Password,UserType")] SignIn model, string returnUrl = "")
        {
           
            //Get the culture property of the thread.
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            //Create TextInfo object.
            TextInfo textInfo = cultureInfo.TextInfo;
            if (ModelState.IsValid)
            {
                if (model.EmailID != null)
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Abhinava"].ConnectionString;
                    //create new sqlconnection and connection to database by using connection string from web.config file  
                    SqlConnection con = new SqlConnection(strcon);
                    con.Open();
                    Session["UserEmail"] = model.EmailID.ToString();
                    FormsAuthentication.SetAuthCookie(model.EmailID, true);
                    string userPassword = model.Password.ToString();
                    DataSet ds = LogINDAL.get_loginDetails(model.EmailID, Convert.ToInt32(model.UserType));
                    // var ds = (as as db.UserprofileandRegs.Where(x => x.UserType == Convert.ToInt32(model.UserType) && x.EmailId == model.EmailID));
                    con.Close();
                    if (ds != null)
                    {
                        if ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0))
                        {
                            string userID = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
                            string Userpwd = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                            string UserType = Convert.ToString(ds.Tables[0].Rows[0]["UserType"]);
                            string UserName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);

                            Session["UserID"] = userID;
                            Session["UserType"] = UserType;
                            Session["UserName"] = textInfo.ToTitleCase(UserName);
                            if (userPassword == Userpwd)
                            {
                                if (model.UserType == UserType)
                                {
                                    if (model.UserType == "1")
                                    { return RedirectToAction("Index", "UserprofileandRegs"); }
                                    else if (model.UserType == "2")
                                    { return RedirectToAction("Index", "Supervisor"); }
                                    else if (model.UserType == "3")
                                    { return RedirectToAction("Index", "Agent"); }

                                }
                                else { TempData["Message"] = "Invalid User Type!!"; }
                            }
                            else { TempData["Message"] = "Invalid Password!!"; }
                        }
                        else { TempData["Message"] = "Invalid Username!!"; }
                    }
                    else { TempData["Message"] = "Invalid Username!!"; }
                }
                else { TempData["Message"] = "Invalid Username!!"; }
            }
            else { TempData["Message"] = "Invalid Username OR Password!!"; }

            return RedirectToAction("SignIN");
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session["UserID"] = null;
            Session["UserType"] = 0;
            Session["UserName"] = null;
            return RedirectToAction("SignIN", "LogON", null);
        }
    }
}