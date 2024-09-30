using System;
using System.Collections.Generic;
using System.Linq;

namespace A5.Problems;

public class ProblemB
{

    public static Dictionary<int, int> M = new Dictionary<int, int>();
    public static List<(int, int, int)> intervals = new List<(int, int, int)>();

    public static Dictionary<int, int> p = new Dictionary<int, int>();


    public static void WeightedIntervalScheduling()
    {
        // Read the input
        var inputs = int.Parse(Console.ReadLine() ?? string.Empty);

        for (int i = 0; i < inputs; i++)
        {
            var line = Console.ReadLine() ?? string.Empty;

            var vals = line.Split(" ");

            intervals.Add((int.Parse(vals[0]), int.Parse(vals[1]), int.Parse(vals[2])));
        }

        // Sort the intervals by their end time
        intervals.Sort((a, b) => a.Item2.CompareTo(b.Item2));

        var s = FindSolution(intervals.Count - 1, new List<int>());

        Console.WriteLine(s.Sum());
    }

    // M-COMPUTE-OPT(j)
    public static int ComputeOpt(int j)
    {
        if (j == -1)
        {
            return 0;
        }
        // If we have already calculated the value of M[j], return it
        if (M.TryGetValue(j, out int value))
        {
            return value;
        }

        // Compute M[j] and store it in the dictionary
        M[j] = Math.Max(intervals[j].Item3 + ComputeOpt(calculateP(j)), ComputeOpt(j - 1));
        return M[j];
    }

    public static List<int> FindSolution(int j, List<int> result)
    {
        if (j == -1)
        {
            return result;
        }

        if (intervals[j].Item3 + ComputeOpt(calculateP(j)) > ComputeOpt(j - 1))
        {
            result.Add(intervals[j].Item3);
            return FindSolution(calculateP(j), result);
        }
        else
        {
            return FindSolution(j - 1, result);
        }
    }

    public static int calculateP(int j)
    {
        // If we have already calculated the value of p[j], return it
        if (p.TryGetValue(j, out int value))
        {
            return value;
        }

        // Binary search to find the rightmost interval that ends before intervals[j] starts
        int lowIndex = 0, highIndex = j - 1;
        while (lowIndex <= highIndex)
        {
            int midIndx = (lowIndex + highIndex) / 2;
            if (intervals[midIndx].Item2 <= intervals[j].Item1)
            {
                if (intervals[midIndx + 1].Item2 <= intervals[j].Item1)
                {
                    lowIndex = midIndx + 1;
                }
                else
                {
                    p[j] = midIndx;
                    return midIndx;
                }
            }
            else
            {
                highIndex = midIndx - 1;
            }
        }

        // we should not get here
        p[j] = -1;
        return -1;
    }
}