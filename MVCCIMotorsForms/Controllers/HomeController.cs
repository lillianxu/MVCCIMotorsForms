using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCIMotorsForms.Models;

namespace MVCCIMotorsForms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult StaffManagement()
        {


            IC_MotersEntities db = new IC_MotersEntities();

            #region SQO

            List<Models.StaffClass> staffList = db.People.Where(x => x.PersonTypeId != 4).Select(x => new StaffClass
            {
                StaffId = x.PersonId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address1 = x.Address1,
                Address2 = x.Address2
            }).ToList();

            //-------------------------
            //var staffList = db.People.Where(x => x.PersonTypeId != 4).Select(x => new StaffClass
            //{ StaffId = x.PersonId, FirstName = x.FirstName,
            //    LastName = x.LastName, Address1 = x.Address1,
            //    Address2 = x.Address2
            //}).ToList();
            #endregion



            return View(staffList);
        }

        public ActionResult CustomerManagement()
        {


            IC_MotersEntities db = new IC_MotersEntities();
            var customerList = db.People.Where(x => x.PersonTypeId == 4).Select(x => new StaffClass
            {
                StaffId = x.PersonId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address1 = x.Address1,
                Address2 = x.Address2
            }).ToList();
            return View(customerList);
        }

        // for search function
        public ActionResult SearchAct()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SearchAct(string nameToFind)
        {
            ViewBag.SearchKey = nameToFind;

            IC_MotersEntities db = new IC_MotersEntities();

            var selectedStaff1 = db.People.Where(x => x.LastName == nameToFind).Select(x => new StaffClass
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                StaffId = x.PersonId,
                Address1 = x.Address1,
                Address2 = x.Address2,
                Salary = x.Salary
            }).ToList();
            return View(selectedStaff1);
        }

        //for edit function
        public ActionResult EditStaffMember(int staffId)
        {
            IC_MotersEntities db = new IC_MotersEntities();

            var selectedStaff = db.People.Find(staffId);
            var staffToEdit = new StaffClass
            {
                FirstName = selectedStaff.FirstName,
                LastName = selectedStaff.LastName,
                StaffId = selectedStaff.PersonId,
                Address1 = selectedStaff.Address1,
                Address2 = selectedStaff.Address2,
                Salary = selectedStaff.Salary
            };
            return View(staffToEdit);
        }

        public ActionResult EditCustomer(int staffId)
        {
            IC_MotersEntities db = new IC_MotersEntities();

            var selectedStaff = db.People.Find(staffId);
            var staffToEdit = new StaffClass
            {
                FirstName = selectedStaff.FirstName,
                LastName = selectedStaff.LastName,
                StaffId = selectedStaff.PersonId,
                Address1 = selectedStaff.Address1,
                Address2 = selectedStaff.Address2,
                Salary = selectedStaff.Salary
            };
            return View(staffToEdit);
        }

        [HttpPost]
        public ActionResult EditStaffMember(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();

                var newStaff = db.People.Find(staffData.StaffId);
                newStaff.PersonId = staffData.StaffId;
                newStaff.FirstName = staffData.FirstName.Trim();
                newStaff.LastName = staffData.LastName.Trim();
                newStaff.Address1 = staffData.Address1.Trim();
                newStaff.Address2 = staffData.Address2.Trim();
                newStaff.Salary = staffData.Salary;
                db.SaveChanges();
                return RedirectToAction("StaffManagement", "Home");        
        }

        [HttpPost]
        public ActionResult EditCustomer(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();

            var newStaff = db.People.Find(staffData.StaffId);
            newStaff.PersonId = staffData.StaffId;
            newStaff.FirstName = staffData.FirstName.Trim();
            newStaff.LastName = staffData.LastName.Trim();
            newStaff.Address1 = staffData.Address1.Trim();
            newStaff.Address2 = staffData.Address2.Trim();
            newStaff.Salary = staffData.Salary;
            db.SaveChanges();
            return RedirectToAction("CustomerManagement", "Home");
        }

        public ActionResult AddCustomer()
        {
            IC_MotersEntities db = new IC_MotersEntities();
            var addCustomer = new StaffClass();
            return View(addCustomer);
        }

        [HttpPost]
        public ActionResult AddCustomer(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();
            Person newStaff = new Person();
            newStaff.PersonId = staffData.StaffId;
            newStaff.FirstName = staffData.FirstName.Trim();
            newStaff.LastName = staffData.LastName.Trim();
            newStaff.Address1 = staffData.Address1.Trim();
            newStaff.PersonTypeId = 4;
            newStaff.SuburbId =3;
            newStaff.PhoneNumber = staffData.PhoneNumber.Trim();
            db.People.Add(newStaff);
            db.SaveChanges();
            return RedirectToAction("CustomerManagement", "Home");
        }

        public ActionResult AddStaff()
        {
            IC_MotersEntities db = new IC_MotersEntities();
            var addStaff = new StaffClass();
            return View(addStaff);
        }

        [HttpPost]
        public ActionResult AddStaff(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();
            Person newStaff = new Person();
            newStaff.FirstName = staffData.FirstName.Trim();
            newStaff.LastName = staffData.LastName.Trim();
            newStaff.Address1 = staffData.Address1.Trim();
            //newStaff.PersonTypeId = 1;
            newStaff.PersonTypeId = staffData.PersonType;
            newStaff.SuburbId = 3;
            newStaff.PhoneNumber = staffData.PhoneNumber.Trim();
            db.People.Add(newStaff);
            db.SaveChanges();
            return RedirectToAction("StaffManagement", "Home");
        }
        public ActionResult CustomerOrder(int staffId)
        {
            IC_MotersEntities db = new IC_MotersEntities();

                var selectedStaff = db.People.Find(staffId);
                var customerOrder = new StaffClass
                {
                    FirstName = selectedStaff.FirstName,
                    LastName = selectedStaff.LastName,
                    StaffId = selectedStaff.PersonId,
                };

                return View(customerOrder);
            }
        }
}

