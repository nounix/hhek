namespace battle_ship.dependencies.model
{
    public class Point
    {
        public Point()
        {
        }

        public Point(int x, int y, char s)
        {
            X = x;
            Y = y;
            S = s;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public char S { get; set; }

        public bool Equals(Point p)
        {
            return X.Equals(p.X) && Y.Equals(p.Y);
        }
    }
}