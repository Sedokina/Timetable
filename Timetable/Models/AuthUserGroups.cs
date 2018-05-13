using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class AuthUserGroups
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public AuthGroup Group { get; set; }
        public AuthUser User { get; set; }
    }
}
