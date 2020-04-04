//using UnityEngine;
//using UnityEditor;
//using System.Collections;
//using System.Linq;
//using System.Reflection;
//using System;

//[InitializeOnLoad]
//public class NewScriptDefaultPathWindow : EditorWindow
//{
//    #region Reflection magic
//    static System.Type AddComponentWindowType = null;
//    static System.Type NewScriptElement = null;
//    static FieldInfo s_AddComponentWindow = null;
//    static FieldInfo m_Tree = null;
//    static FieldInfo m_Directory = null;

//    static EditorWindow m_CurrentWindow = null;
//    static object m_CurrentElement = null;

//    static NewScriptDefaultPathWindow()
//    {
//        var types = typeof(EditorWindow).Assembly.GetTypes();
//        AddComponentWindowType = types.Where(t => t.Name == "AddComponentWindow").FirstOrDefault();
//        s_AddComponentWindow = AddComponentWindowType.GetField("s_AddComponentWindow", BindingFlags.NonPublic | BindingFlags.Static);
//        m_Tree = AddComponentWindowType.GetField("m_Tree", BindingFlags.NonPublic | BindingFlags.Instance);

//        var nestedTypes = AddComponentWindowType.GetNestedTypes(BindingFlags.NonPublic);

//        Debug.Log("Fields");
//        foreach (var f in (BindingFlags[])Enum.GetValues(typeof(BindingFlags)))
//        {
//            var types2 = AddComponentWindowType.GetNestedTypes(f);
//            var count = types2.Count();

//            Debug.Log("Flag<" + f.ToString() + $"> ({count}): " + string.Join(",", types2.Select(t => t.Name)));
//        }

//        NewScriptElement = nestedTypes.Where(t => t.Name == "NewScriptElement").FirstOrDefault();

//        Debug.Log("nested types: " + string.Join(",", nestedTypes.Select(t => t.Name)));

//        m_Directory = NewScriptElement.GetField("m_Directory");

//        EditorApplication.update += BackgroundCheck;
//    }

//    static EditorWindow GetAddComponentWindow()
//    {
//        var inst = (EditorWindow)s_AddComponentWindow.GetValue(null);
//        return inst;
//    }

//    static object GetNewScriptElement()
//    {
//        var window = GetAddComponentWindow();
//        if (window == null)
//        {
//            m_CurrentWindow = null;
//            m_CurrentElement = null;
//            return null;
//        }
//        if (window == m_CurrentWindow)
//        {
//            return m_CurrentElement;
//        }
//        m_CurrentWindow = window;
//        System.Array a = (System.Array)m_Tree.GetValue(window);
//        var list = a.OfType<object>().ToArray();
//        for (int i = 0; i < list.Length; i++)
//        {
//            if (list[i].GetType() == NewScriptElement)
//            {
//                m_CurrentElement = list[i];
//                return m_CurrentElement;
//            }
//        }
//        return null;
//    }
//    static string Directory
//    {
//        get
//        {
//            var element = GetNewScriptElement();
//            if (element == null)
//                return "";
//            return (string)m_Directory.GetValue(element);
//        }
//        set
//        {
//            var element = GetNewScriptElement();
//            if (element == null)
//                return;
//            m_Directory.SetValue(element, value);
//        }
//    }
//    #endregion Reflection magic

//    [MenuItem("Tools/Default script path settings")]
//    static void Init()
//    {
//        var win = CreateInstance<NewScriptDefaultPathWindow>();

//        win.ShowUtility();
//    }
//    static string dir = "";
//    static string currentDir = "";
//    static bool enableBackgroundCheck = false;
//    static int counter = 0;
//    static NewScriptDefaultPathWindow instance;
//    static bool initialized = false;

//    static void LoadSettings()
//    {
//        dir = EditorPrefs.GetString("NewScriptDefaultPath", "Views");
//        enableBackgroundCheck = EditorPrefs.GetBool("NewScriptDefaultPath_Enabled", false);
//    }

//    static void BackgroundCheck()
//    {
//        if (!initialized)
//        {
//            initialized = true;
//            LoadSettings();
//        }
//        if (enableBackgroundCheck)
//        {
//            // check only once a second
//            if (++counter > 100)
//            {
//                counter = 0;
//                Directory = dir;
//                if (instance != null)
//                {
//                    string tmp = Directory;
//                    if (tmp != currentDir)
//                    {
//                        currentDir = tmp;
//                        instance.Repaint();
//                    }
//                }
//            }
//        }
//    }

//    void OnEnable()
//    {
//        LoadSettings();
//        instance = this;
//        titleContent = new GUIContent("Edit default script path");
//    }

//    void OnDisable()
//    {
//        instance = null;
//    }
//    Color GetColor(bool aActive)
//    {
//        if (aActive)
//            return Color.green;
//        return Color.red;
//    }


//    void OnGUI()
//    {
//        Color oldColor = GUI.color;
//        GUI.changed = false;
//        enableBackgroundCheck = GUILayout.Toggle(enableBackgroundCheck, "enable background check", "button");
//        GUILayout.BeginHorizontal();
//        GUILayout.Label("Default script save path:", GUILayout.Width(150));
//        dir = GUILayout.TextField(dir);
//        GUILayout.EndHorizontal();
//        GUILayout.BeginHorizontal();
//        GUILayout.Label("Background check is ");
//        GUI.color = GetColor(enableBackgroundCheck);
//        GUILayout.Label((enableBackgroundCheck ? "enabled" : "disabled"), "box");
//        GUI.color = oldColor;
//        GUILayout.FlexibleSpace();
//        GUILayout.EndHorizontal();

//        if (GUI.changed)
//        {
//            EditorPrefs.SetString("NewScriptDefaultPath", dir);
//            EditorPrefs.SetBool("NewScriptDefaultPath_Enabled", enableBackgroundCheck);
//        }
//        if (enableBackgroundCheck && dir != "")
//        {
//            GUI.color = GetColor(currentDir == dir);
//            if (currentDir == dir)
//                GUILayout.Label("Directory successfully set", "box");
//            else
//                GUILayout.Label(" - Window currently not open - ", "box");
//            GUI.color = oldColor;
//        }
//        GUILayout.FlexibleSpace();
//        GUILayout.BeginHorizontal();
//        GUILayout.FlexibleSpace();
//        if (GUILayout.Button("Close"))
//        {
//            Close();
//        }
//        GUILayout.Space(15);
//        GUILayout.EndHorizontal();
//        GUILayout.Space(15);
//    }
//}