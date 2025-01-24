﻿namespace Xeyth.Result.Benchmarks.Extensions;

internal static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        Random random = new();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}