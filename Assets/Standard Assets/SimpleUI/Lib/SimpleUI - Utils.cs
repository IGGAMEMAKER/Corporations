using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SimpleUI
{
    public partial class SimpleUI
    {
        #region debugging
        static string Measure(DateTime start) => Measure(start, DateTime.Now);
        static string Measure(DateTime start, DateTime end)
        {
            var milliseconds = (end - start).TotalMilliseconds;

            return $"{milliseconds:0}ms";
        }

        static void Print2(string text)
        {
            //Print("PRT2: " + text);
        }

        static void Print(string text)
        {
            Debug.Log(text);
        }

        static void BoldPrint(string text)
        {
            Debug.Log($"<b>{text}</b>");
        }
        #endregion



        #region string utils

        public static IEnumerable<SimpleUISceneType> GetSubUrls(string url, bool recursive) => prefabs.Where(p => isSubRouteOf(p.Url, url, recursive));

        /// <summary>
        /// if recursive == false
        /// function will return true ONLY for DIRECT subroutes
        /// 
        /// otherwise it will return true for sub/sub routes too
        /// </summary>
        /// <param name="subUrl"></param>
        /// <param name="root"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static bool isSubRouteOf(string subUrl, string root, bool recursive)
        {
            // searching for /ProjectScreen descendants

            // filter /Abracadabra
            if (!subUrl.StartsWith(root))
                return false;

            // filter self
            if (subUrl.Equals(root))
                return false;

            var subStr = subUrl.Substring(root.Length);

            if (root.Equals("/"))
                subStr = subUrl;

            bool isUrlItself = subStr.IndexOf('/') == 0;

            // filter /ProjectScreenBLAH
            if (!isUrlItself)
                return false;

            // remaining urls are
            // /ProjectScreen/Blah
            // /ProjectScreen/Blah/Blah

            // subStrs
            // /Blah
            // /Blah/Blah

            bool isDirectSubroute = subStr.LastIndexOf('/') == 0;
            bool isSubSubRoute = subStr.LastIndexOf('/') > 0;

            if (!recursive)
                return isDirectSubroute;
            else
                return true;
        }

        public static string GetPrettyNameFromAssetPath(string path)
        {
            var x = path.Split('/').Last();
            var ind = x.LastIndexOf(".prefab");

            if (isSceneAsset(x))
            {
                ind = x.LastIndexOf(".unity");
            }

            return x.Substring(0, ind);
        }

        public static string GetPrettyNameForExistingUrl(string url)
        {
            var prefab = GetPrefabByUrl(url);

            return prefab.Name;
        }

        public static string GetUpperUrl(string url)
        {
            var index = url.LastIndexOf("/");

            if (index <= 0)
                return "/";

            return url.Substring(0, index);
        }

        internal static bool Contains(string text1, string searching)
        {
            return text1.ToLower().Contains(searching.ToLower());
        }

        public static string GetValidatedUrl(string url)
        {
            if (!url.StartsWith("/"))
                return url.Insert(0, "/");

            return url;
        }

        #endregion

        public static bool IsAssetPathExists(string path)
        {
            return prefabs.Any(p => p.AssetPath.Equals(path));
        }

        public static bool IsUrlExist(string url)
        {
            return prefabs.Any(p => p.Url.Equals(url));
        }
    }
}