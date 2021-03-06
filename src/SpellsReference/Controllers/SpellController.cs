﻿using AutoMapper;
using SpellsReference.Data.Repositories;
using SpellsReference.Models;
using SpellsReference.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SpellsReference.Controllers
{
    public class SpellController : Controller
    {
        private ISpellRepository _spellRepo;

        public SpellController(ISpellRepository spellRepo)
        {
            _spellRepo = spellRepo;
        }

        [AllowAnonymous]
        public ActionResult Index(SpellFilterViewModel filter)
        {
            var viewModel = new SpellListViewModel()
            {
                Spells = filter.HasValues ? _spellRepo.List(filter) : _spellRepo.List()
            };
            return View(viewModel);
        }

        // Selects a single spell from a list of spells. This might later be moved 
        // to a functionality from spellbook. The Spell Edit and Delete will be tied into this view. 
        // Also, maybe [Authorize], not sure.
        [AllowAnonymous]
        public ActionResult Select(int id, int? spellbookId)
        {
            Spell spell = _spellRepo.Get(id);

            // Maybe eventually set up Mapper DI.
            //var mapper = new Mapper(config);
            //SpellViewModel viewModel = mapper.Map<SpellViewModel>(spell);

            var viewModel = new SpellViewModel();
            viewModel.Id = spell.Id;
            viewModel.Name = spell.Name;
            viewModel.Level = spell.Level;
            viewModel.School = spell.School;
            viewModel.CastingTime = spell.CastingTime;
            viewModel.Range = spell.Range;
            viewModel.Verbal = spell.Verbal;
            viewModel.Somatic = spell.Somatic;
            viewModel.Materials = spell.Materials;
            viewModel.Ritual = spell.Ritual;
            viewModel.Description = spell.Description;

            if (spellbookId != null)
            {
                viewModel.SpellbookId = spellbookId;
            }

            return View(viewModel);
        }

        public ActionResult Create(int? spellbookId)
        {
            var viewModel = new SpellViewModel();
            viewModel.SpellbookId = spellbookId;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? spellbookId, SpellViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var spell = new Spell()
                {
                    Name = viewModel.Name,
                    Level = viewModel.Level,
                    School = viewModel.School,
                    CastingTime = viewModel.CastingTime,
                    Range = viewModel.Range,
                    Verbal = viewModel.Verbal,
                    Somatic = viewModel.Somatic,
                    Materials = viewModel.Materials,
                    Duration = viewModel.Duration,
                    Ritual = viewModel.Ritual,
                    Description = viewModel.Description
                };

                var success = _spellRepo.Add(spell);
                if (success.HasValue)
                {
                    if (spellbookId != null)
                    {
                        return RedirectToAction("AddSpell", "Spellbook", new { id = spellbookId });
                    }
                    return RedirectToAction("Index", "Spell");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to add spell.");
                }
            }

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var spell = _spellRepo.Get(id);
            var viewModel = new SpellViewModel()
            {
                Name = spell.Name,
                Level = spell.Level,
                School = spell.School,
                CastingTime = spell.CastingTime,
                Range = spell.Range,
                Verbal = spell.Verbal,
                Somatic = spell.Somatic,
                Materials = spell.Materials,
                Duration = spell.Duration,
                Ritual = spell.Ritual,
                Description = spell.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, SpellViewModel viewModel)
        {
            var spell = new Spell()
            {
                Id = id,
                Name = viewModel.Name,
                Level = viewModel.Level,
                School = viewModel.School,
                CastingTime = viewModel.CastingTime,
                Range = viewModel.Range,
                Verbal = viewModel.Verbal,
                Somatic = viewModel.Somatic,
                Materials = viewModel.Materials,
                Duration = viewModel.Duration,
                Ritual = viewModel.Ritual,
                Description = viewModel.Description
            };

            if (_spellRepo.Update(spell))
            {
                return RedirectToAction("Select", "Spell",
                    routeValues: new { id = spell.Id });
            }
            else
            {
                ModelState.AddModelError("", "Unable to update Spell");
                return View(viewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            Spell spell = _spellRepo.Get(id);

            var viewModel = new SpellViewModel();
            viewModel.Id = spell.Id;
            viewModel.Name = spell.Name;
            viewModel.Level = spell.Level;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SpellViewModel viewModel)
        {
            bool success = _spellRepo.Delete(id);

            if (success)
            {
                return RedirectToAction("Index", "Spell");
            }
            else
            {
                ModelState.AddModelError("", "Unable to delete Spell");
                return View(viewModel);
            }
        }

        [AllowAnonymous]
        public ActionResult Filter()
        {
            var viewModel = new SpellFilterViewModel();
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Filter(SpellFilterViewModel viewModel)
        {
            var routeValues = new
            {
                name = viewModel.Name,
                level = viewModel.Level.ToString(),
                school = viewModel.School.ToString()
            };
            return RedirectToAction("Index", routeValues: routeValues);
        }
    }
}