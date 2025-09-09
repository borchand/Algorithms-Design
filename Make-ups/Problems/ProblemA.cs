using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MakeUps.Problems;

public class ProblemA
{
    public static void OpenPitMining()
    {
        var N = int.Parse(Console.ReadLine() ?? string.Empty);

        var blocks = new List<Block>();

        for (var i = 0; i < N; i++)
        {
            var line = Console.ReadLine()?.Split(' ').Select(int.Parse).ToArray();

            if (line == null) continue;

            blocks.Add(new Block
            {
                Id = i + 1,
                Value = line[0],
                Cost = line[1],
                Obstructs = line.Skip(3).ToList()
            });
        }

        // Find obstructed by
        for (var i = 0; i < blocks.Count; i++)
        {
            var block = blocks[i];

            foreach (var obstructed in block.Obstructs)
            {
                blocks[obstructed - 1].ObstructedBy.Add(blocks[i]);
            }
        }

        // Find max profit
        var maxProfit = FindMaxProfit(blocks, new List<int>());
        Console.WriteLine(maxProfit);
    }

    private static int FindMaxProfit(List<Block> blocks, List<int> maxVisited)
    {
        int? maxProfit = null;

        var newVisited = new List<int>();
        for (var i = 0; i < blocks.Count; i++)
        {
            if (maxVisited.Contains(i + 1)){

                continue;
            }
            var tempVisited = new List<int>();
            tempVisited.AddRange(maxVisited);
            
            var block = blocks[i];
            var result = block.Profit(tempVisited);

            if (maxVisited.Any() && result.Item1 <= 0){
                continue;
            }

            if (maxProfit == null || maxProfit < result.Item1){
                maxProfit = result.Item1;
                newVisited = result.Item2;
            }
        }

        if (maxProfit == null){
            return 0;
        }
        maxVisited.AddRange(newVisited);

        return (int)maxProfit + FindMaxProfit(blocks, maxVisited);
    }
}

public class Block
{
    public int Id { get; set; }

    public int Value { get; set; }

    public int Cost { get; set; }

    public List<int> Obstructs { get; set; } = new List<int>();

    public List<Block> ObstructedBy { get; set; } = new List<Block>();

    public (int, List<int>) Profit(List<int> visited)
    {
        var profit = Value - Cost;

        if (visited.Contains(Id)){
            return (0, visited);
        }

        visited.Add(Id);

        if (ObstructedBy.Any())
        {
            var obstructedProfit = 0;

            foreach (var obstructed in ObstructedBy)
            {
                if (!visited.Contains(obstructed.Id))
                {
                    var result = obstructed.Profit(visited);

                    obstructedProfit += result.Item1;
                }
            }

            profit += obstructedProfit;
        }

        return (profit, visited);
    }
}