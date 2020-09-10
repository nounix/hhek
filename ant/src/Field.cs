using System;
using System.Drawing;

public class Field
{
    int lenX;
    int lenY;
    char[,] field;

    public Ant[] ants;

    int antCount;
    int queenCount;

    int foodCount;
    char foodSymbol;
    Point[] food;

    public Field()
    {
        lenX = 20;
        lenY = 40;
        field = new char[lenX, lenY];

        antCount = 10;
        queenCount = 5;
        ants = new Ant[antCount+queenCount];

        foodCount = 20;
        foodSymbol = 'F';
        food = new Point[foodCount];

        GenField();
        GenAnts();
        GenFood();
    }

    public Field(int x, int y, int ants)
    {
        lenX = x;
        lenY = y;
        antCount = ants;
    }

    private void GenField()
    {
        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                char symbol = ' ';
                if (i == 0 || i == lenX - 1 || j == 0 || j == lenY - 1)
                {
                    symbol = '#';
                }
                field[i, j] = symbol;
            }
        }
    }

    private void GenFood()
    {
        int tmpFood = 0;
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        while (tmpFood < foodCount)
        {
            int x = rnd.Next(1, lenX - 1);
            int y = rnd.Next(1, lenY - 1);
            if (field[x, y] == ' ')
            {
                food[tmpFood] = new Point(x, y);
                tmpFood++;
            }
        }
    }

    private void GenAnts()
    {
        Random rnd = new Random();

        for (int i = 0; i < antCount; i++)
        {
            ants[i] = new Ant(rnd.Next(1, lenX - 1), rnd.Next(1, lenY - 1), 'o', 100);
        }

        for (int i = antCount; i < antCount+queenCount; i++)
        {
            ants[i] = new Queen(rnd.Next(1, lenX - 1), rnd.Next(1, lenY - 1), 'Q', 100);
        }
    }

    public void Eat(Point point)
    {
        if (field[point.X, point.Y] == foodSymbol)
            food[IndexOf(food, point)] = new Point(-1, -1);
    }

    private int IndexOf(Point[] points, Point point)
    {
        for(int i = 0; i < points.Length; i++)
        {
            if (points[i].X == point.X && points[i].Y == point.Y)
                return i;
        }
        return -1;
    }

    public char OnPos(Point pos)
    {
        if (pos.X > 1 && pos.Y > 1 && pos.X < lenX-1 && pos.Y < lenY-1)
            return field[pos.X, pos.Y];
        return '!'; // "!" means, cant go there
    }

    private void Update()
    {
        // clear field
        for(int i = 1; i < lenX-1; i++)
        {
            for (int j = 1; j < lenY-1; j++){
                field[i, j] = ' ';
            }
        }

        // draw food
        foreach(Point point in food)
        {
            if(point.X > 1 && point.Y > 1)
                field[point.X, point.Y] = foodSymbol;
        }

        // draw ants
        for (int i = 0; i < antCount+queenCount; i++)
        {
            field[ants[i].posX, ants[i].posY] = ants[i].symbol;
        }
    }

    public void Print()
    {
        Update();

        Console.Clear();
        Console.SetCursorPosition(0, 0);

        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                System.Console.Write(field[i, j]);
            }
            System.Console.WriteLine();
        }
    }
}