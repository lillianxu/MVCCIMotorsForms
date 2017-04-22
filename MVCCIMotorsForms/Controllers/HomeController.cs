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
        #region Default
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
#endregion

        #region Staff Management
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
#endregion
       
        #region Add Staff
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
        #endregion

        #region Customer Management
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
        #endregion
        
        #region Add Customer
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

        #endregion

        #region  Search Function
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
        #endregion

        #region CustomerOrder function

        public ActionResult CustomerOrder(int staffId)
        {


            IC_MotersEntities db = new IC_MotersEntities();

            List<Models.StaffClass> orderList = new List<StaffClass>();


            var joinQuery = (from pn in db.People
                             join so in db.SalesOrders on pn.PersonId equals so.PersonId
                             //join od in db.SalesOrderDetails on so.SalesOrderId equals od.SalesOrderId
                             where pn.PersonId == staffId
                             select new
                             {
                                 StaffId=pn.PersonId,
                                 LastName = pn.LastName,
                                 SalesOrderId=so.SalesOrderId,
                                 OrderNumber = so.OrderNumber,
                                // ProductID=od.ProductId
                             }).ToList();
            foreach (var item in joinQuery) 
            {
                orderList.Add(new StaffClass()
                {
                    StaffId = item.StaffId,
                    LastName = item.LastName,
                    SalesOrderId = item.SalesOrderId,
                    OrderNumber = item.OrderNumber,
                   // ProductID=item.ProductID
                });
            }
            return View(orderList);
        }
        #endregion

        #region AllCustomerOrder
        public ActionResult AllCustomerOrder()
        {

            IC_MotersEntities db = new IC_MotersEntities();
            List<Models.StaffClass> allOrder = new List<StaffClass>();
            var orders = (from pn in db.People
                             join so in db.SalesOrders on pn.PersonId equals so.PersonId
                             join od in db.SalesOrderDetails on so.SalesOrderId equals od.SalesOrderId
                             select new
                             {
                                 StaffId = pn.PersonId,
                                 LastName = pn.LastName,
                                 SalesOrderId = so.SalesOrderId,
                                 OrderNumber = so.OrderNumber,
                                 OrderTime=so.OrderDate,
                                 ProductID=od.ProductId
                               
                             }).ToList();
            foreach (var item in orders)
            {
                allOrder.Add(new StaffClass()
                {
                    StaffId = item.StaffId,
                    LastName = item.LastName,
                    SalesOrderId = item.SalesOrderId,
                    OrderNumber = item.OrderNumber,
                    OrderTime=item.OrderTime,
                    ProductID=item.ProductID
                  
                });
            }

            return View(allOrder);
        }
        #endregion

        # region Add Order
        public ActionResult AddOrder()
        {
            IC_MotersEntities db = new IC_MotersEntities();
            var addOrder = new StaffClass();
            
            //Person saleperson = new Person();

            ////var item = db.People.Where(p => p.PersonId == 4);
            //List<SelectListItem> selectP = new List<SelectListItem>();

            //var staffList = from p in db.People where p.PersonTypeId == 4 select p;
            ////List < Models.StaffClass > staffList = db.People.Where(x => x.PersonTypeId == 4).Select(x => new StaffClass
            ////{
            ////    StaffId = x.PersonId,
            ////}).ToList();

            //foreach (var a in staffList)

            //{
            //    string pname = a.LastName;
            //    string pid = a.PersonId.ToString();
            //    selectP.Add(new SelectListItem { Text = pname, Value = pid});

            //}
            //ViewData["selCate"] =staffList;
            return View(addOrder);
        }

        [HttpPost]
        public ActionResult AddOrder(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();
            SalesOrder newOrder = new SalesOrder();
            newOrder.SalesOrderId = staffData.SalesOrderId;
            newOrder.PersonId = staffData.StaffId;
            newOrder.OrderNumber = staffData.OrderNumber.Trim();
            newOrder.OrderDate = staffData.OrderTime;
            db.SalesOrders.Add(newOrder);
            db.SaveChanges();
            return RedirectToAction("AllCustomerOrder", "Home");
        }
        #endregion

        #region Add Order Detail
        public ActionResult AddOrderDetail()
        {
            IC_MotersEntities db = new IC_MotersEntities();
            var addOrderDetail = new StaffClass();
            return View(addOrderDetail);
        }

        [HttpPost]
        public ActionResult AddOrderDetail(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();
            SalesOrderDetail newOrder = new SalesOrderDetail();
            newOrder.SalesOrderId = staffData.SalesOrderId;
            newOrder.ProductId = staffData.ProductID;
            newOrder.OrderQty = staffData.OrderQty;
            db.SalesOrderDetails.Add(newOrder);
            db.SaveChanges();
            return RedirectToAction("AddOrder", "Home");
        }
        #endregion

        #region EditOrder
        //public ActionResult EditOrder(int salesOrderId)
        //{
            IC_MotersEntities db = new IC_MotersEntities();

            //    var selectedOrder1= (from pn in db.People
            //                         join so in db.SalesOrders on pn.PersonId equals so.PersonId).Find(salesOrderId);

            //    var selectedOrder = (from pn in db.People
            //                     join so in db.SalesOrders on pn.PersonId equals so.PersonId
            //                     //join od in db.SalesOrderDetails on so.SalesOrderId equals od.SalesOrderId
            //                     where so.SalesOrderId == salesOrderId
            //                     select new
            //                     {
            //                         StaffId = pn.PersonId,
            //                         LastName = pn.LastName,
            //                         SalesOrderId = so.SalesOrderId,
            //                         OrderNumber = so.OrderNumber,
            //                         // ProductID=od.ProductId
            //                     });

            //    var editOrder = new StaffClass
            //    {
            //        //StaffId =salesOrderId,
            //        //LastName = selectedOrder.LastName,
            //        //SalesOrderId = selectedOrder.SalesOrderId,
            //        //OrderNumber = selectedOrder.OrderNumber,
            //    };

            public ActionResult EditOrder(int salesOrderId)
            {
                IC_MotersEntities db = new IC_MotersEntities();

                var selectedOrder = db.SalesOrders.Find(salesOrderId);
                var OrderToEdit = new StaffClass
                {
                     OrderTime= selectedOrder.OrderDate,
                     StaffId=selectedOrder.PersonId,
                     OrderNumber=selectedOrder.OrderNumber
                };
                return View(OrderToEdit);
            }

        [HttpPost]
        public ActionResult EditOrder(StaffClass staffData)
        {
            IC_MotersEntities db = new IC_MotersEntities();

            var newOrder = db.SalesOrders.Find(staffData.SalesOrderId);
            newOrder.PersonId = staffData.StaffId;
            newOrder.OrderNumber = staffData.OrderNumber.Trim();
            newOrder.OrderDate = staffData.OrderTime;
            db.SaveChanges();
            return RedirectToAction("AllCustomerOrder", "Home");
        }

        #endregion
    }
}

