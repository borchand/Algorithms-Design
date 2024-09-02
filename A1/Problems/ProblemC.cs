using System;
using System.Collections.Generic;

namespace A1.Problems;

public class ProblemC
{
	public static void Proplem()
	{
		var cases = int.Parse(Console.ReadLine() ?? string.Empty);

        for (var i = 0; i < cases; i++){
            var trips = int.Parse(Console.ReadLine() ?? string.Empty);

            var set = new HashSet<string>();

            for (var j = 0; j < trips; j++){
                var s = Console.ReadLine() ?? string.Empty;

                set.Add(s);
            }

            Console.WriteLine(set.Count);
        }
	}
}