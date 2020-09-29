using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListView : View // MonoBehaviour
{
    public GameObject Prefab;

    public int index = 0;
    public int reversedIndex => Items.Count - index;

    [Header("Specify this field to autoscroll. Two layers higher Scroll View")]
    public ScrollRect _sRect;
    public bool AutoScroll = false;


    // highlight selected elements
    public bool HighlightChosenVariant = false;
    public int ChosenIndex = -1;

    bool noElementsWereSelected => ChosenIndex == -1;
    public bool ForceUpdate = false;
    //

    bool wasCleanedUp = false;

    [HideInInspector]
    public List<GameObject> Items;
    [HideInInspector]
    public GameObject Item;

    void OnDisable()
    {
        ChosenIndex = -1;
        //this.OnDeselect();
        ////Clean();
    }

    //private void OnEnable()
    //{
    //    OnDeselect();
    //}

    void Start()
    {
        Clean();
    }

    public virtual void OnItemSelected(int ind) { }

    public virtual void OnDeselect()
    {
        ChosenIndex = -1;
    }

    internal void ChooseElement(int elementIndex)
    {
        if (ChosenIndex == elementIndex)
        {
            // deselect
            OnDeselect();
        }
        else
        {
            ChosenIndex = elementIndex;

            Item = Items[elementIndex];
            OnItemSelected(ChosenIndex);
        }


        int i = 0;
        foreach (var item in Items)
        {
            bool highlight = noElementsWereSelected || (i == ChosenIndex);

            if (highlight)
                item.GetComponent<CanvasGroup>().alpha = 1;
            else
                item.GetComponent<CanvasGroup>().alpha = 0.25f;

            i++;
        }
    }

    // T is gameEntity in most cases
    // but you can use other data types if you need

    public abstract void SetItem<T>(Transform t, T entity, object data = null);
    //public virtual void DebugEntity<T>(T entity) { }

    public void SetItems<T>(IEnumerable<T> entities, object data = null) => SetItems(entities.ToArray(), data);
    public void SetItems<T>(T[] entities, object data = null)
    {
        Render(entities, gameObject, data);

        if (AutoScroll)
            StartCoroutine(ScrollDown());
    }

    void Clean()
    {
        if (wasCleanedUp)
            return;

        // remove all objects in this list
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        Items = new List<GameObject>();
        wasCleanedUp = true;
    }

    void Render<T>(T[] entities, GameObject Container, object data = null)
    {
        Clean();

        #region check for nulls
        if (entities == null)
            return;

        if (Prefab == null)
        {
            Debug.LogError("No prefab given! " + gameObject.name);
            Debug.Log("No prefab given! " + gameObject.name);

            return;
        }
        #endregion

        index = 0;
        foreach (var e in entities)
        {
            GameObject o = GetObjectForItem();

            SetItem(o.transform, e, data);

            Show(o);

            HighLightChosenElement(o);

            index++;
        }

        for (var i = index; i < Items.Count; i++)
        {
            // there are more elements than needed
            Hide(Items[i]);
        }
    }


    GameObject GetObjectForItem()
    {
        if (index >= Items.Count)
        {
            var o = Instantiate(Prefab, transform, false);
            Items.Add(o);
        }

        return Items[index];
    }

    void HighLightChosenElement(GameObject o)
    {
        if (HighlightChosenVariant)
        {
            if (o.GetComponent<CanvasGroup>() == null)
                o.AddComponent<CanvasGroup>();

            if (o.GetComponent<NotifyListIfElementWasChosen>() == null)
                o.AddComponent<NotifyListIfElementWasChosen>();

            o.GetComponent<NotifyListIfElementWasChosen>().SetEntity(index, this);
        }
    }

    IEnumerator ScrollDown()
    {
        yield return new WaitForSeconds(0.15f);
        Scroll();
    }

    void Scroll()
    {
        _sRect.verticalNormalizedPosition = 0f;
    }
}