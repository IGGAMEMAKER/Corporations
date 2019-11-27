using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    public static class RandomEnum<T>
    {
        public static T GenerateValue(T exception)
        {
            T result = GenerateValue();
            Debug.Log("Generate value: " + result);
            while (result.Equals(exception))
                result = GenerateValue();

            return result;
        }

        //public static T GenerateValue(List<T> exceptions)
        //{
        //    T result = GenerateValue();

        //    while (exceptions.Contains(result))
        //        result = GenerateValue();

        //    return result;
        //}

        public static T GenerateValue()
        {
            Array array = Enum.GetValues(typeof(T));

            return PickRandomItem(array);
        }

        public static T PickRandomItem(Array array)
        {
            int id = UnityEngine.Random.Range(0, array.Length);

            return (T)array.GetValue(id);
        }
    }
}
