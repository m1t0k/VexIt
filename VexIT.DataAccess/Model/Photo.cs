using System;

namespace VexIT.DataAccess.Model
{
    public class Photo : EntityBase
    {
        public string Content { get; set; }
        public virtual Event Event { get; set; }
        public Guid EventId { get; set; }
    }
}