using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class AuthGroupPermissions
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PermissionId { get; set; }

        public AuthGroup Group { get; set; }
        public AuthPermission Permission { get; set; }
    }
}
