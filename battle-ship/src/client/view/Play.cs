using System;
using System.Collections.Generic;
using System.Linq;
using battle_ship.dependencies.framework.net;
using battle_ship.dependencies.framework.view;
using battle_ship.dependencies.model;

namespace battle_ship.client.view
{
    public class Play : View
    {
        private controller.Play _controller;

        public override void SetController(Controller controller)
        {
            _controller = (controller.Play) controller;
        }

        public override void PreRender()
        {
            Terminal.Clear();
        }

        public override void Render()
        {
            Terminal.PrintCenter("Your ships:");
            PrintField(PlaceShips(CreateField(_controller.GetFieldSize()), _controller.GetAllShips()));
            Terminal.PrintCenter("Your hits:");
            PrintField(PlaceHits(CreateField(_controller.GetFieldSize()), _controller.GetAllHits()));

            var op = _controller.GameRunning();

            // Game loop
            while (op.Response.Equals(Operation.Status.Yes))
            {
                if (_controller.HitReady().Equals(Operation.Status.Yes))
                {
                    Terminal.PrintCenter("Enter hit coordinates:");
                    Terminal.ReadCenter();
                }
                else
                    Terminal.PrintLoading("Waiting for enemy:");
            }

            Terminal.PrintCenter(op.DeserializePayload<Guid>("user").Equals(_controller.Session.Id)
                ? "You have lost!"
                : "You have won!");

            Terminal.ReadCenter();
        }

        private static IEnumerable<char[]> PlaceShips(IReadOnlyList<char[]> field,
            IEnumerable<dependencies.model.Ship> ships)
        {
            foreach (var ship in ships)
            foreach (var point in ship.Points)
                field[point.X][point.Y] = point.S;

            return field;
        }

        private static IEnumerable<char[]> PlaceHits(IReadOnlyList<char[]> field,
            IEnumerable<Hit> hits)
        {
            foreach (var hit in hits) field[hit.Point.X][hit.Point.Y] = hit.Point.S;

            return field;
        }

        private static char[][] CreateField(int fieldSize)
        {
            var field = new char[fieldSize][];

            for (var i = 0; i < fieldSize; i++)
                field[i] = new char[fieldSize];

            for (var i = 0; i < fieldSize; i++)
            for (var j = 0; j < fieldSize; j++)
                if (i != 0 && j == 0)
                    field[i][j] = (char) (64 + i);
                else if (j != 0 && i == 0)
                    field[i][j] = (char) (48 + j);
                else
                    field[i][j] = ' ';

            return field;
        }

        private void PrintField(IEnumerable<char[]> field)
        {
            foreach (var row in field)
                Terminal.PrintCenter(string.Concat(row.Select(c => c + " ")));
        }
    }
}