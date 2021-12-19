using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day18
    {
        //This is either a single number (like a 9) or a pair. [9,0] at which time it is a snilfish number that is a single number.
        //[9,0] is SF { left = SF { Number = 9}, Right = SF { Number = 0} }
        public class SnailfishNumber
        {
            public SnailfishNumber(SnailfishNumber? parent)
            {
                Parent = parent;
            }
            public SnailfishNumber(SnailfishNumber? parent, string parseString)
            {
                Parent = parent;
                Parse(parseString);
            }

            public override string ToString()
            {
                if (Left==null && Right==null)
                {
                    return Number.ToString();
                }
                else
                {
                    return $"[{Left.ToString()},{Right.ToString()}]";
                }
            }
            public int Parse(string problem)
            {
                switch (problem[0])
                {
                    case '[':
                        int i = 0;
                        Left = new SnailfishNumber(this);
                        i += 1;//for the opening bracket
                        i += Left.Parse(problem[i..]);
                        i += 1;//for the comma
                        Right = new SnailfishNumber(this);
                        i += Right.Parse(problem[i..]);
                        i += 1;//for the closing bracket.
                        return i;
                    default://number
                        Number = problem[0] - '0';
                        return 1;
                }
            }

            public bool Explode()
            {
                if (Left != null && Left.Left!=null)
                {
                    if (Left.Explode())
                        return true;
                }
                if (Right != null && Right.Left!=null)
                {
                    if (Right.Explode())
                        return true;
                }
                //find a nesting 4 levels deep. In the example there is one right at the top, and then another within the Right.
                var parent = this.Parent;
                int level = 0;
                while (parent != null)
                {
                    level++;
                    parent = parent.Parent;
                }
                if (level >= 4 && Left!=null && Right!=null)
                {
                    //Console.WriteLine($"Found an explosion: [{Left.Number},{Right.Number}]. Projected Left and Right numbers: {ParentLeft?.Number} and {ParentRight?.Number}");
                    if (ParentLeft != null)
                    {
                        ParentLeft.Number += Left.Number;
                    }
                    if (ParentRight != null)
                    {
                        ParentRight.Number += Right.Number;
                    }
                    if (Parent.Left==this)
                    {
                        Parent.Left = new SnailfishNumber(Parent);
                        Parent.Left.Number = 0;
                    }
                    else
                    {
                        Parent.Right = new SnailfishNumber(Parent);
                        Parent.Right.Number = 0;
                    }
                    return true;
                }

                return false;
            }

            public bool Split()
            {
                if (Left != null)
                {
                    if (Left.Split())
                        return true;
                }
                if (Right != null)
                {
                    if (Right.Split())
                        return true;
                }
                if (Left==null && Right==null && Number >= 10)
                {
                    if (Parent.Left==this)
                    {
                        Parent.Left = new SnailfishNumber(Parent);
                        Parent.Left.Left = new SnailfishNumber(Parent.Left);
                        Parent.Left.Right = new SnailfishNumber(Parent.Left);
                        Parent.Left.Left.Number = Number / 2;
                        Parent.Left.Right.Number = Number / 2 + (((Number / 2) * 2 != Number) ? 1 : 0);
                        //Console.WriteLine($"Split: {Number} becomes [{Parent.Left.Left.Number},{Parent.Left.Right.Number}]");
                        return true;

                    }
                    else
                    {
                        Parent.Right = new SnailfishNumber(Parent);
                        Parent.Right.Left = new SnailfishNumber(Parent.Right);
                        Parent.Right.Right = new SnailfishNumber(Parent.Right);
                        Parent.Right.Left.Number = Number / 2;
                        Parent.Right.Right.Number = Number / 2 + (((Number / 2) * 2 != Number) ? 1 : 0);
                        //Console.WriteLine($"Split: {Number} becomes [{Parent.Right.Left.Number},{Parent.Right.Right.Number}]");
                        return true;
                    }
                }
                return false;
            }

            public void Reduce()
            {
                bool ActionTaken = true;
                while (ActionTaken==true)
                {
                    if (Explode())
                        continue;
                    if (Split())
                        continue;
                    ActionTaken = false;
                }
            }

            public long Magnitude
            {
                get
                {
                    if (Left==null && Right==null)
                        return Number;
                    return Left.Magnitude*3 + Right.Magnitude*2;
                }
            }
            public SnailfishNumber? Left { get; set; } = null;
            public SnailfishNumber? Right { get; set; } = null;
            public int Number { get; set; }
            public SnailfishNumber? Parent { get; set; } = null;
            public SnailfishNumber? ParentLeft
            {
                get
                {
                    SnailfishNumber? parent = null;
                    if (Parent != null)
                    {
                        if (Parent.Left != this)
                            parent = Parent.Left;
                        else parent = Parent.ParentLeft;
                    }
                    if (parent != null && parent.Right!=null)
                    {
                        while (parent.Right!=null)
                        {
                            parent = parent.Right;
                        }
                    }
                    return parent;
                }
            }
            public SnailfishNumber? ParentRight
            {
                get
                {
                    SnailfishNumber? parent = null;
                    if (Parent != null)
                    {
                        if (Parent.Right != this)
                            parent = Parent.Right;
                        else parent = Parent.ParentRight;
                    }
                    if (parent != null && parent.Left!=null)
                    {
                        while (parent.Left!=null)
                        {
                            parent = parent.Left;
                        }
                    }
                    return parent;
                }
            }

        }

        public Day18()
        {

        }

        public string SolvePart1()
        {
            var lines = data.Replace("\r\n", "\n").Split('\n');
            var sum = new SnailfishNumber(null, lines.First());
            foreach (var line in lines.Skip(1))
            {
                var newSum = new SnailfishNumber(null)
                {
                    Left = sum
                };
                sum.Parent = newSum;
                newSum.Right = new SnailfishNumber(newSum, line);
                sum = newSum;
                sum.Reduce();
            }
            return sum.Magnitude.ToString();
        }

        public string SolvePart2()
        {
            List<long> magnitudes = new();
            foreach (var line in data.Replace("\r\n", "\n").Split('\n'))
            {
                foreach (var line2 in data.Replace("\r\n", "\n").Split('\n'))
                {
                    if (line==line2)
                        continue;
                    var sum = new SnailfishNumber(null);
                    sum.Left = new SnailfishNumber(sum, line);
                    sum.Right = new SnailfishNumber(sum, line2);
                    sum.Reduce();
                    magnitudes.Add(sum.Magnitude);
                }
            }
            return magnitudes.Max().ToString();
        }

        public static string tdata = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]";


        public static string data = @"[[0,6],[[[4,0],[6,6]],[[2,2],9]]]
