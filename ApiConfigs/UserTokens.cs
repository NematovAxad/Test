using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiConfigs
{
    public static class UserTokens
    {
        public static int UserId(this ControllerBase cBase)
        {
            var value = cBase.User.FindFirst(m => m.Type.ToLower() == "id")?.Value;
            if (string.IsNullOrEmpty(value))
            {
                return 0;

            }
            return int.Parse(value);
        }
        public static int UserOrgId(this ControllerBase cBase)
        {
            var value = cBase.User.FindFirst(m => m.Type.ToLower() == "orgid")?.Value;
            if (string.IsNullOrEmpty(value)) { return 0; }
            return int.Parse(value);
        }
        public static List<string> UserRights(this ControllerBase cBase)
        {
            List<string> rights = new List<string>();
            var values = cBase.User.FindAll(m => m.Type.ToLower() == "nis_systemaccess");
            foreach (var i in values)
            {
                rights.Add(i.Value);
            }
            if (rights.Count == 0)
            {
                return null;
            }
            return rights;
        }
    }
}
