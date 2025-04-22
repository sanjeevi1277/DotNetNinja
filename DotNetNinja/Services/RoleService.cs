using DotNetNinja.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;

namespace DotNetNinja.Services
{
    public class RoleService : IRole
    {
        public readonly ApplicationDbContext _db;
        public RoleService(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateRoles(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                return false;

            var newRole = new Role
            {
                RoleName = role,
            };

            _db.Roles.Add(newRole);
            _db.SaveChanges();
            return true;
        }
    }
}
