using System;
using System.Collections.Generic;
using System.Linq;

namespace A2.Problems;

public class ProblemA {
    public static void StablePerfectMatching() {
        var input = Console.ReadLine() ?? string.Empty;

        var n = int.Parse(input.Split(" ")[0]);

        var m = int.Parse(input.Split(" ")[1]);

        var group1 = new Dictionary<string, List<string>>();
        var group2 = new Dictionary<string, List<string>>();

        for (int i = 0; i < n; i++) {
            var line = Console.ReadLine() ?? string.Empty;

            var vals = line.Split(" ");

            if (n / 2 > i) {
                group1[vals[0]] = new List<string>();
            } else {
                group2[vals[0]] = new List<string>();
            }

            for (var j = 1; j < vals.Length; j++) {
                
                if (n / 2 > i) {
                    group1[vals[0]].Add(vals[j]);
                } else {
                    group2[vals[0]].Add(vals[j]);
                }
            }
        }


        var matches = new Dictionary<string, string>();

        
        while (group1.Aggregate(0, (acc, x) => acc + x.Value.Count) > 0 && matches.Count != n / 2) {

            var p = group1.First(x => !matches.ContainsKey(x.Key));

            var r = p.Value.First();
            p.Value.RemoveAt(0);

            if (!matches.ContainsValue(r)){

                matches.Add(p.Key, r);
            } else {
                var pm = matches.First(x => x.Value == r).Key;

                var pmIndex = group2[r].IndexOf(pm);
                var pIndex = group2[r].IndexOf(p.Key);

                if (pIndex < pmIndex){
                    matches.Remove(pm);
                    matches.Add(p.Key, r);
                }
            }
        }

        foreach (var key in matches.Keys) {
            Console.WriteLine($"{key} {matches[key]}");
        }
    }
}