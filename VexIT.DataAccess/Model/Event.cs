using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VexIT.DataAccess.Model
{
    public class Event : EntityBase
    {
        public short CategoryId { get; set; }
        [MaxLength(256)] public string Name { get; set; }

        [MaxLength(512)] public string Country { get; set; }
        [MaxLength(512)] public string City { get; set; }
        [MaxLength(512)] public string Street { get; set; }
        [MaxLength(512)] public string Place { get; set; }
        [MaxLength(512)] public string YouTubeUrl { get; set; }
        [MaxLength(2048)] public string Description { get; set; }

        public DateTime ScheduledAt { get; set; }
        public virtual Photo Photo { get; set; }
    }
}