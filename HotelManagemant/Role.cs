using HotelManagemant.Data;
using HotelManagemant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotelManagemant
{
    public class Role : System.Web.Security.RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string[] GetRolesForUser(string username)
        {
            using (var userContext = new Context())
            {
                var user = userContext.Login.Where(u => u.Email == username);
                var userRoles = user.Select(m=>m.Category).ToArray();

                if (user == null)
                    return new string[] { };
                return userRoles;
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

      
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (Context db = new Context())
            {
                Login user = db.Login.FirstOrDefault(u => u.Email.Equals(u.Email.Equals(username, StringComparison.CurrentCultureIgnoreCase)));

                var roles = db.Login.Select(r => r.Category);

                if (user != null)
                    return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
                else
                    return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}