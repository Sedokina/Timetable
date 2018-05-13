using System;
using System.Collections.Generic;

namespace DataModel.Models
{
    public partial class DjangoAdminLog
    {
        public int Id { get; set; }
        public DateTime ActionTime { get; set; }
        public string ObjectId { get; set; }
        public string ObjectRepr { get; set; }
        public short ActionFlag { get; set; }
        public string ChangeMessage { get; set; }
        public int? ContentTypeId { get; set; }
        public int UserId { get; set; }

        public DjangoContentType ContentType { get; set; }
        public AuthUser User { get; set; }
    }
}
