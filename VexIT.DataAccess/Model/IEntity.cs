using System;

namespace VexIT.DataAccess.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}