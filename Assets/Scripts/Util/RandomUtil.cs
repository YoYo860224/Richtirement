using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RandomUtil
{
    public static System.Random random = new System.Random();

    public static double normal()
    {
        double X = random.NextDouble();
        double Y = random.NextDouble();

        return Math.Sqrt(-2.0 * Math.Log(X)) * Math.Cos(2.0 * Math.PI * Y);
    }
}
