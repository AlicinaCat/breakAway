using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Models.Contact;


namespace BreakAway.Controllers
{
    public class ContactController : Controller
    {
        private readonly IRepository _repository;

        public ContactController(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public ActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var viewModel = new IndexViewModel();

            viewModel.Contacts = (from contact in _repository.Contacts
                                  select new ContactItem
                                  {
                                      Id = contact.Id,
                                      FirstName = contact.FirstName,
                                      LastName = contact.LastName,
                                      Title = contact.Title,
                                      AddDate = contact.AddDate,
                                      ModifiedDate = contact.ModifiedDate
                                  }).ToArray();

            return View(viewModel);
        }

        public ActionResult Edit(int id, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var contact = _repository.Contacts.FirstOrDefault(p => p.Id == id);

            if (contact == null)
            {
                return RedirectToAction("Index", "Contact");
            }

            var viewModel = new EditViewModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Title = contact.Title,
                AddDate = contact.AddDate,
                ModifiedDate = contact.ModifiedDate,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", "Contact", model);
            }

            var contact = _repository.Contacts.FirstOrDefault(p => p.Id == model.Id);

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Title = model.Title;
            contact.AddDate = model.AddDate;
            contact.ModifiedDate = model.ModifiedDate;

            _repository.Save();

            return RedirectToAction("Edit", "Contact", new { id = contact.Id, message = "Changes saved successfully" });
        }
    }
}