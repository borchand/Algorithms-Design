using System;
using System.Collections.Generic;
using System.Linq;

namespace A3.Problems;

public class ProblemH
{
    public static void ColoringSocks()
    {
        // Read the input
        var input = (Console.ReadLine() ?? string.Empty).Split(" ");

        var socks = int.Parse(input[0]);
        var capacity = int.Parse(input[1]);
        var maxDiff = int.Parse(input[2]);

        var machineCount = 0;
        var currentCapacity = capacity;
        var minVal = 0;

        var colorVals = new List<int>();
        var colorValsInput = (Console.ReadLine() ?? string.Empty).Split(" ");

        foreach (var colorVal in colorValsInput){
            colorVals.Add(int.Parse(colorVal));
        }
        colorVals.Sort();

        for (int i = 0; i < colorVals.Count; i++)
        {

            if (i == 0){
                // first sock
                machineCount++;
                currentCapacity--;
                minVal = colorVals[i];
            } else {
                // if machine is full we reset with new machine or color diff is to big
                if (currentCapacity == 0 || maxDiff < (colorVals[i] - minVal)){
                    machineCount++;
                    currentCapacity = capacity;
                    minVal = colorVals[i];
                }
                // if we can add to machine
                if (currentCapacity > 0 && maxDiff >= (colorVals[i] - minVal)){
                    currentCapacity--;
                }
            }
        }

        // write answer in console
        Console.WriteLine(machineCount);
    }
}