using System;

class Program {
    public static void Main (string[] args) {
        Field field = new Field();

        while(!Console.KeyAvailable)
        {
            foreach(Ant ant in field.ants) {
                ant.Move(field.OnPos, field.Eat);
            }

            field.Print();
            System.Threading.Thread.Sleep(200);
        }
     
        // Console.Write ("Press any key to continue . . . ");
        // Console.ReadKey (true);
    }
}