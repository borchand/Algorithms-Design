using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace A6.Problems;

public class ToysInCat{

    public new List<int> toysInCat { get; set; }

    public int allowedToUse { get; set; }
}

public class ProblemA
{
    public static void WaifUntilDark()
    {
        var input = Console.ReadLine()!.Split(" ").Select(int.Parse).ToArray();
        var children = input[0];
        var toys = input[1];
        var categories = input[2];

        var childLikes = new List<int>[children];
        for (var i = 0; i < children; i++)
        {
            childLikes[i] = Console.ReadLine()!.Split(" ").Skip(1).Select(int.Parse).ToList();
        }

        var toysInCat = new List<ToysInCat>();
        for (var i = 0; i < categories; i++)
        {
            toysInCat[i] = new ToysInCat{toysInCat = new List<int>(), allowedToUse = 0};
            var line = Console.ReadLine()!.Split(" ").Skip(1).Select(int.Parse).ToArray();
            for (var j = 0; j < line.Length; j++)
            {
                toysInCat[i].toysInCat.Add(line[j]);
            }
            toysInCat[i].allowedToUse = line[line.Length - 1];
        }

        var used = new bool[toys];
        var result = 0;
        for (var i = 0; i < children; i++)
        {
            var satisfied = false;
            foreach (var toy in childLikes[i])
            {
                for (var j = 0; j < categories; j++)
                {
                    if (toysInCat[j].toysInCat.Contains(toy) && toysInCat[j].allowedToUse > 0 && !used[toy - 1])
                    {
                        toysInCat[j].allowedToUse--;
                        used[toy - 1] = true;
                        satisfied = true;
                        result++;
                        break;
                    }
                }
                if (satisfied)
                {
                    break;
                }
            }
        }

        Console.WriteLine(result);
    }
}