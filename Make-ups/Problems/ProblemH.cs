using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MakeUps.Problems;

public class ProblemH
{
    public static HashSet<(int, int)> Visited = new HashSet<(int, int)>();

    public static void RobotsOnAGrid()
    {
        // the grid is a n x n grid
        int n = int.Parse(Console.ReadLine() ?? string.Empty);

        List<List<char>> grid = new List<List<char>>();

        for (int i = 0; i < n; i++)
        {
            var line = Console.ReadLine()?.ToCharArray().ToList() ?? new List<char>();

            grid.Add(line);
        }

        // find the number of paths starting from the top left corner to the bottom right corner only moving right or down
        var result = FindPaths(grid);

        if (result == 0)
        {
            if (DFS(grid))
            {
                Console.WriteLine("THE GAME IS A LIE");
            }
            else
            {
                Console.WriteLine("INCONCEIVABLE");
            }
        }
        else
        {
            // result modulo 2^31 - 1
            Console.WriteLine(result);
        }
    }

    private static bool DFS(List<List<char>> grid){
        var stack = new Stack<(int, int)>();
        stack.Push((0, 0));

        while (stack.Count > 0)
        {
            var (x, y) = stack.Pop();

            if (x >= grid.Count || y >= grid.Count || x < 0 || y < 0)
            {
                continue;
            }

            if (grid[x][y] == '#')
            {
                continue;
            }

            if (x == grid.Count - 1 && y == grid.Count - 1)
            {
                return true;
            }

            if (Visited.Contains((x, y)))
            {
                continue;
            }

            Visited.Add((x, y));

            stack.Push((x + 1, y));
            stack.Push((x, y + 1));
            stack.Push((x - 1, y));
            stack.Push((x, y - 1));
        }

        return false;
    }

    private static long FindPaths(List<List<char>> grid)
    {
        var result = new Dictionary<(int, int), long>();

        for (var i = 0; i < grid.Count(); i++)
        {
            for (var j = 0; j < grid.Count(); j++)
            {
                if (grid[i][j] == '#')
                {
                    result[(i, j)] = 0;
                }
                else if (i == 0 && j == 0)
                {
                    result[(i, j)] = 1;
                }
                else
                {
                    result[(i, j)] = (result.GetValueOrDefault((i - 1, j), 0) + result.GetValueOrDefault((i, j - 1), 0)) % (long)(Math.Pow(2, 31) - 1);
                }
            }
        }

        return result[(grid.Count - 1, grid.Count - 1)];
    }
}