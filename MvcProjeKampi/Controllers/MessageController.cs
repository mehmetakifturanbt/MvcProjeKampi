using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message

        private MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();

        [Authorize]
        public ActionResult Inbox()
        {
            var messagelist = mm.GetListInbox();
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            var messagelist = mm.GetListSendbox();
            var sendList = messagelist.FindAll(x => x.isDraft == false);
            return View(messagelist);
        }

        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message model, string button)
        {
            ValidationResult results = new ValidationResult();
            if (button == "draft")
            {

                results = messageValidator.Validate(model);
                if (results.IsValid)
                {
                    model.MessageDate = DateTime.Now;
                    model.SenderMail = "admin@gmail.com";
                    model.isDraft = true;
                    mm.MessageAdd(model);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "save")
            {
                results = messageValidator.Validate(model);
                if (results.IsValid)
                {
                    model.MessageDate = DateTime.Now;
                    model.SenderMail = "admin@gmail.com";
                    model.isDraft = false;
                    mm.MessageAdd(model);
                    return RedirectToAction("SendBox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }

        public ActionResult Draft()
        {
            var sendList = mm.GetListSendbox();
            var draftList = sendList.FindAll(x => x.isDraft == true);
            return View(draftList);
        }

        public ActionResult GetDraftMessageDetails(int id)
        {
            var Values = mm.GetById(id);
            return View(Values);
        }

    }
}