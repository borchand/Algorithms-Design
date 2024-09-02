using System;

namespace A1.Problems;

public class ProblemA
{
	public static void EchoInputThreeTimes()
	{
		var input = Console.ReadLine() ?? string.Empty;

        var times = 3;
        for (int i = 0; i < times; i++)
        {
            Console.Write(input);
            if (i != times - 1)
            {
                Console.Write(" ");
            }
        }
	}
}