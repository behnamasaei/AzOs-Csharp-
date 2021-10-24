using System;
using System.Threading;

namespace TicTocToe
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("Select from menu:");
            Console.WriteLine($"1. Human    vs    Human\n" +
                              $"2. human    vs    pc");
            string selectMenu = Console.ReadLine();

            Program program = new Program();
            switch (selectMenu)
            {
                case "1":
                    program.TwoPlayer();
                    break;

                case "2":
                    program.OnePlayer();
                    break;

                default:
                    Console.Clear();
                    Main(args);
                    break;
            }
        }

        void OnePlayer()
        {
            var start = DateTime.UtcNow;

            string[,] cellSelect = { { "-", "-", "-" }, { "-", "-", "-" }, { "-", "-", "-" } };
            string win;
            Random random = new Random();
            int row, col;
            do
            {
                var cells = CellSelectBoard(cellSelect);
                row = cells[0];
                col = cells[1];

                cellSelect[row, col] = "X";

                Console.Clear();
                win = WinCondition(cellSelect);
                if (win != "")
                    break;

                BoardPrint(cellSelect);

                ////
                ///


                do
                {
                    row = random.Next(0, 3);
                    col = random.Next(0, 3);


                } while (cellSelect[row, col] != "-");
                cellSelect[row, col] = "O";

                Console.Clear();
                win = WinCondition(cellSelect);
                if (win != "")
                    break;

                BoardPrint(cellSelect);

            } while (true);

            var end = DateTime.UtcNow;
            Console.WriteLine($"winner is {win} Time:{(end - start)}");
        }

        void TwoPlayer()
        {
            var start = DateTime.UtcNow;

            string[,] cellSelect = { { "-", "-", "-" }, { "-", "-", "-" }, { "-", "-", "-" } };
            string win;

            int row, col;
            do
            {
                var cells = CellSelectBoard(cellSelect);
                row = cells[0];
                col = cells[1];

                cellSelect[row, col] = "X";

                Console.Clear();
                win = WinCondition(cellSelect);
                if (win != "")
                    break;

                BoardPrint(cellSelect);

                ////
                ///

                cells = CellSelectBoard(cellSelect);
                row = cells[0];
                col = cells[1];

                cellSelect[row, col] = "O";

                Console.Clear();
                win = WinCondition(cellSelect);
                if (win != "")
                    break;

                BoardPrint(cellSelect);

            } while (true);
            var end = DateTime.UtcNow;
            Console.WriteLine($"winner is {win} Time:{(end - start)}");
        }

        string WinCondition(string[,] cell)
        {
            for (int i = 0; i < 3; i++)
            {
                if (cell[i, 0] == cell[i, 1] && cell[i, 0] == cell[i, 2] && cell[i, 2] != "-")
                    return cell[i, 2];
            }

            for (int i = 0; i < 3; i++)
            {
                if (cell[0, i] == cell[1, i] && cell[0, i] == cell[2, i] && cell[2, i] != "-")
                    return cell[2, i];
            }

            if (cell[0, 0] == cell[1, 1] && cell[0, 0] == cell[2, 2] && cell[2, 2] != "-")
                return cell[0, 0];

            if (cell[2, 0] == cell[1, 1] && cell[2, 0] == cell[0, 2] && cell[0, 2] != "-")
                return cell[2, 0];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 4)
                        return "Equal";
                    if (i < 3)
                    {
                        if (cell[i, j] == "-")
                            break;
                    }
                }
            }
            return "";
        }

        int[] CellSelectBoard(string[,] cellSelect)
        {
            int row, col;
            do
            {
                Console.Clear();
                BoardPrint(cellSelect);
                Console.Write($"\nselect row cell [0-2] : ");
                row = Convert.ToInt32(Console.ReadLine());
                Console.Write($"\nselect col cell [0-2] : ");
                col = Convert.ToInt32(Console.ReadLine());

            } while (row > 2 || row < 0 || col < 0 || col > 2 || cellSelect[row, col] != "-");
            int[] cells = { row, col };
            return cells;
        }

        void BoardPrint(string[,] cellSelect)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(cellSelect[i, j] + " ");
                }
                Console.WriteLine();
            }
        }


    }
}
