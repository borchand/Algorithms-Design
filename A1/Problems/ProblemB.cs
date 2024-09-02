using System;
using System.Collections.Generic;
using System.Linq;

namespace A1.Problems;

public class ProblemB
{
	public static void Backspace()
	{
		var input = Console.ReadLine() ?? string.Empty;

        var stack = new Stack<char>();


        foreach (var c in input)
        {      
            if (c == '<') {
                // we assume that backspace is never pressed in an emtpy line.
                stack.Pop();
            }
            else
            {
                stack.Push(c);
            }
        }

        var output = stack.Reverse().ToArray();

        Console.WriteLine(output);
	}
}