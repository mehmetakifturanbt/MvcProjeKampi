using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        private MessageManager mm = new MessageManager(new EfMessageDal());
        private ContactManager cm = new ContactManager(new EfContactDal());
        private ContactValidator cv = new ContactValidator();

        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }

        public PartialViewResult MessageListMenu()
        {

            var contactList = cm.GetList();
            ViewBag.contactCount = contactList.Count();
            var listResult = mm.GetListInbox();
            ViewBag.inboxCount = listResult.Count();
            var listResult2 = mm.GetListSendbox();
            var sendList = listResult2.FindAll(x => x.isDraft == false);
            ViewBag.sendCount = sendList.Count();
            var draftList = listResult.FindAll(x => x.isDraft == true);
            ViewBag.draftCount = draftList.Count();

            return PartialView();
        }
    }
}