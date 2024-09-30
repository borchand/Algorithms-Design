using System;
using System.Collections.Generic;
using System.Linq;

namespace A5.Problems;

public class ProblemK
{
    public static void Knapsack()
    {
        // check if we can read a line
        while(true)
        {
            // Read the input
            var input = (Console.ReadLine() ?? string.Empty).Split(" ");

            if (input.Length <= 1){
                return;
            }
            var capacity = int.Parse(input[0]);
            var n = int.Parse(input[1]);

            var weights = new List<int>();
            var values = new List<int>();

            for(var i = 0; i < n; i++){
                var line = (Console.ReadLine() ?? string.Empty).Split(" ");

                var value = int.Parse(line[0]);
                var weight = int.Parse(line[1]);

                values.Add(value);
                weights.Add(weight);
            }

            Solve(n, capacity, weights, values);
        }
    }

    public static void Solve(int n, int capacity, List<int> weights, List<int> values)
    {
        var M = new Dictionary<(int, int), int>();

        if (n == 1){
            // if there is only one weight no need to calculate
            Console.WriteLine(weights[0]);
            return;
        }

        // initialize the dictionary

        for(var j = 0; j <= capacity; j++){
            M[(-1, j)] = 0;
        }



        for(var i = 0; i < n; i++){
            for(var j = 0; j <= capacity; j++){

                if (weights[i] > j){
                    M[(i, j)] = M[(i - 1, j)];
                }else{
                    M[(i, j)] = Math.Max(M[(i-1, j)], M[(i - 1, j - weights[i])] + values[i]);
                }
            }
        }

        var result = new List<int>();
        // find the items that are in the knapsack
        for(var i = n - 1; i >= 0; i--){
            if (M[(i, capacity)] != M[(i - 1, capacity)]){
                result.Add(i);
                capacity -= weights[i];
            }
        }
    
        Console.WriteLine(result.Count);
        foreach(var item in result){
            Console.Write($"{item} ");
        }
    }
}