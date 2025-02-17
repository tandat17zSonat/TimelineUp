using System;
using UnityEngine;

public static class Utils
{
    public static string FormatNumber(long num)
    {
        string sign = num < 0 ? "-" : "";
        long absNum = Math.Abs(num);

        if (absNum >= 1_000_000_000)
            return sign + (absNum / 1_000_000_000.0).ToString("0.0") + "b";
        else if (absNum >= 1_000_000)
            return sign + (absNum / 1_000_000.0).ToString("0.0") + "m";
        else if (absNum >= 1_000)
            return sign + (absNum / 1_000.0).ToString("0.0") + "k";
        else
            return sign + absNum.ToString();
    }
}
