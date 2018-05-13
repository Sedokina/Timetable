using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class DjangoContentType
    {
        public DjangoContentType()
        {
            AuthPermission = new HashSet<AuthPermission>();
            DjangoAdminLog = new HashSet<DjangoAdminLog>();
        }

        public int Id { get; set; }
        public string AppLabel { get; set; }
        public string Model { get; set; }

        public ICollection<AuthPermission> AuthPermission { get; set; }
        public ICollection<DjangoAdminLog> DjangoAdminLog { get; set; }
    }
}
