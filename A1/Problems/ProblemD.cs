using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A1.Problems;

public class ProblemD
{
	public static void Proplem()
	{
		var input = Console.ReadLine() ?? string.Empty;
        var listInput = Console.ReadLine() ?? string.Empty;

        var temp = input.Split(" ");

        var n = int.Parse(temp[0]);
        var t = int.Parse(temp[1]);

        var list = listInput.Split(" ").Select(int.Parse).ToList();

        switch (t)
        {
            case 1:
                Console.WriteLine(7);
            break;
            case 2:
                if (list[0] > list[1]){
                    Console.WriteLine("Bigger");
                } else if (list[0] == list[1]){
                    Console.WriteLine("Equal");
                } else {
                    Console.WriteLine("Smaller");
                }
            break;
            case 3:
                var tempList = new List<int>{list[0], list[1], list[2]};
                tempList.Sort();
                Console.WriteLine(tempList[1]);
            break;
            case 4:
                Console.WriteLine(list.Sum());
            break;
            case 5:
                Console.WriteLine(list.Where(x => x % 2 == 0).Sum());
            break;
            case 6:
                // 65 is 'A' in ASCII
                var charArray = list.Select(x => (char)((x % 26) + 65)).ToArray();

                Console.WriteLine(new String(charArray).ToLower());
            break;
            case 7:
                var visited = new List<int>();
                var done = false;

                var idx = list[0];
                visited.Add(idx);

                while (!done){
                    var nextIdx = list[idx];

                    if (visited.Contains(nextIdx)){
                        Console.WriteLine("Cyclic");
                        done = true;
                    }
                    else if (nextIdx < 0 || nextIdx >= list.Count){
                        Console.WriteLine("Out");
                        done = true;
                    }
                    else if (nextIdx == list.Count - 1){
                        Console.WriteLine("Done");
                        done = true;
                    } 
                    else {
                        idx = nextIdx;
                    }

                }
            break;
        }

        
	}
}