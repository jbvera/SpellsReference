﻿using SpellsReference.Api.Models;
using SpellsReference.Models;
using SpellsReference.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SpellsReference.Data.Repositories
{
    public class SpellRepository : ISpellRepository
    {
        private IContext _context;

        public SpellRepository(IContext context)
        {
            _context = context;
        }

        public int? Add(Spell entity)
        {
            try
            {
                _context.Spells.Add(entity);
                _context.SaveChanges();
                return entity.Id;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int?> AddAsync(Spell entity)
        {
            try
            {
                _context.Spells.Add(entity);
                await _context.SaveChangesAsync();
                return entity.Id;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            var goner = new Spell() { Id = id };
            _context.Entry(goner).State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                // We can't use the same technique to delete the Spell by its ID
                // as the Delete method because the API SpellController loads
                // the Spell into the context before calling this method.
                var spell = await _context.Spells.FindAsync(id);
                _context.Spells.Remove(spell);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Spell Get(int id)
        {
            return _context.Spells.SingleOrDefault(s => s.Id == id);
        }

        public Task<Spell> GetAsync(int id)
        {
            return _context.Spells.SingleOrDefaultAsync(s => s.Id == id);
        }

        public List<Spell> List()
        {
            return _context.Spells.ToList();
        }

        public List<Spell> List(SpellFilterViewModel filter)
        {
            var spells = _context.Spells
                .Where(s =>
                    (!filter.Level.HasValue || filter.Level.Value == s.Level) &&
                    (!filter.School.HasValue || filter.School.Value == s.School) &&
                    (filter.Name == null || s.Name.Contains(filter.Name))
                    )
                .ToList();
            return spells;
        }

        public Task<List<Spell>> ListAsync()
        {
            return _context.Spells
                .ToListAsync();
        }

        public Task<List<Spell>> ListAsync(SpellListFilter filter)
        {
            var spells = _context.Spells
                .Where(s =>
                    (!filter.Level.HasValue || filter.Level.Value == s.Level) &&
                    (!filter.School.HasValue || filter.School.Value == s.School) &&
                    (filter.Name == null || s.Name.Contains(filter.Name))
                    )
                .ToListAsync();
            return spells;
        }

        public bool Update(Spell entity)
        {
            try
            {
                _context.UpdateEntity(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Spell entity)
        {
            try
            {
                _context.UpdateEntity(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}