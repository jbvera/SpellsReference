﻿using SpellsReference.Data;
using SpellsReference.Data.Repositories;
using SpellsReference.Models;
using SpellsReference.Models.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SpellsReference.Controllers
{
    [Authorize]
    public class SpellbookController : Controller
    {
        private ISpellbookRepository _spellbookRepo;

        public SpellbookController(ISpellbookRepository spellbookRepo)
        {
            _spellbookRepo = spellbookRepo;
        }

        public ActionResult Index()
        {
            List<Spellbook> spellbooks = _spellbookRepo.List();

            return View(spellbooks);
        }

        public ActionResult Select(int id)
        {
            Spellbook spellbook = _spellbookRepo.Get(id);

            return View(spellbook);
        }

        public ActionResult Create()
        {
            var viewModel = new SpellbookViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpellbookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var spellbook = new Spellbook()
                {
                    Name = viewModel.Name
                };

                var success = _spellbookRepo.Add(spellbook);
                if (success.HasValue)
                {
                    return RedirectToAction("Index", "Spellbook");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to add spellbook.");
                }
            }
            return View(viewModel);
        }

        public ActionResult AddSpell(int id)
        {
            var spellbook = _spellbookRepo.Get(id);
            var spells = _spellbookRepo.GetNonmemberSpells(id);
            var viewModel = new AddSpellToSpellbookViewModel()
            {
                SpellbookId = spellbook.Id,
                SpellbookName = spellbook.Name,
                Spells = spells
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddSpell(int id, int spellId, AddSpellToSpellbookViewModel viewModel)
        {
            if (_spellbookRepo.AddSpellToSpellbook(id, spellId))
            {
                return RedirectToAction("Select", "Spellbook", routeValues: new { id = id });
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult RemoveSpell(int id, int spellId, SpellbookViewModel viewModel)
        {
            if (_spellbookRepo.RemoveSpellFromSpellbook(id, spellId))
            {
                return RedirectToAction("Select", "Spellbook", routeValues: new { id = id });
            }

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            Spellbook spellbook = _spellbookRepo.Get(id);

            var viewModel = new SpellbookViewModel();
            viewModel.Name = spellbook.Name;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SpellbookViewModel viewModel)
        {
            var spellbook = new Spellbook() {          
                Id = id,
                Name = viewModel.Name
            };
            if  (_spellbookRepo.Update(spellbook))
            {
                return RedirectToAction("Select", "Spellbook",
                    routeValues: new { id = spellbook.Id });
            }
            else
            {
                ModelState.AddModelError("", "Unable to update Spellbook");
                return View(viewModel);
            }
        }


        public ActionResult Delete(int id)
        {
            Spellbook spellbook = _spellbookRepo.Get(id);

            var viewModel = new SpellbookViewModel();
            viewModel.Id = spellbook.Id;
            viewModel.Name = spellbook.Name;
            viewModel.Spells = spellbook.Spells;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SpellViewModel viewModel)
        {
            bool success = _spellbookRepo.Delete(id);

            if (success)
            {
                return RedirectToAction("Index", "Spellbook");
            }
            else
            {
                ModelState.AddModelError("", "Unable to delete Spell");
                return View(viewModel);
            }
        }
    }
}