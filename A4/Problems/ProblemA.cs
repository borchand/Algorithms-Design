using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace A4.Problems;

public class Point
{
    public Point(
        double x,
        double y
    )
    {
        X = x;
        Y = y;
    }

    public string ToString(NumberFormatInfo nfi)
    {
        return X.ToString(nfi) + " " + Y.ToString(nfi);
    }

    public double X { get; set; }

    public double Y { get; set; }
}

public class ClosestPairPoint
{
    public ClosestPairPoint(
        Point p1,
        Point p2
    )
    {
        P1 = p1;
        P2 = p2;
    }

    public Point P1 { get; set; }

    public Point P2 { get; set; }
}

public class ProblemA
{
    public static void ClosestPair()
    {
        // Solves problem with reading the decimals correctly
        NumberFormatInfo nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        // Read the input
        var input = int.Parse(Console.ReadLine() ?? string.Empty);

        var points = new Point[input];

        for(var i = 0; i < input; i++)
        {
            var point = (Console.ReadLine() ?? string.Empty).Split(" ");
            var x = double.Parse(point[0], nfi);
            var y = double.Parse(point[1], nfi);

            points[i] = new Point(x, y);
        }

        points = points.OrderBy(p => p.X).ToArray();

        var closestPoints = FindClosestPair(points).Item1;

        Console.WriteLine(closestPoints.P1.ToString(nfi));
        Console.WriteLine(closestPoints.P2.ToString(nfi));        
    }

    private static (ClosestPairPoint, double) FindClosestPair(Point[] lst){

        var n = lst.Length;

        if (n < 4){
            return FindSmallestEuclideanDistance(lst);
        }

        var mid = n / 2;

        var left = FindClosestPair(lst.Take(mid).ToArray());
        var right = FindClosestPair(lst.Skip(mid).ToArray());

        if (left.Item2 == 0 || right.Item2 == 0)
        {
            // No need to continue we found to equal points
            return left.Item2 == 0 ? left : right;
        }
        else
        {
            var minD = left.Item2 > right.Item2 ? right.Item2 : left.Item2;

            var closestPoints = left.Item2 > right.Item2 ? right : left;

            var listAroundL = lst.Where(p => Math.Abs(p.X - lst[mid].X) < Math.Sqrt(minD)).ToArray();

            var middel = FindSmallestEuclideanDistanceMiddel(listAroundL, minD);

            if (middel.Item2 < minD)
            {
                closestPoints = middel;
            }

            return closestPoints;
        }
    }

    private static (ClosestPairPoint, double) FindSmallestEuclideanDistance(Point[] points)
    {
        double smallestEuclideanDistance = double.MaxValue;
        var closestPoints = new ClosestPairPoint(new Point(0, 0), new Point(0, 0));

        
        for (var i = 0; i < points.Length; i++)
        {
            var p1 = points[i]; 
            for (var j = i + 1; j < points.Length; j++)
            {
                var p2 = points[j];
                var d = EuclideanDistance(p1, p2);

                if (smallestEuclideanDistance > d)
                {
                    smallestEuclideanDistance = d;
                    closestPoints = new ClosestPairPoint(p1, p2);
                }
            }
        }

        return (closestPoints, (double)smallestEuclideanDistance!);
    }
    
    
    private static (ClosestPairPoint, double) FindSmallestEuclideanDistanceMiddel(Point[] points, double minD)
    {
        double smallestEuclideanDistance = minD;
        var closestPoints = new ClosestPairPoint(new Point(0, 0), new Point(0, 0));

        points = points.OrderBy(p => p.Y).ToArray();
        
        for (var i = 0; i < points.Length; i++)
        {
            var p1 = points[i]; 
            for (var j = i + 1; j < points.Length && (points[j].Y - p1.Y < Math.Sqrt(smallestEuclideanDistance)); j++)
            {
                var p2 = points[j];
                var d = EuclideanDistance(p1, p2);

                if (smallestEuclideanDistance > d)
                {
                    smallestEuclideanDistance = d;
                    closestPoints = new ClosestPairPoint(p1, p2);
                }
            }
        }

        return (closestPoints, (double)smallestEuclideanDistance!);
    }

    private static double EuclideanDistance(Point p1, Point p2)
    {
        var x = p2.X - p1.X;
        var y = p2.Y - p1.Y;
        return x * x + y * y;
    }
}