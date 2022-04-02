using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class Extensions
{
    private static Random rng = new Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


public class Parameters
{
    public enum GameType
    {
        Basic
    }

    public abstract class CardParameter
    {
        
    }
    public class CardParameter<T> : CardParameter where T : struct
    {
        public string Key;
        public T Min;
        public T Max;

        public CardParameter(string key, T min, T max)
        {
            Key = key;
            Min = min;
            Max = max;
        }
    }
}
