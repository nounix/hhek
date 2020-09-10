using System;
using battle_ship.dependencies.framework.db;

namespace battle_ship.dependencies.model
{
    public class Game : IEntity
    {
        public Game()
        {
        }

        public Game(int fieldSize)
        {
            Id = Guid.NewGuid();
            Users = new Guid[2];
            FieldSize = fieldSize;
        }

        public Guid[] Users { get; set; }
        public int FieldSize { get; }
        public Guid ActualTurn { get; set; }

        public Guid Id { get; set; }
    }
}