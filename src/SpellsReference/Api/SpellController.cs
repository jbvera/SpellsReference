﻿using System.Threading.Tasks;
using System.Web.Http;
using SpellsReference.Api.Models;
using SpellsReference.Data.Repositories;
using System.Linq;

namespace SpellsReference.Api
{
    public class SpellController : ApiController
    {
        private ISpellRepository _spellRepo;

        public SpellController(ISpellRepository spellRepo)
        {
            _spellRepo = spellRepo;
        }

        public Task<SpellDeleteResponse> Delete(SpellDeleteRequest request)
        {
            return null;
        }

        /// <summary>
        /// ROUTE
        /// `api/spell`
        /// 
        /// RESPONSE BODY
        /// {
        ///     "spells": [
        ///         {
        ///             "id": `int`,
        ///             "name": `string`,
        ///             "level": `int`,
        ///             "school": `string`,
        ///             "castingTime": `string`,
        ///             "range": `string`,
        ///             "verbal": `bool`,
        ///             "somatic": `bool`,
        ///             "materials": `string`,
        ///             "duration": `string`,
        ///             "ritual": `bool`,
        ///             "description": `string`
        ///         },
        ///         .
        ///         .
        ///         .
        ///     ]
        /// }
        /// </summary>
        /// <returns>The response body.</returns>
        public async Task<SpellListResponse> Get()
        {
            var spells = await _spellRepo.ListAsync();
            var response = new SpellListResponse();
            spells.ForEach(s => response.Spells.Add(s.GetInfo()));
            return response;
        }

        public Task<SpellDetailsResponse> Get(int id)
        {
            return null;
        }

        public Task<SpellUpdateResponse> Patch(SpellUpdateRequest request)
        {
            return null;
        }

        public Task<SpellListResponse> Post(SpellListRequest request)
        {
            return null;
        }

        public Task<SpellCreateResponse> Post(SpellCreateRequest request)
        {
            return null;
        }
    }
}