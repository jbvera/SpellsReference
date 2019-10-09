﻿using System.Data.Entity;
using SpellsReference.Models;
using System.Threading.Tasks;

namespace SpellsReference.Data
{
    public interface IContext
    {
        DbSet<Spellbook> Spellbooks { get; set; }
        DbSet<Spell> Spells { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}