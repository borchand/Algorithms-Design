using System;
using System.Collections.Generic;
using System.Linq;

namespace A3.Problems;

public class ProblemA
{
    public static void IntervalScheduling()
    {
        // Read the input
        var input = int.Parse(Console.ReadLine() ?? string.Empty);

        var intervals = new List<(int, int)>();

        for (int i = 0; i < input; i++)
        {
            var line = Console.ReadLine() ?? string.Empty;

            var vals = line.Split(" ");

            intervals.Add((int.Parse(vals[0]), int.Parse(vals[1])));
        }

        // Sort the intervals by their end time
        intervals.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        var j = new List<(int, int)>();

        j.Add(intervals[0]);

        for (var i = 1; i < intervals.Count; i++){
            if (intervals[i].Item1 >= j.Last().Item2){
                j.Add(intervals[i]);
            }
        }

        Console.WriteLine(j.Count);
    }
}