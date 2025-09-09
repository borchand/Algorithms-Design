using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeUps.Problems;

public class ProblemD
{
    private static Dictionary<int, int[]> SubsetSums = [];

    private static List<(int[], int[])> Results = [];

    public static void EqualSums() {
        var T = int.Parse(Console.ReadLine() ?? string.Empty);

        var results = new Dictionary<int, int[]>();
        for (var i = 0; i < T; i++)
        {
            int[] ints = Console.ReadLine()?.Split(' ').Select(int.Parse).ToArray() ?? [];

            // reomve the first element
            ints = ints.Skip(1).ToArray();

            // reset subset sums
            SubsetSums = [];

            var found = CalcSubset(ints.ToList(), new List<int>(), 0);

            if (!found)
            {
                Results.Add((new int[]{}, new int[]{}));
            }
        }

        var x = 1;
        foreach (var result in Results)
        {
            Console.WriteLine($"Case #{x}:");

            if (result.Item1.Length == 0)
            {
                Console.WriteLine("Impossible");
                x++;
                continue;
            }

            Console.WriteLine(string.Join(" ", result.Item1));
            Console.WriteLine(string.Join(" ", result.Item2));
            x++;
        }
    }

    static bool CalcSubset(List<int> A, List<int> subset, int index)
    {
        var subsetSum = subset.Sum();

        SubsetSums.TryGetValue(subsetSum, out var value);

        if (value != null)
        {
            Results.Add((value, subset.ToArray()));
            return true;
        }

        if (subset.Count > 0)
        {
            SubsetSums[subset.Sum()] = subset.ToArray();
        }

        for (int i = index; i < A.Count; i++) {

            subset.Add(A[i]);

            var found = CalcSubset(A, subset, i + 1);

            if (found)
            {
                return true;
            }

            subset.RemoveAt(subset.Count - 1);
        }

        return false;
    }
}