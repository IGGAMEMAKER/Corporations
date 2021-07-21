using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using static SimpleUI.SimpleUI;

namespace SimpleUI
{
    public partial class SimpleUI : EditorWindow
    {
        //void SaveData()
        //{
        //    SimpleUI.SaveData();
        //}

        //void OpenPrefab(SimpleUISceneType prefab)
        //{
        //    PossiblePrefab = null;
        //    isDraggedPrefabMode = false;
        //    isUrlEditingMode = false;

        //    SimpleUI.OpenPrefab(prefab);
        //}

        //static void OpenPrefab(SimpleUISceneType p)
        //{
        //    newPath = p.AssetPath;
        //    newUrl = p.Url;


        //    isConcreteUrlChosen = true;


        //    LoadReferences(newUrl);

        //    OpenAssetByPath(newPath);
        //}

        #region Render prefabs

        void RenderPrefabs(IEnumerable<SimpleUISceneType> list, string trimStart = "", bool hideUrl = false)
        {
            foreach (var p in list)
            {
                var c = GUI.color;

                // set color
                bool isChosen = hasChosenPrefab && prefabs[ChosenIndex].AssetPath.Equals(p.AssetPath);

                var color = isChosen ? Color.yellow : Color.white;

                //ColorUtility.TryParseHtmlString(isChosen ? "gold" : "white", out Color color);
                //var color = ColorUtility.TryParseHtmlString(isChosen ? "#FFAB04" Visuals.GetColorFromString(isChosen ? Colors.COLOR_YOU : Colors.COLOR_NEUTRAL);

                GUI.contentColor = color;
                GUI.color = color;
                GUI.backgroundColor = color;


                GUIStyle style = GUI.skin.FindStyle("Button");
                style.richText = true;

                string trimmedUrl = p.Url;

                if (trimStart.Length > 0)
                {
                    var lastDashIndex = trimmedUrl.LastIndexOf('/');

                    trimmedUrl = trimmedUrl.Substring(lastDashIndex);
                    //trimmedUrl = trimmedUrl.Trim(trimStart.ToCharArray());
                }

                if (hideUrl)
                {
                    trimmedUrl = "";
                }

                if (GUILayout.Button($"<b>{p.Name}</b>\n{trimmedUrl}", style))
                {
                    OpenPrefab(p);
                }

                // restore colors
                GUI.contentColor = c;
                GUI.color = c;
                GUI.backgroundColor = c;
            }
        }



        void RenderRecentPrefabs()
        {
            var sortedByOpenings = prefabs.OrderByDescending(pp => pp.LastOpened);
            var recent = sortedByOpenings.Take(6);

            GUILayout.Label("Recent prefabs", EditorStyles.boldLabel);
            searchUrl = EditorGUILayout.TextField("Search", searchUrl);

            if (searchUrl.Length == 0)
            {
                RenderPrefabs(recent);
            }
            else
            {
                if (Button("Clear"))
                {
                    searchUrl = "";
                }

                Space();
                RenderPrefabs(sortedByOpenings.Where(p => Contains(p.Url, searchUrl) || Contains(p.Name, searchUrl)));
            }
        }

        void RenderFavoritePrefabs()
        {
            var top = prefabs.OrderByDescending(pp => pp.Usages).Take(4);

            GUILayout.Label("Favorite prefabs", EditorStyles.boldLabel);
            RenderPrefabs(top, "", true);
        }

        void RenderAllPrefabs()
        {
            var top = prefabs.OrderByDescending(pp => pp.Url);

            GUILayout.Label("All prefabs", EditorStyles.boldLabel);
            RenderPrefabs(top);
        }

        void RenderRootPrefab()
        {
            var upperUrl = GetUpperUrl(newUrl);

            bool isTopRoute = newUrl.Equals("/");

            if (!isTopRoute)
            {
                var root = GetPrefabByUrl(upperUrl);

                Label($"Root");

                RenderPrefabs(new List<SimpleUISceneType> { root });
            }
        }

        void RenderSubroutes()
        {
            var subUrls = GetSubUrls(newUrl, false);

            if (subUrls.Any())
            {
                Label("SubRoutes");
                RenderPrefabs(subUrls, newUrl);
            }
        }

        void RenderPrefabs()
        {
            Space();

            RenderFavoritePrefabs();
            RenderRecentPrefabs();
        }
        #endregion


        #region UI shortcuts
        public static bool Button(string text)
        {
            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            if (!text.Contains("\n"))
                text += "\n";

            return GUILayout.Button($"<b>{text}</b>", style);
        }

        public static void Label(string text)
        {
            Space();
            BoldLabel(text);
        }

        public static void BoldLabel(string text)
        {
            GUILayout.Label(text, EditorStyles.boldLabel);
        }

        public static void Space(int space = 15)
        {
            GUILayout.Space(space);
        }

        #endregion

        static void Print(string str)
        {
            Debug.Log(str);
        }

        static void Error(string str)
        {
            Debug.LogError(str);
        }
    }
}