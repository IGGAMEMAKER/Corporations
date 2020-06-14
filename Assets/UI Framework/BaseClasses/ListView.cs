using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListView : View // MonoBehaviour
{
    public GameObject Prefab;

    public int index = 0;

    [Header("Specify this field to autoscroll. Two layers higher Scroll View")]
    public ScrollRect _sRect;
    public bool AutoScroll = false;

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

    void Render<T>(T[] entities, GameObject Container, object data = null)
    {
        // remove all objects in this list
        foreach (Transform child in transform)
            Destroy(child.gameObject);


        //for (int i = 0; i < entities.Length; i++)
        if (entities == null)
            return;

        index = 0;
        foreach (var e in entities)
        {
            if (Prefab == null)
            {
                Debug.Log("No prefab given! " + gameObject.name);
                return;
            }
            var o = Instantiate(Prefab, transform, false);

            SetItem(o.transform, e, data);
            index++;
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