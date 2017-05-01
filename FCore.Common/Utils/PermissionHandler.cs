using FCore.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCore.Common.Utils
{
    public static class PermissionHandler<PermEntity> where PermEntity : IPermission
    {
        public static PermEntity VerifyHierarchy(PermEntity currentPerms, PermEntity postedPerms)
        {
            if (currentPerms.Admin != postedPerms.Admin)
            {
                return SetAdminRole(postedPerms);
            }
            else
            {
                if (currentPerms.Admin == true)
                {
                    postedPerms.Admin = false;
                }
                else
                {
                    //string falseRole = string.Empty;
                    if (IsTwoRolesTrue(currentPerms)) // , out falseRole
                    {
                        postedPerms.Admin = true;
                        

                        //switch (falseRole)
                        //{
                        //    case "chat":
                        //        postedPerms.Admin = SetAdminRole(postedPerms.ManageChat);
                        //        break;
                        //    case "edit":
                        //        postedPerms.Admin = SetAdminRole(postedPerms.Edit);
                        //        break;
                        //    case "create":
                        //        postedPerms.Admin = SetAdminRole(postedPerms.Create);
                        //        break;
                        //}
                        //return postedPerms;
                    }
                    else
                    {
                        if (currentPerms.Create == true && currentPerms.Edit == true && currentPerms.ManageChat == true)
                        {
                            postedPerms.Admin = false;
                        }
                    }
                }

                return postedPerms;
            }
        }

        private static PermEntity SetAdminRole(PermEntity postedPerms)
        {
            if (postedPerms.Admin == false)
            {
                postedPerms.Create = false;
                postedPerms.Edit = false;
            }
            else
            {
                postedPerms.Create = true;
                postedPerms.Edit = true;
                postedPerms.ManageChat = true;
            }
            return postedPerms;
        }

        ///// <summary>
        ///// Determines if the posted permission role, to additional two true current roles, is to be raised or demote admin.
        ///// </summary>
        ///// <param name="parallel">The posted role parallel to the current false role</param>
        ///// <returns></returns>
        //private static bool SetAdminRole(bool parallel) 
        //{
        //    if (parallel == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private static bool IsTwoRolesTrue(PermEntity current) // , out string falseRole
        {
            if (current.Create == true & current.Edit == true && current.ManageChat == false)
            {
                //falseRole = "chat";
                return true;
            }
            if (current.Create == true & current.Edit == false && current.ManageChat == true)
            {
                //falseRole = "edit";
                return true;
            }
            if (current.Create == false & current.Edit == true && current.ManageChat == true)
            {
                //falseRole = "create";
                return true;
            }
            //falseRole = string.Empty;
            return false;
        }
    }
}