[[9,[[1,6],[6,0]]],[[1,[0,8]],[[0,8],[9,8]]]]
[[[0,[2,1]],3],[[[2,4],[1,2]],[7,5]]]
[[[[8,3],[8,5]],[[7,8],[5,5]]],[9,2]]
[[8,[1,9]],[[[9,9],[9,2]],1]]
[[[[3,7],[2,1]],[0,9]],4]
[[[[3,8],[6,0]],[0,7]],[[[6,3],[2,0]],9]]
[[[9,[7,0]],[8,[9,6]]],[[5,6],4]]
[[[[3,6],[3,6]],[0,2]],[[[8,3],9],[[3,4],8]]]
[[7,[8,4]],1]
[6,[[3,[5,6]],[0,6]]]
[[[7,[4,7]],[[4,5],[4,3]]],[[5,5],[0,[4,2]]]]
[[[0,[2,9]],[[2,4],[4,8]]],[[8,[9,5]],[[9,6],0]]]
[[[[2,0],[9,7]],[[3,2],0]],[7,7]]
[[5,[2,1]],[[3,[5,1]],[[8,5],[1,8]]]]
[[[[9,7],6],[[7,8],7]],[[[6,8],9],[[9,5],7]]]
[[4,2],[[[0,1],[7,2]],[[0,2],[5,5]]]]
[[1,8],[[5,[7,9]],[[3,1],[7,1]]]]
[[[4,[4,6]],6],5]
[[[5,[3,6]],6],[[[8,0],[8,6]],[[3,3],[0,1]]]]
[[4,[[2,6],[0,9]]],[[0,6],[4,2]]]
[[[[9,4],[6,5]],7],[[[1,5],[0,9]],[4,[4,2]]]]
[[7,[[6,5],8]],[[[5,6],0],[6,[3,5]]]]
[[[5,[6,4]],[8,[0,4]]],[[3,[9,3]],4]]
[[[[4,0],6],[6,[6,5]]],[[9,[6,3]],[[9,6],7]]]
[[[[2,2],4],[8,[7,2]]],[2,1]]
[5,[9,[[5,9],4]]]
[[[1,[7,7]],[[2,2],8]],[[[9,7],5],[4,3]]]
[[[[6,8],1],1],[1,[[2,0],6]]]
[[[[0,5],8],[[8,9],[9,3]]],[[[5,5],[4,2]],2]]
[[[9,[2,5]],[6,[1,7]]],[5,[3,[2,2]]]]
[[[7,6],8],[[[1,9],3],[5,2]]]
[8,[[2,[0,7]],8]]
[[[[8,1],[0,0]],5],1]
[[1,[[4,8],0]],[[9,[7,8]],5]]
[[[[1,3],1],[[9,8],[6,6]]],5]
[[[3,2],[[0,5],[0,1]]],[[9,[9,3]],[4,9]]]
[[[0,[2,4]],[[3,3],[6,5]]],[[1,[2,1]],[[3,4],9]]]
[[2,[3,[7,6]]],[5,5]]
[[[[8,2],0],[[9,6],[9,0]]],[[[6,2],[5,0]],9]]
[7,[9,7]]
[3,[[[5,5],1],[8,5]]]
[[[5,5],[5,6]],[9,5]]
[[[9,7],[1,2]],[8,[5,[7,0]]]]
[[[1,[5,2]],[7,[8,9]]],[2,[[4,5],[2,3]]]]
[[[4,[2,2]],[5,[4,7]]],[[[0,3],2],[5,[2,6]]]]
[[0,[[6,5],5]],[[7,[7,2]],3]]
[[[4,[9,4]],[1,9]],[7,[[7,1],[6,1]]]]
[1,[0,2]]
[[[[5,1],[2,1]],[[7,8],6]],[[3,[4,9]],2]]
[[9,[[4,0],[8,8]]],[[[6,6],[2,8]],[1,[1,5]]]]
[[[1,2],[7,0]],[7,[[3,0],5]]]
[[[6,[0,8]],3],[[3,7],1]]
[[[[6,1],[1,0]],9],[[4,8],[3,[0,8]]]]
[[6,[3,[5,8]]],9]
[[[[5,0],[7,7]],[[3,1],[4,8]]],5]
[[[3,7],[9,0]],[[[0,2],7],0]]
[8,9]
[[8,[[0,8],4]],[1,[[4,6],2]]]
[[[5,5],3],[[6,6],[0,[6,3]]]]
[[[7,[3,7]],[[6,1],[9,4]]],[[[8,9],1],[[8,7],6]]]
[[6,[[0,9],[2,3]]],[[1,[5,3]],[8,4]]]
[[[3,5],8],[[[2,4],[7,5]],5]]
[[0,[[7,0],[9,4]]],[[[0,0],[6,7]],[6,5]]]
[[[[1,9],[6,4]],0],[6,[3,[4,8]]]]
[[[[1,6],[0,4]],8],[[8,8],6]]
[[[[7,4],[9,6]],7],[[1,6],[1,0]]]
[1,[[[6,8],5],5]]
[8,4]
[9,[[9,[3,9]],0]]
[5,[[[4,9],7],[[1,0],0]]]
[[[6,1],[0,[2,3]]],[[[7,8],[5,9]],9]]
[3,[[3,[3,4]],[6,[7,8]]]]
[[[7,[7,1]],[4,[2,0]]],[6,[7,3]]]
[[6,9],[[3,[4,7]],3]]
[1,[[9,[5,1]],[7,[7,5]]]]
[[3,2],[[9,[6,8]],[[1,0],2]]]
[[[[3,2],8],[7,6]],9]
[[3,[[9,5],6]],[5,9]]
[[[3,[6,3]],[[7,0],[5,7]]],[[3,3],[[4,9],[4,8]]]]
[[[0,[4,3]],2],[3,[0,[1,3]]]]
[[[7,[3,4]],[7,[3,1]]],[[0,[4,7]],6]]
[[[1,[7,4]],[[8,7],3]],4]
[[[5,5],[[0,3],2]],[1,[[9,4],6]]]
[[[[6,0],[8,8]],[6,[6,0]]],[5,6]]
[[[1,[5,4]],[[5,9],[1,7]]],[[5,[4,7]],[4,[4,4]]]]
[[0,[[2,6],0]],[[6,[4,3]],5]]
[[[1,[5,3]],[9,[1,2]]],[[[4,8],[5,6]],0]]
[[0,7],[1,[7,7]]]
[4,[[7,[7,2]],[[9,1],7]]]
[2,[[1,6],[6,9]]]
[[[4,[4,5]],9],[[[1,7],6],[3,[7,3]]]]
[[6,[[1,1],[7,8]]],[[[5,2],[8,1]],5]]
[[[5,5],[[4,1],[1,2]]],[[3,8],[3,4]]]
[[[[1,9],[0,3]],[4,[0,9]]],4]
[[[4,9],0],[[9,0],[8,[7,5]]]]
[[6,[5,3]],[[[6,6],4],[[6,8],4]]]
[[[[1,1],2],1],[1,[[6,4],2]]]
[[[[6,3],[1,5]],[6,[7,7]]],[6,6]]
[[[[3,0],[5,6]],1],[[[9,3],[1,7]],[[3,4],[2,7]]]]";
    }
}
