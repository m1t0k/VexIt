using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VexIT.DataAccess.Model
{
    public class Event : EntityBase
    {
        [MaxLength(512)] public string Name { get; set; }

        [MaxLength(1024)] public string Place { get; set; }

        public DateTime ScheduledAt { get; set; }
    }
}