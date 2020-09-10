using System;

namespace battle_ship.dependencies.framework.db
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}