using Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class BaseClass : MonoBehaviour
{
    public static List<T> FindObjectsOfTypeAll<T>()
    {
        var results = new List<T>();
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (var j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }

        return results;
    }

    public static void PlaySound(Sound sound)
    {
        SoundManager.Play(sound);
    }


    public bool Contains<T>(GameObject obj = null)
    {
        if (obj == null)
            obj = gameObject;

        return obj.GetComponent<T>() != null;
    }

    public T AddIfAbsent<T>(GameObject obj = null) where T : Component
    {
        if (obj == null)
            obj = gameObject;

        if (!Contains<T>())
            return obj.AddComponent<T>();

        return obj.GetComponent<T>();
    }

    public void RemoveIfExists<T>(GameObject obj = null) where T : Component
    {
        if (obj == null)
            obj = gameObject;

        if (Contains<T>())
            Destroy(obj.GetComponent<T>());
    }

    public void AddOrRemove<T>(GameObject obj = null) where T : Component
    {
        if (obj == null)
            obj = gameObject;

        if (Contains<T>())
            Destroy(obj.GetComponent<T>());
    }


    public void ToggleIsChosenComponent(bool isChosen)
    {
        if (Contains<IsChosenComponent>())
            gameObject.GetComponent<IsChosenComponent>().Toggle(isChosen);
    }

    Dictionary<Type, GameObject> CachedObjects = new Dictionary<Type, GameObject>();

    public T Find<T>()
    {
        var t = typeof(T);
        if (!CachedObjects.ContainsKey(t))
        {
            CachedObjects[t] = (GameObject) FindObjectOfType(typeof(T));
        }

        // https://stackoverflow.com/questions/1003023/cast-to-generic-type-in-c-sharp
        return (T) Convert.ChangeType(CachedObjects[t], typeof(T));
        //return (T)CachedObjects[t];
    }

    // -----------------------------

    public void Draw(MonoBehaviour mb, bool condition) => Draw(mb.gameObject, condition);
    public void Draw(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }

    public void ShowOnly(GameObject obj, GameObject[] objects) => ShowOnly(obj, objects.ToList());
    public void ShowOnly(GameObject obj, List<GameObject> objects)
    {
        foreach (var o in objects)
        {
            Draw(o, o.GetInstanceID() == obj.GetInstanceID());
        }
    }

    public void DrawCanvasGroup(GameObject go, bool condition)
    {
        var group = go.GetComponent<CanvasGroup>();

        if (group != null)
        {
            DrawCanvasGroup(group, condition);
        }
    }
    public void DrawCanvasGroup(CanvasGroup group, bool condition)
    {
        group.alpha = condition ? 1f : 0;
        //group.interactable = condition;
        group.blocksRaycasts = condition;
    }

    public void Show(MonoBehaviour mb) => Draw(mb.gameObject, true);
    public void Show(GameObject go) => Draw(go, true);

    public void Hide(MonoBehaviour mb) => Draw(mb.gameObject, false);
    public void Hide(GameObject go) => Draw(go, false);

    public void HideAll(params GameObject[] objects) => HideAll(objects.ToList());
    public void HideAll(IEnumerable<GameObject> objects)
    {
        foreach (var b in objects)
            Hide(b);
    }

    public void ShowAll(params GameObject[] objects) => ShowAll(objects.ToList());
    public void ShowAll(IEnumerable<GameObject> objects)
    {
        foreach (var b in objects)
            Show(b);
    }

    // Animations
    AnimationSpawner AnimationSpawner;
    public void Animate(string text)
    {
        Animate(text, transform);
    }

    public void Animate(string text, Transform t)
    {
        if (AnimationSpawner == null)
            AnimationSpawner = FindObjectOfType<AnimationSpawner>();

        if (AnimationSpawner != null)
            AnimationSpawner.Spawn(text, t);
    }

    public void Animate(string text, GameObject obj)
    {
        if (AnimationSpawner == null)
            AnimationSpawner = FindObjectOfType<AnimationSpawner>();

        if (AnimationSpawner != null)
            AnimationSpawner.Spawn(text, obj.transform);
    }
}