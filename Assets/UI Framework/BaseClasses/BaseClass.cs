using Assets.Core;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

public partial class BaseClass : MonoBehaviour
{
    // data
    public static GameContext Q => Contexts.sharedInstance.game;

    public int CurrentIntDate => ScheduleUtils.GetCurrentDate(Q);


    public GameEntity SelectedCompany => ScreenUtils.GetSelectedCompany(Q);

    public NicheType SelectedNiche => ScreenUtils.GetSelectedNiche(Q);

    // TODO REMOVE
    public IndustryType SelectedIndustry => ScreenUtils.GetSelectedIndustry(Q);

    public GameEntity SelectedHuman => ScreenUtils.GetSelectedHuman(Q);
    public int SelectedTeam => ScreenUtils.GetSelectedTeam(Q);
    
    // TODO REMOVE
    public GameEntity SelectedInvestor => ScreenUtils.GetSelectedInvestor(Q);

    public ScreenMode CurrentScreen => ScreenUtils.GetMenu(Q).menu.ScreenMode;



    public GameEntity Hero => ScreenUtils.GetPlayer(Q);


    public GameEntity MyGroupEntity => Companies.GetPlayerControlledGroupCompany(Q);
    public GameEntity MyCompany => MyGroupEntity ?? null;
    public GameEntity Flagship => Companies.GetFlagship(Q, MyCompany) ?? null;

    public bool HasCompany => MyCompany != null;

    //
    // GameObjects
    public bool Contains<T>()
    {
        return gameObject.GetComponent<T>() != null;
    }

    public T AddIfAbsent<T>() where T : Component
    {
        if (!Contains<T>())
            return gameObject.AddComponent<T>();

        return gameObject.GetComponent<T>();
    }

    public void RemoveIfExists<T>() where T : Component
    {
        if (Contains<T>())
            Destroy(gameObject.GetComponent<T>());
    }

    public void AddOrRemove<T>() where T : Component
    {
        if (Contains<T>())
            Destroy(gameObject.GetComponent<T>());
    }



    public void ToggleIsChosenComponent(bool isChosen)
    {
        if (Contains<IsChosenComponent>())
            gameObject.GetComponent<IsChosenComponent>().Toggle(isChosen);
    }


    GameEntity _Company;

    public GameEntity GetFollowableCompany()
    {
        if (_Company == null)
        {
            var c = GetComponentInParent<FollowableCompany>();

            if (c == null)
                return null;
            else
                _Company = c.Company;
        }

        return _Company;
    }

    Dictionary<Type, GameObject> CachedObjects = new Dictionary<Type, GameObject>();

    public T Find<T>()
    {
        var t = typeof(T);
        if (!CachedObjects.ContainsKey(t))
        {
            CachedObjects[t] = (GameObject)FindObjectOfType(typeof(T));
        }

        // https://stackoverflow.com/questions/1003023/cast-to-generic-type-in-c-sharp
        return (T)Convert.ChangeType(CachedObjects[t], typeof(T));
        //return (T)CachedObjects[t];
    }

    // -----------------------------

    public void Draw(MonoBehaviour mb, bool condition) => Draw(mb.gameObject, condition);
    public void Draw(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }

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

    public void HideAll(List<GameObject> objects)
    {
        foreach (var b in objects)
            Hide(b);
    }

    public void ShowAll(List<GameObject> objects)
    {
        foreach (var b in objects)
            Show(b);
    }
}
