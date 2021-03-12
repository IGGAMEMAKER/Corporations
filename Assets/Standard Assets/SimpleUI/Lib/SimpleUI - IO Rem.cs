using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

namespace SimpleUI
{
    // Save/Load info
    public partial class SimpleUI
    {
        static string GetCallerName(int skipFrames)
        {
            skipFrames++;
            var frame = new StackFrame(skipFrames);

            if (frame.GetMethod().Name.Equals("get_SimpleUI"))
            {
                return GetCallerName(skipFrames + 1);
            }

            return $"{frame.GetMethod().Name}";
        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            // https://answers.unity.com/questions/1425758/how-can-i-find-all-instances-of-a-scriptable-objec.html

            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }
    }
}