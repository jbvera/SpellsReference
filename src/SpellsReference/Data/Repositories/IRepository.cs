﻿using System.Collections.Generic;

namespace SpellsReference.Data.Repositories
{

    /// <summary>
    /// This class defines a generic set of methods common to all repositories.
    /// </summary>
    /// <typeparam name="TEntityType">The entity type the implementing repository handles.</typeparam>
    public interface IRepository<TEntityType>
    {
        /// <summary>
        /// Attempts to add an entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The entity's ID in the database if successful; null otherwise.</returns>
        int? Add(TEntityType entity);

        /// <summary>
        /// Attempts to delete an entity from the database.
        /// </summary>
        /// <param name="id">The entity's ID in the database.</param>
        /// <returns>True if the entity was successfully deleted; false otherwise.</returns>
        bool Delete(int id);

        /// <summary>
        /// Attempts to retrieve an entity from the database given its ID.
        /// </summary>
        /// <param name="id">The entity's ID in the database.</param>
        /// <returns>The entity record if successfull; null otherwise.</returns>
        TEntityType Get(int id);

        /// <summary>
        /// Retrieves the list of records of the entity type from the database.
        /// </summary>
        /// <returns>The list of entities.</returns>
        List<TEntityType> List();

        /// <summary>
        /// Attempts to update an entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>True if the entity was successfully updated; false otherwise.</returns>
        bool Update(TEntityType entity);
    }
}