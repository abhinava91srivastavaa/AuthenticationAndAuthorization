using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationAndAuthorization;
using System.Data.SqlClient;
using System.Configuration;

namespace AuthenticationAndAuthorization.Controllers
{
    public class UserprofileandRegsController : Controller
    {
        private AuthandAuthorizationEntities db = new AuthandAuthorizationEntities();

        // GET: UserprofileandRegs
        public ActionResult Index()
        {
            return View(db.UserprofileandRegs.ToList());
        }

        // GET: UserprofileandRegs/Details/5
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

        // GET: UserprofileandRegs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserprofileandRegs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,EmailId,Password,PhoneNo,CommunicationAddress,Status")] UserprofileandReg userprofileandReg)
        {
            if (ModelState.IsValid)
            {
                db.UserprofileandRegs.Add(userprofileandReg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofileandReg);
        }

        // GET: UserprofileandRegs/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: UserprofileandRegs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,EmailId,Password,PhoneNo,CommunicationAddress,Status")] UserprofileandReg userprofileandReg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofileandReg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofileandReg);
        }

        // GET: UserprofileandRegs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: UserprofileandRegs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserprofileandReg userprofileandReg = db.UserprofileandRegs.Find(id);
            db.UserprofileandRegs.Remove(userprofileandReg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[8] { new DataColumn("FirstName", typeof(string)),
                                new DataColumn("LastName", typeof(string)),
                                new DataColumn("EmailId",typeof(string)),
                                new DataColumn("Password",typeof(string)),
                                new DataColumn("PhoneNo",typeof(string)),
                                new DataColumn("CommunicationAddress",typeof(string)),
                                new DataColumn("Status",typeof(string)),
                                new DataColumn("UserType",typeof(string))
                });


                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            if (cell.Contains("\r"))
                            {
                               var cell1 = cell.Split('\r')[0];
                                dt.Rows[dt.Rows.Count - 1][i] = cell1;

                            }
                            else
                            { dt.Rows[dt.Rows.Count - 1][i] = cell; }
                            
                            i++;
                        }
                    }
                }

                string conString = ConfigurationManager.ConnectionStrings["Abhinava"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.UserprofileandReg";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                        sqlBulkCopy.ColumnMappings.Add("LastName", "LastName");
                        sqlBulkCopy.ColumnMappings.Add("EmailId", "EmailId");
                        sqlBulkCopy.ColumnMappings.Add("Password", "Password");
                        sqlBulkCopy.ColumnMappings.Add("PhoneNo", "PhoneNo");
                        sqlBulkCopy.ColumnMappings.Add("CommunicationAddress", "CommunicationAddress");
                        sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                        sqlBulkCopy.ColumnMappings.Add("UserType", "UserType");

                        con.Open();
                        dt.Rows.Remove(dt.Rows[0]);
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return View();
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
