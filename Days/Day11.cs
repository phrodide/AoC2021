using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day11
    {
        int[,] octopus = new int[10, 10];

        public Day11()
        {

        }

        void Flash(int yy, int xx, int[,] Octo, int[,] flash)
        {
            if (flash[yy, xx]==1) return;
            flash[yy, xx] = 1;
            //Console.Write($"[{yy},{xx}] ");
            for (int i = yy-1; i <= yy+1; i++)
            {
                if (i < 0) continue;
                if (i > 9) continue;
                for (int j =xx -1; j <= xx+1; j++)
                {
                    if (j < 0) continue;
                    if (j > 9) continue;
                    if (i==yy && j==xx) continue;
                    Octo[i, j] += 1;
                    if (Octo[i, j] > 9 && flash[i, j]==0)
                    {
                        Flash(i, j, Octo, flash);
                    }
                }
            }
        }

        public string SolvePart1()
        {
            var lines = data.Replace("\r\n", "\n").Split('\n');
            int y = 0;
            foreach (var line in lines)
            {
                for (int x = 0; x < 10; x++)
                {
                    octopus[y, x] = line[x] - '0';
                }
                y++;
            }
            int flashes = 0;
            for (int i = 0; i < 100; i++)
            {
                int[,] newOcto = new int[10, 10];
                int[,] flash = new int[10, 10];
                for (int yy = 0; yy < 10; yy++)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        newOcto[yy, xx] = octopus[yy, xx] + 1;
                    }
                }
                for (int yy = 0; yy < 10; yy++)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        if (newOcto[yy,xx] > 9)
                        {
                            Flash(yy, xx, newOcto,flash);
                        }
                    }
                }
                for (int yy = 0; yy < 10; yy++)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        if (newOcto[yy, xx] > 9)
                        {
                            flashes++;
                            newOcto[yy, xx] = 0;
                        }
                    }
                }
                Console.WriteLine($"Step { i+1 } of 10, Flashes {flashes}");
                octopus = newOcto;
            }

            return flashes.ToString();
        }

        public string SolvePart2()
        {
            var lines = data.Replace("\r\n", "\n").Split('\n');
            int y = 0;
            foreach (var line in lines)
            {
                for (int x = 0; x < 10; x++)
                {
                    octopus[y, x] = line[x] - '0';
                }
                y++;
            }
            for (int i = 0; i < 1000; i++)
            {
                int[,] newOcto = new int[10, 10];
                int[,] flash = new int[10, 10];
                for (int yy = 0; yy < 10; yy++)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        newOcto[yy, xx] = octopus[yy, xx] + 1;
                    }
                }
                for (int yy = 0; yy < 10; yy++)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        if (newOcto[yy, xx] > 9)
                        {
                            Flash(yy, xx, newOcto, flash);
                        }
                    }
                }
                int flashes = 0;
                for (int yy = 0; yy < 10; yy++)
                {
                    for (int xx = 0; xx < 10; xx++)
                    {
                        if (newOcto[yy, xx] > 9)
                        {
                            flashes++;
                            newOcto[yy, xx] = 0;
                        }
                    }
                }
                Console.WriteLine($"Step { i+1 } of 10, Flashes {flashes}");
                if (flashes == 100)
                {
                    return (i+1).ToString();
                }
                octopus = newOcto;
            }

            return "Not enough iterations";
        }

        public static string tdata = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

        public static string data = @"6111821767
1763611615
3512683131
8582771473
8214813874
2325823217
2222482823
5471356782
3738671287
8675226574";
    }
}
