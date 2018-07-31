using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VexIT.DataAccess.Model
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        [Key] [Column(Order = 0)] public Guid Id { get; set; }
    }
}