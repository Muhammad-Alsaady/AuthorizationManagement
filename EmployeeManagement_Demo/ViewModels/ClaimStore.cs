﻿using System.Security.Claims;

namespace EmployeeManagement_Demo.ViewModels
{
    public class ClaimStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create Role", "Create Role"),
            new Claim("Edit Role", "Edit Role"),
            new Claim("Delete Role", "Delete Role"),
        };

    }
}
