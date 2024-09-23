using System;
using System.Collections.Generic;
using System.Linq;

namespace A5.Problems;

public class ProblemA
{
    public static void WallacetheWeightliftingWalrus()
    {
        // Read the input
        var n = int.Parse(Console.ReadLine() ?? string.Empty);

        var weights = new List<int>();

        for(var i = 0; i < n; i++){
            var weight = int.Parse(Console.ReadLine() ?? string.Empty);

            weights.Add(weight);
        }

        var M = new Dictionary<(int, int), int>();


        if (n == 1){
            // if there is only one weight no need to calculate
            Console.WriteLine(weights[0]);
            return;
        }

        // capacity
        var capacity = 1000;
        var extraCapacity = capacity + weights.Max();

        // initialize the dictionary

        for(var j = 0; j <= extraCapacity; j++){
            M[(-1, j)] = 0;
        }

        var closestWeight = 0;
        for(var i = 0; i < n; i++){
            for(var j = 0; j <= extraCapacity; j++){

                if (weights[i] > j){
                    M[(i, j)] = M[(i - 1, j)];
                }else{
                    M[(i, j)] = Math.Max(M[(i-1, j)], M[(i - 1, j - weights[i])] + weights[i]);
                }

                if (Math.Abs(M[(i, j)] - capacity) <= Math.Abs(closestWeight - capacity)){
                    closestWeight = M[(i, j)];
                }

            }
        }

        Console.WriteLine(closestWeight);
    }
}