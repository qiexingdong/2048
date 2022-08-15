using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Program
    {
        //
        static void Main(string[] args)
        {
            GameCore gameCore = new GameCore();
            gameCore.RandomEmpty();
            gameCore.RandomEmpty();
            do
            {
                if(gameCore.Flag)
                gameCore.RandomEmpty();
                ArrOut(gameCore);
                AllMove(gameCore);
            } while (gameCore.Check());
            Console.WriteLine("程序已经退出");
        }
        //
        private static void ArrOut(GameCore core)
        {
            Console.Clear();
            for (int i = 0; i < core.Arr.GetLength(0); i++)
            {
                for (int j = 0; j < core.Arr.GetLength(1); j++)
                    Console.Write(core.Arr[i, j] + "\t");
                Console.WriteLine();
            }
        }
        private static void AllMove(GameCore core)
        {
            switch (Console.ReadLine())
            {
                case "w":
                    core.Move(MoveDirection.Up);
                    break;
                case "s":
                    core.Move(MoveDirection.Down);
                    break;
                case "a":
                    core.Move(MoveDirection.Left);
                    break;
                case "d":
                    core.Move(MoveDirection.Right);
                    break;

            }
        }
    }
}
