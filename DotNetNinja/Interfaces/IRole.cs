using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.Interfaces
{
    public interface IRole
    {
        bool CreateRoles(string role);
    }
}
