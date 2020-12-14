using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListView : View // MonoBehaviour
{
    public GameObject Prefab;

    public bool ForceCleanup = false;

    [HideInInspector]
    public int index = 0;
    [HideInInspector]
    public int count = 0;
    [HideInInspector]
    public int reversedIndex => count - index;

    [Header("Specify this field to autoscroll. Two layers higher Scroll View")]
    public ScrollRect _sRect;
    public bool AutoScroll = false;


    // highlight selected elements
    public bool HighlightChosenVariant = false;
    [HideInInspector]
    public int ChosenIndex = -1;

    bool noElementsWereSelected => ChosenIndex == -1;
    //

    bool wasCleanedUp = false;

    [HideInInspector]
    public List<GameObject> Items;
    [HideInInspector]
    public GameObject Item;

    private bool listWasChanged;
    
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

    public virtual void OnDeselectAll()
    {
        ChosenIndex = -1;
    }
    public virtual void OnDeselect(int ind)
    {
        ChosenIndex = -1;
    }

    internal void ChooseElement(int elementIndex)
    {
        if (ChosenIndex == elementIndex)
        {
            //if (elementIndex >= 0)
            //    Item = Items[ChosenIndex];
            
            // deselect
            OnDeselect(ChosenIndex);
        }
        else
        {
            ChosenIndex = elementIndex;

            Item = Items[ChosenIndex];
            OnItemSelected(ChosenIndex);
        }

        if (HighlightChosenVariant)
        {
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
    }

    public virtual void OnListChanged()
    {
        listWasChanged = false;
    }

    // T is gameEntity in most cases
    // but you can use other data types if you need

    public abstract void SetItem<T>(Transform t, T entity);
    //public virtual void DebugEntity<T>(T entity) { }

    public void SetItems<T>(IEnumerable<T> entities) => SetItems(entities.ToArray());
    public void SetItems<T>(T[] entities)
    {
        Render(entities, gameObject);

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

    void Render<T>(T[] entities, GameObject Container)
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
        count = entities.Count();

        foreach (var e in entities)
        {
            GameObject o = GetObjectForItem();

            SetItem(o.transform, e);

            Show(o);

            HighLightChosenElement(o);

            index++;
        }

        for (var i = index; i < Items.Count; i++)
        {
            // there are more elements than needed
            Hide(Items[i]);

            listWasChanged = true;
        }

        if (listWasChanged)
        {
            OnListChanged();
        }
    }

    GameObject _NewInstance => Instantiate(Prefab, transform, false);

    GameObject GetObjectForItem()
    {
        if (index >= Items.Count)
        {
            //var o = Instantiate(Prefab, transform, false);
            Items.Add(_NewInstance);

            listWasChanged = true;
        }
        else
        {
            if (ForceCleanup)
            {
                Destroy(Items[index]);

                Items.RemoveAt(index);

                Items.Insert(index, _NewInstance);

                //return Items[index];
            }
        }

        return Items[index];
    }

    void HighLightChosenElement(GameObject o)
    {
        if (HighlightChosenVariant)
        {
            if (o.GetComponent<CanvasGroup>() == null)
                o.AddComponent<CanvasGroup>();
        }

            if (o.GetComponent<NotifyListIfElementWasChosen>() == null)
                o.AddComponent<NotifyListIfElementWasChosen>();

            o.GetComponent<NotifyListIfElementWasChosen>().SetEntity(index, this);
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