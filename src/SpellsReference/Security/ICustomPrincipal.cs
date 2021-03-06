﻿using System.Security.Principal;

namespace SpellsReference.Security
{
    public interface ICustomPrincipal : IPrincipal
    {
        int Id { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}