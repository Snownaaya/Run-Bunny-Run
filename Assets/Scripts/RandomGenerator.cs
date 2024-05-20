using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerator
{
    private static System.Random s_random = new System.Random();

    public static int Range(int minimum, int maximum) => s_random.Next(minimum, maximum);

    //public static T Choose<T>(List<T> list)
    //{
    //    if (list == null || list.Count == 0)
    //    {
    //        return default(T);
    //    }

    //    int index = Range(0, list.Count);
    //    return list[index];
    //}
}
