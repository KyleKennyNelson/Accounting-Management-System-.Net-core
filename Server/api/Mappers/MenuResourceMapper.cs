
using api.Dtos.MenuResource;
using api.Models;
using LKACSoftModel;
using System;

namespace api.Mappers
{
    public static class MenuResourceMapper
    {
        public static MenuResourceDto ToMenuResourceDto(this V_Menu Menu, List<MenuResourceDto> MenuChildrens, List<RolePermissionDto> rolePermissions)
        {
            return new MenuResourceDto
            {
                menuID = Menu.menuID,
                MenuName = Menu.menuName,
                OwnerRoles = rolePermissions,
                children = MenuChildrens
            };
        }

        public static Menu ToMenuModel(this V_Menu Menu)
        {
            return new Menu
            {
                Id = Menu.menuID,
                MenuName = Menu.menuName
            };
        }

        public static Role ToRoleModel(this V_Role Role)
        {
            return new Role
            {
                Id = Role.RoleID,
                RoleName = Role.RoleName,
                UserCount = Role.UserCount
            };
        }

        public static Perm ToPermModel(this Identity.Permission perm)
        {
            return new Perm
            {
                Id = perm.Id,
                PermName = perm.Name
            };
        }

        public static List<RoleMenuPerm> ToRoleMenuPerm(this List<RoleMenuPermAll> roleMenuPermAlls)
        {
            //var roles = new List<Role>();
            //var menus = new List<Menu>();
            //var permissions = new List<Permission>();

            var roleMenuPerms = new List<RoleMenuPerm>();

            foreach (var roleMenuPermAll in roleMenuPermAlls)
            {
                // Check if the Role-Menu combination already exists
                var existingEntry = roleMenuPerms.FirstOrDefault(rm => rm.Role.Id == roleMenuPermAll.RoleId && rm.Menu.Id == roleMenuPermAll.MenuId);

                if (existingEntry == null)
                {
                    // Create a new RoleMenuPerm entry
                    var newRoleMenuPerm = new RoleMenuPerm
                    {
                        Role = new Role
                        {
                            Id = roleMenuPermAll.RoleId,
                            RoleName = roleMenuPermAll.RoleName,
                            UserCount = roleMenuPermAll.UserCount
                        },
                        Menu = new Menu
                        {
                            Id = roleMenuPermAll.MenuId,
                            MenuName = roleMenuPermAll.MenuName
                        },
                        Perms = new List<Perm>
                        {
                            new Perm
                            {
                                Id = roleMenuPermAll.PermId,
                                PermName = roleMenuPermAll.PermName
                            }
                        }
                    };

                    roleMenuPerms.Add(newRoleMenuPerm);
                }
                else
                {
                    // Add permission to the existing RoleMenuPerm entry
                    existingEntry.Perms ??= new List<Perm>();
                    if (!existingEntry.Perms.Any(p => p.Id == roleMenuPermAll.PermId))
                    {
                        existingEntry.Perms.Add(new Perm
                        {
                            Id = roleMenuPermAll.PermId,
                            PermName = roleMenuPermAll.PermName
                        });
                    }
                }
            }

            return roleMenuPerms;
        }

        public static RolePermissionDto ToRolePermissionDto(this V_Role_Menu roleMenu, List<PermissionDto> permissions)
        {
            return new RolePermissionDto
            {
                menuID = roleMenu.menuID,
                rolemenuID = roleMenu.rolemenuID,
                RoleName = roleMenu.roleName,
                PermissionType = permissions,
                permissionID = roleMenu.permissionID
            };
        }

        public static PermissionDto ToPermissionDto(this V_Permission_RoleMenu permissions)
        {
            return new PermissionDto
            {
                rolemenuID = permissions.rolemenuID,
                PermissionName = permissions.permissionName,
                permissionID = permissions.permissionID,
            };
        }
    }
}
