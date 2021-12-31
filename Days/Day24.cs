using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day24
    {
        //              0   1   2   3   4   5   6   7   8   9   10  11   12  13
        int[] xAdd = { 14, 12, 11, -4, 10, 10, 15, -9, -9, 12, -15, -7, -10,  0 };
        int[] yAdd = {  7,  4,  8,  1,  5, 14, 12, 10,  5,  7,   6,  8,   4,  6 };
        int[] zMul = {  1,  1,  1, 26,  1,  1,  1, 26, 26,  1,  26, 26,  26, 26 };
        int[] input = { 2,  9,  5,  9,  9,  4,  6,  9,  9,  9,   1,  7,   3,  9 };
        int[] input2 = { 1, 7,  1,  5,  3,  1,  6,  9,  6,  9,   1,  1,   1,  8 };

        public string SolvePart1()
        {
            Stack<int> zStack = new Stack<int>();
            zStack.Push(-1);
            int  z = 0;
            for (int i = 0; i < 14; i++)
            {
                if (zMul[i]==26)
                {
                    int trashed = zStack.Peek();
                    Console.WriteLine($"[{trashed}] will be discarded.");
                }
                zStack.TryPeek(out int poppedI);
                Console.Write($"[{i}] Comparing Z[{poppedI}], value of [{(z % 26)}]+[{xAdd[i]}] to input {input[i]}...");
                if ((z % 26)+xAdd[i]!=input[i])
                {
                    Console.WriteLine($"triggered, pushing {input[i]+yAdd[i]} to the stack.");
                    z /= zMul[i];
                    z = z*26 + input[i] + yAdd[i];
                    zStack.Push(i);
                }
                else
                {
                    Console.WriteLine("NOT TRIGGERED");
                    z /= zMul[i];
                }
                if (zMul[i]==26)
                {
                    int trashed = zStack.Pop();
                }
            }


            return z.ToString();
        }

        public string SolvePart2()
        {
            Stack<int> zStack = new Stack<int>();
            zStack.Push(-1);
            int z = 0;
            for (int i = 0; i < 14; i++)
            {
                if (zMul[i]==26)
                {
                    int trashed = zStack.Peek();
                    Console.WriteLine($"[{trashed}] will be discarded.");
                }
                zStack.TryPeek(out int poppedI);
                Console.Write($"[{i}] Comparing Z[{poppedI}], value of [{(z % 26)}]+[{xAdd[i]}] to input {input2[i]}...");
                if ((z % 26)+xAdd[i]!=input2[i])
                {
                    Console.WriteLine($"triggered, pushing {input2[i]+yAdd[i]} to the stack.");
                    z /= zMul[i];
                    z = z*26 + input2[i] + yAdd[i];
                    zStack.Push(i);
                }
                else
                {
                    Console.WriteLine("NOT TRIGGERED");
                    z /= zMul[i];
                }
                if (zMul[i]==26)
                {
                    int trashed = zStack.Pop();
                }
            }


            return z.ToString();
        }

        public string data = @"inp w     
mul x 0
add x z
mod x 26
div z 1
add x 14
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 7
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 12
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 4
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 11
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 8
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -4
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 1
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 10
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 5
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 10
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 14
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 15
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 12
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -9
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 10
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -9
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 5
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 1
add x 12
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 7
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -15
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 6
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -7
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 8
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x -10
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 4
mul y x
add z y
inp w
mul x 0
add x z
mod x 26
div z 26
add x 0
eql x w
eql x 0
mul y 0
add y 25
mul y x
add y 1
mul z y
mul y 0
add y w
add y 6
mul y x
add z y";
    }
}
