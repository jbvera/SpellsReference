﻿using Microsoft.EntityFrameworkCore;
using SpellsReferenceCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellsReferenceCore.Data.ViewModels;

namespace SpellsReferenceCore.Data
{
    public class SpellsReferenceContext : DbContext, ISpellsReferenceContext
    {
        // Query Debugger
        //public Context()
        //{
        //    Database.Log = (message) =>
        //    {
        //        Debug.WriteLine(message);
        //    };
        //}

        //public DbSet<User> Users { get; set; }
        public DbSet<Spellbook> Spellbooks { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<SpellbookSpell> SpellbookSpells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // EF Core does not support code-first many-to-many relationships.
            // As a workaround, we can use a bridge table and create two
            // one-to-many relationships.

            // Set the SpellbookSpell composite primary key
            modelBuilder.Entity<SpellbookSpell>()
                .HasKey(sbs => new { sbs.SpellbookId, sbs.SpellId });

            // Define the Spell to SpellbookSpell relationship
            modelBuilder.Entity<SpellbookSpell>()
                .HasOne(sbs => sbs.Spell)
                .WithMany(s => s.SpellbookSpells)
                .HasForeignKey(sbs => sbs.SpellId);

            // Define the Spellbook to SpellbookSpell relationship
            modelBuilder.Entity<SpellbookSpell>()
                .HasOne(sbs => sbs.Spellbook)
                .WithMany(sb => sb.SpellbookSpells)
                .HasForeignKey(sbs => sbs.SpellbookId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SpellsReferenceCore;Trusted_Connection=True;");
        }

        public DbSet<SpellsReferenceCore.Data.ViewModels.SpellDeleteViewModel> SpellDeleteViewModel { get; set; }

        ///// <summary>
        ///// This method updates a row in the database through an entity represented by 
        ///// the input parameters.
        ///// 
        ///// The parameters must follow the following order:
        ///// {`property1 name`, `property1 value`, `property2 name`, `property2 value`, ...}
        ///// Having an odd number of parameters causes an exception to be thrown.
        ///// 
        ///// NOTE: This method temporarily disables automatic validation to allow partially
        ///// initialized entities to be updated (e.g. Properties with the Required attribute 
        ///// can be left null). Since automatic validation is disabled, all input parameters
        ///// MUST be manually validated prior to calling this method.
        ///// </summary>
        ///// <typeparam name="TEntityType">The type of the entity to update.</typeparam>
        ///// <param name="id">The primary key Id of the entity.</param>
        ///// <param name="parameters">The array of property names and values to update.</param>
        //public void UpdateEntity<TEntityType>(int id, params object[] parameters)
        //{
        //    if (parameters.Count() % 2 != 0)
        //    {
        //        throw new ArgumentException("Invalid number of parameters");
        //    }

        //    Type entityType = typeof(TEntityType);
        //    TEntityType entity = Activator.CreateInstance<TEntityType>();
        //    entityType.GetProperty("Id").SetValue(entity, id);
        //    var set = Set(entityType);
        //    set.Attach(entity);
        //    for (int index = 0; index < parameters.Length; index += 2)
        //    {
        //        var parameterName = (string)parameters[index];
        //        var parameterValue = parameters[index + 1];

        //        entityType.GetProperty(parameterName).SetValue(entity, parameterValue);
        //        Entry((object)entity).Property(parameterName).IsModified = true;
        //    }

        //    Configuration.ValidateOnSaveEnabled = false;
        //    SaveChanges();
        //    Configuration.ValidateOnSaveEnabled = true;

        //}

        ///// <summary>
        ///// This method updates a row in the database through an input instance of an 
        ///// entity. The entity must have a valid Id property, and all other properties
        ///// must pass automatic validation.
        ///// 
        ///// </summary>
        ///// <typeparam name="TEntityType">The type of the entity to update.</typeparam>
        ///// <param name="context">The Context object for updating the database.</param>
        ///// <param name="entity">The entity to update.</param>
        //public void UpdateEntity<TEntityType>(TEntityType entity)
        //    where TEntityType : class
        //{
        //    Type entityType = typeof(TEntityType);
        //    if (entityType.GetProperty("Id").GetValue(entity) == null)
        //    {
        //        throw new ArgumentException("Entity does not have an ID");
        //    }
        //    var dbSet = Set<TEntityType>();
        //    dbSet.Attach(entity);
        //    Entry(entity).State = EntityState.Modified;
        //    SaveChanges();
        //}
    }
}
