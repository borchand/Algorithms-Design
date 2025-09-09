using System;
using System.Collections.Generic;

using System.Linq;

namespace MakeUps.Problems;

public class ProblemB
{
    private static List<int> coins = [];


    // Memoization
    private static Dictionary<int, int> memo = [];

    private static Dictionary<int, int> memoGreedy = [];

    public static void CanonicalCoinSystems()
    {

        var n = int.Parse(Console.ReadLine() ?? string.Empty);

        coins = Console.ReadLine()?.Split(' ').Select(int.Parse).ToList() ?? new List<int>();

        coins.Sort((a, b) => b.CompareTo(a));

        var sumTwoSmallest = coins[coins.Count - 1] + coins[coins.Count - 2];

        var sumTwoLargest = coins[0] + coins[1];


        if (n == 2){
            Console.WriteLine("canonical");

            return;
        }


        for (var i = sumTwoSmallest; i < sumTwoLargest; i++){

            // var startTime = DateTime.Now;

            var greedyCount = GreedyMethod(i);
            // Console.WriteLine("Greedy: " + greedyCount);

            // startTime = DateTime.Now;
            int optimal = OptimalSolution(0, 0, i, greedyCount);
            
            // Console.WriteLine("Optimal: " + optimal);
            if (greedyCount != optimal)
            {
                Console.WriteLine("non-canonical");
                return;
            }

        }

        Console.WriteLine("canonical");
    }

    public static int OptimalSolution(int currentCoinsCount, int currentId, int targetSum, int greedyCount)
    {

        if (memo.TryGetValue(targetSum, out var value))
        {
            return currentCoinsCount + value;
        }

        if (currentId < 0 || currentCoinsCount > greedyCount)
        {
            return int.MaxValue;
        }

        var lookingAt = coins[currentId];
    

        if (lookingAt > targetSum)
        {
            return OptimalSolution(currentCoinsCount, currentId + 1, targetSum, greedyCount);
        }

        if (lookingAt == targetSum)
        {
            return currentCoinsCount + 1;
        }
        
        var missing = targetSum % lookingAt;

        if (missing == 0)
        {
            return currentCoinsCount + targetSum / lookingAt;
        }

        var useCurrent = OptimalSolution(currentCoinsCount + targetSum / lookingAt, currentId + 1, missing, greedyCount);
        var skipCurrent = OptimalSolution(currentCoinsCount, currentId + 1, targetSum, greedyCount);

        var minCoins = Math.Min(useCurrent, skipCurrent);

        memo[targetSum] = minCoins;

        return minCoins;
    }

    public static int GreedyMethod(int sum)
    {
        int count = 0;

        for (int i = 0; i < coins.Count; i++)
        {
            int coin = coins[i];
            count += sum / coin;
            sum %= coin;
        }

        return count;
    }
}