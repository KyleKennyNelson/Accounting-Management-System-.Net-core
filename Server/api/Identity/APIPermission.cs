using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace api.Identity
{
    public class APIPermission
    {
        [Key, Column(Order = 1)]
        public virtual int RoleApiId { get; set; }

        [Key, Column(Order = 2)]
        public virtual int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual ApplicationRoleAPI RoleApi { get; set; }

    }
}
