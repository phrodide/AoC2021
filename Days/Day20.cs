﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day20
    {
        class Image
        {
            public Image(string source)
            {
                var lines = source.Split("\r\n");
                image = new int[lines.Length,lines[0].Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        image[i,j] = lines[i][j]=='#' ? 1 : 0;
                    }
                }
            }
            public Image(int y, int x)
            {
                image = new int[y, x];
            }
            int[,] image = new int[0, 0];
            public int this[int y,int x]
            {
                get
                {
                    if (image.GetLength(0) <= y ||image.GetLength(1) <= x || y < 0 || x < 0)
                    {
                        //if (privateRound==1)
                            //return 1;
                        return 0;
                    }
                    return image[y,x];
                }
                set
                {
                    if (image.GetLength(0) > y &&image.GetLength(1) > x && y >= 0 && x >= 0)
                    {
                        image[y, x] = value;
                    }

                }
            }

            public int X {
                get
                {
                    return image.GetLength(1);
                }

            }
            public int Y
            {
                get
                {
                    return image.GetLength(0);
                }

            }

            int privateRound = 1;

            public int GetEnhancementIndex(int y, int x, int round)
            {
                privateRound = round;
                return this[y-1, x-1] << 8 | 
                    this[y-1, x] << 7 |
                    this[y-1, x+1] << 6 |
                    this[y, x-1] << 5 |
                    this[y, x] << 4 |
                    this[y, x+1] << 3 |
                    this[y+1, x-1] << 2 | 
                    this[y+1, x] << 1 |
                    this[y+1, x+1];
            }
        }

        string array = data.Split("\r\n\r\n").First();

        public string SolvePart1()
        {
            var part2 = data.Split("\r\n\r\n").Last();
            Image initial = new Image(part2);
            //Image round1 = new Image(initial.Y+30, initial.X+30);
            //Image round2 = new Image(round1.Y+30, round1.X+30);
            var round1 = Enhance(initial);
            /*for (int y = -15; y < initial.Y+15; y++)
            {
                for (int x = -15; x < initial.X+15; x++)
                {
                    int o = initial.GetEnhancementIndex(y, x,1);
                    int bit = array[o]=='#' ? 1 : 0;
                    round1[y+15,x+15] = bit;
                }
            }*/
            for (int y = 0; y < round1.Y; y++)
            {
                for (int x = 0; x < round1.X; x++)
                {
                    Console.Write(round1[y, x]==1 ? "#" : " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            var round2 = Enhance(round1);
            /*for (int y = -15; y < round1.Y+15; y++)
            {
                for (int x = -15; x < round1.X+15; x++)
                {
                    int o = round1.GetEnhancementIndex(y, x, 2);
                    int bit = array[o]=='#' ? 1 : 0;
                    round2[y+15, x+15] = bit;
                }
            }*/
            int litBits = 0;
            for (int y = 18; y < round2.Y-18; y++)
            {
                for (int x = 18; x < round2.X-18; x++)
                {
                    litBits += round2[y, x];
                    Console.Write(round2[y, x]==1 ? "#" : " ");
                }
                Console.WriteLine();
            }

            return litBits.ToString();
        }

        Image Enhance(Image input)
        {
            int buf = 10;
            Image output = new Image(input.Y+buf+buf, input.X+buf+buf);
            for (int y = -buf; y < input.Y+buf; y++)
            {
                for (int x = -buf; x < input.X+buf; x++)
                {
                    int o = input.GetEnhancementIndex(y, x, 1);
                    int bit = array[o]=='#' ? 1 : 0;
                    output[y+buf, x+buf] = bit;
                }
            }
            return output;

        }

        public string SolvePart2()
        {
            var part2 = data.Split("\r\n\r\n").Last();
            Image initial = new Image(part2);
            for (int i = 0; i < 25; i++)
            {
                var round1 = Enhance(initial);
                var round2 = Enhance(round1);
                var output = new Image(round2.Y-36, round2.X-36);
                for (int y = 0; y < output.Y; y++)
                {
                    for (int x = 0; x < output.X; x++)
                    {
                        output[y, x] = round2[y+18, x+18];
                    }
                }
                initial = output;
            }
            int litBits = 0;
            for (int y = 0; y < initial.Y; y++)
            {
                for (int x = 0; x < initial.X; x++)
                {
                    litBits += initial[y, x];
                    Console.Write(initial[y, x]==1 ? "#" : " ");
                }
                Console.WriteLine();
            }

            return litBits.ToString();
        }

        public static string tdata = @"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###";

        public static string data = @"###.#..##.####.....#.#..##.#....##.##.##..#.####.#.#.#.#.#..##.####.####...##.#.###..#..#.##...##..##..#....###########..####..###....#.##...##...#....#########.#..#..##..#..#.#.#.##...###..####.##.#####.####.#....#.#.##..###...#..#...##.##.#..#...#..##..#..#...###........#..##....#.#.##.##.##.######..##.#...#..#######.###.#..#.#....#.###.#....#.....#.#..##.#.......##..#..#..#...#..##.######.#.####.#....#.....#.##.#.#..##.#..##..#.##..#.##...###......##.#####..##.###.#.#.######.#####..#.#..#.....#....#.##..

.##.#######....##...######.##.#.#...#.#..#..#.##...#.#.#.#.#.##..#......#..##..#.#..#.#.##.#.#...#..
.#.#.#.##..########.....####..#..##...##.###...#########....####.##.####..##.##..####.##...#..#....#
##..#######....##.##.#...#.#.#.##.#...#.###########....#.##.#.#...#...#..###..#..#..#.##.#.#.##..###
#.##....##..##....#..#.####.#..##....###.#...#.####.#..#........#..####..##.#####.#..#.#.#..#...##.#
..##.####...#..#.#.####..#.####....#....#.##.##.####...#..#....####..........##.....###.#..#.####.#.
##.#.#.###.##..#..#...#.###.####.#...#.##.#.##...###.#.####.#.##......###.##.###..#..#.....##.###...
#.#.#####.#######.######..##.#.....#.#.##...#..###.##.#.#....#.....#.##..#.##.#######..##..#....#..#
...#.##.##.#...###.#...#....#.#...##..##.###......#.#.####....####.#######....###..###...#...#....##
.##..##.....##.###..#..##..#......##.#.##...##.#.#.#.##...#####.##.....#....#.##.#.#.#....###.##.#..
..####.##..#.###..#....#..####....#.#.###.####.#....##..#.#.##.....##.###.##.###..##......#....#.#..
#.#.##.....#.##.###..##..####.#...#..##.#.#.##..##......#..###.######.#.##..##.###.#####.#.........#
.#.#.#.####.#.####.#.#.#.#########..##.#.#.....#.####.#.#.#..###.#..#..##..#.#.#.##........#..#.####
######.###..##.##.#.##.##...#.......#.#..#..##.#..#....#.#...##.#.###.#..##.##..##..#....###.#.###..
#..#..#####......####.#.#####...#...##..##.#..###.....#..#.#...######.##.######..#.##.###.#...#...##
..#####.#.#...##.###.####...#..##.##..##.##.#.#..####..#.####....#.#..#....##.##.##..#..##..##.#..#.
....#####.##..##.###.##...#.##.#.##....##...###..#...####.#.#..#..###...##..##.###.##.##...###.###..
....##......#....##..##.#.###..###..##.....##.#.#.#.#.###.###..#.##.####..#.#..#...#...##...#.##.##.
##....##....#.#...#......#..#.#.#..#..##.##..#...###....#.#..#.#.###.###..######.##...##..##...##.#.
##.#..###..#..##...##.#..#...###...#..####.#....####..##.###.##...#....#####.##...#............#.#..
..##..#..###.#...##.#.##.#....###.#...#.#.###..#.#..#.##...#.#.###.###.####..#.#####.###..#.#...#..#
..##..##.#..###.#..#.##.##..#.##.##.####......####.###..#.##..#.#....#.##...###...##.##.##.##.#.#.##
#.##.#.#.#.###.#..#.##.#.#.####...#.#.###.##.##..#.#.#.###.#.##.##.#.#.#.###...##.....#......#...###
.##..#########.####.####.#.##....#....#.#.#.#..#.#..##.###.###....###...#..#.##..#..##..#..#...##.##
..###..##.##..#.#....##..##..#.####..#...#.#.####.#####..##....##.....#....#...####...##...###.####.
########.##..##.##...##.##..###...#####.####.#.#.#.#.######...##..####..##..##....#..##.....###.####
.##.....#..#....#.####...#.###..####...##.#...###.##...##..#..###.#..#.#####...##...#..##.##..###...
.###....#.#####....#.#....#.###.......#######.#...##...#..#.#....#..##..###.####.....#.#.##..##.##..
#...#.##.#.##.#.##...###.#..#.####...##...####.#...#.####.#.##...#...##.......#..#......#.##.#..#.#.
..#####..#......#.#..#...#...#..#.....##.#...##..###.......##..##..#.#....###.#######..#...#.#...##.
.......##.###....#.##....####..#..#.####.#.###########.#..#....###.#.###.#.##...#.#...###.#.#.....#.
.###.##...###....###..#........##.......##...#.###...##.##..#....##.....##.##....##..#...#.##.#..##.
####...#..##......###.##..#.#######.#.#..##...#.#.#...#####.####...########.#..#..###.############..
#.#.#..##....#.....##...#.#####.##.#####...#....#.##.#..#.##.##...###..#.##.#.###.###..#.#.....#..#.
.####.#####..#.##.#.###.##..#.#.##...#...#.#.....###..#..#.#...####...#...##.###.###..##..#.....##..
##...#...##..####....#.#.####...#..##.....#.##...###..##.#.#.#.....##.##.#.........##...###..##....#
####.#.##....#.....##.#....##.......#.#.#.#.###..#.###...###..#.##.###...#..###...###...#...##.#.###
.#...#.#######.###########..#..###.##..#.##.##...#..###....#######.##..#.#....####.#..##...#...#....
###..#..##.#.##.##....#..#.#.##...#####.##......#.##....#.##..##...#.##.#.##.##...#.#.#.#....#.##..#
.##....#..#..#...#..#.#....#.#####...#..#.....#....#.#....##...##.##..#..#..........#.#.###...##.#.#
#....#.####...######.#####.#...##..#.#...##..##...#.##.#.##.####.#####.....###..#..#.#.##.##.#..#...
..##..###.####.##.....###.###...#....##.#.#...#.....##....#.#..#..#####..##.#.###.#..##.#.###...#.#.
..##.##...#..#..#.#..#.#..##.#.#.......#.#.#...#.#..#..##..##.###.###...###..##...#####...#..#.###.#
#.#.#.....##........#####.....#...##..###.#.#.##...#.##..#.###...#.######.###..##..###.#.#.#....##..
..#.#.#.##.####.#..#.....#.##.......##...#..##.#####..###..#..#..####..##.##.#.##...####.#.##..#.##.
#######.#..###.....##..#.#.#####.#..#.#.##.###..#.......###......##..#####..#.#..##.##....#..#..#..#
##.##..###.....##..####.##.###.#.#......#.#.######.#..#......#####.....#######.#.#.#.###.#.###.#.###
##.#..##.##...#...#..##..#...#.##.#...#####..##.##..#.#.#..#..##..#..#.#..#........#...#..###..#.##.
#.#.#...#..###.#......#.#..#.########......##.#.##..#..##..#...###.#..#.#..#.#..#####.##...#########
.#..#.##.#######.#####.##.####....#..#.#.#.#########.#.###....#.#....###...##.#.#....##....#.###....
..######.####....#..##...####..#...#..#...#####.##.#.#....#..#.#######..#....#.#.#..#.#....###..##.#
...#.###..#..######.####....##....####.##..#.#.#####...##.#..#..#.###.#.#.#.##....#.###.#..#.#.###..
#..#.#.#.##.###.###.#....##.#####.#.##.....####.....#.....#.#.###...#.##.#...##.###...##....##.#..#.
###..#.##...###..##..#..#.##.#.####...##.#..#.###..#.#..#.#.....#####..#.######..###..#....##..#.##.
##..##.###.###.#####.#..##...####.###..###.###.##..#..##...#....##.###.##..###..#.........##.##..#.#
##...#....#.##.#.#......####.##....##.##....##...#....#.#.....#....##...##....###..###..###.#.....##
.####...#..###...#####.#.##...##.....##...####..#####..##...#..##...####.....#.##...#.###...#.##.###
##.#..##..#..#.#####...#...##....##.#.#.#.########..#..#.#.###..###...##.##.#.##.#..##.#.##.###.#.#.
###.#.###.###......#....#######.#.#..#..##...##.###....###.##.#...##....#...#..#.#...######..##.###.
#.###.####.###.#.#..##.###..#.##..#...#..#.#..#####..#.#.###.#.#.#..#..#...#.#....######.##.....##.#
.#...#.#.#..#.##..##..#.##.#####..#....######...#...#.....#.#..##.#...###.##.#######.#.##.#....#.#..
.#....#...#..#.#.##.#....##....#..#...#..#.##..#.##.##..#..#..#.#.#..##....##.#..#...#..#...##.##...
..#.#..#.#...#.###.#.#######.#...##..#.##...#.#.###.#..#.##..#..#.#######.####.#.####..#.##.#.##..#.
#.#..#######.###..#.####..#.##.#...###.###.#..#.##.#...#...#.#.#.###..#.##.##....#.###...##...#.#..#
.##...#...##.#.##..####.#.#.#..####.##...##..#..#.#.#..#..#....##.##.##...#.#.#.##.#....#.######...#
##.#..#.##.....###.########.##.####...#.####...##.######.###..#######.#..##.#..#..#.#.####..###.#..#
#.#..####.########...##..##..####..#.##.#..#..#..#..###.######..##.....##.##..#...#....#...##....###
#############..##..###..#.###.##..###..##...##.#.......#####.#.#..##..#.....##.#..#....##....#.##...
####.##.#.#...##.#..#..#.##.##.#.###..#.#.#.....#.#.###..#..##.#.#.##..##.#...##..###....##...##.#.#
#...##.#....#..#.....##..#.#...##...##..#.#########.#..#.#.##........###.###...##..#####.###.#....#.
..#..#...#..##...#..##...#.#.....#.#######.####.##..#.#...#......#.#..####.#........#...#.#..#..##.#
#.##..##.#.#..##.#.####.#.#..#..#...#####.######..#.#.######...#..##.##..#####.#.#.##...##.#..##..##
...##.##.#....###.#...#.##...#..###...##.#.#..#.###.....#...##.......##.###.##..##.#.##...#....##.#.
###....##.#.#.#..#....####.#.....##.....#...##.#.....#.###.#####.#..#.##...###.......#...#.....#.#.#
....#.#..#.#..##.####.##...#######.#..#.##.##..####..#.#....##..#.....#...#.##.####.##.#####.#######
#..#..#.###....#.##.#.#.##.###.##.###.............#....##.##.###..#...#.#..#.#.#.#...#..#......#####
...##..##..#....####.#..##.#.#.#.#.....#.#..###...##.##....#####......#####.##..###.##.#..###..#...#
.##..#.#.#.##.###.#.#..##...#.##..#####.######..#.#...#....#...#..##..#.#.....#.#.##..##.#..##.##...
.#.#.#.#.#..####.#.#.##.....##.##.#.##..###.#..##...##..#....##..#..##...##.#..##..####.#..#.#...#..
#.#..##.#.#..#...##.##....#.#...###...###.####.#.####.##.#...##.#..##....###..#..#.##.#.#..#.#.##..#
.###...####.#.###....##..#...#####......##...#...#..#.#..###..#.....##..###.#.###.....#.##..####.#.#
#..###.#....#..###...#..####.####..#.#####.#.....#...##..##..#...#..#...#.##...##..########..#.##.#.
..######..#.####..#..####..#..#...##...#...###.##...#..###.##...#.###.##...#....#....##..##.##.###.#
###.#.##..###..#..#........#.#.........##..##.....###.###.#.......###..#.#....##....#.....#.##...###
..#.....#.#..###.##.####.#.......#.#.##..##.#.##...####..#.###..#.###.#####.####....#.#...#.#..##..#
.....######.###..##.#..####..#..##.####.##..##.##...#.#..##...#.##..####.####...##..#.....#.....####
###.##.#..#.##.#.#.###....#..#.#..##..#.....###.##.##.#.###.####...##.###..#..##.##..##..##.###...##
#....####.####.####.#.######...#########..##..#.#....#.......#.##.#.....#.###..#.##....#.##..##.##.#
...##..#...#..##..#.###.##..#..###.###....###.####.#.####.#.#.#.#.#....#######.##..###.#.######.##..
#...#.#.#...#.#.#..#.###.#.#####.#.###.#..##.#.####...#.#.#.....#..####..#.###....##.##..#.#..##.#.#
...##..##....#.##.###...##.....###....#..###....#.#.##..###..###..#.#....#.###....#.##..######..#.##
...#..#...###...##.###.....#...#.#..#.##..###.#....###.#.#...##.#..##..###.#...#..#..#.##..#...#.#..
#.....#.###.##..##.#..#..#....#..##.###.##.#.#..##...#....##.##.#.#....#.###..#..###....###.#.#.#.##
...##.#..###..##..##.###.#.#..###....#.##..##.#..###...##...#.#.....###.....##.....#.#.##.#.###..#.#
..##.##..#...#..#.#.##.##.########.#.######....######.#.###..##...###..#####.##.#..#.##.#.##..######
.....##.##..#.#.#...###.#.#....##...###.....#.###...#.....###.#......#.#..#.##..#.######.#...####..#
..####....####.#......#.##..###.###...##########..##......#..###.#.#...#####.#.#########..###..#....
#..#..#.#..#..#..##..#.###.#...#.#.####...#.#######...##.##..#..####.#.....#..##.####.##.##.##.#....
#..#....####.#.###.##...##.###.####.##.######....#.#.#.###.##.##.#.#.##...##.#.#.##.#..#..###.#.....
.###.#....##.#.###...##.#.##...##.###.#.####.##.#####..#.##....##..##....#######...#..###.#....##...
.#..#..###.#.##.....##..#.#.###.###..#...#.#.#...#..##..###....#..##........#.#.##..##.####.#.###.#.";
    }
}
