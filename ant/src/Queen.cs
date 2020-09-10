using System;
using System.Drawing;

class Queen : Ant
{
    private static char symbolDead = 'X';

    public Queen () {
        symbol = 'Q';
    }

    public Queen(int x, int y, char symb, int days)
    {
        posX = x;
        posY = y;
        symbol = symb;
        this.days = days;
    }

    public new void Move(Func<Point, char> onPos, Action<Point> eat)
    {

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
                    Point point = new Point(posX - 2, posY);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posX-=2;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
            case 1:
                {
                    Point point = new Point(posX + 2, posY);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posX+=2;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
            case 2:
                {
                    Point point = new Point(posX, posY - 2);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posY-=2;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
            case 3:
                {
                    Point point = new Point(posX, posY + 2);
                    char pos = onPos(point);
                    if (pos == ' ' || pos == 'F')
                    {
                        posY+=2;
                        days--;
                        if (pos == 'F') eat(point);
                    }
                    break;
                }
        }

    }
}