using System;
using System.Drawing;

public class Ant
{
    public int posX;
    public int posY;
    public char symbol;
    private static char symbolDead = 'x';
    public int days;

    public Ant () { }

    public Ant (int x, int y, char symb, int days) {
        posX = x;
        posY = y;
        symbol = symb;
        this.days = days;
    }

    public void Move (Func<Point, char> onPos, Action<Point> eat) {

        if (days <= 0)
        {
            if (days <= -10) symbol = ' ';
            else symbol = symbolDead;
            return;
        }

        Random rand = new Random(Guid.NewGuid().GetHashCode());

        switch (rand.Next(0, 4))
        {
            case 0:
            {
                Point point = new Point(posX - 1, posY);
                char pos = onPos(point);
                if (pos == ' ' || pos == 'F')
                {
                    posX--;
                    days--;
                    if (pos == 'F') eat(point);
                }
                break;
            }
            case 1:
                {
                    Point point = new Point(posX + 1, posY);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posX++;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
            case 2:
                {
                    Point point = new Point(posX, posY-1);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posY--;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
            case 3:
                {
                    Point point = new Point(posX, posY+1);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posY++;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
        }
            
    }
}