using UnityEngine;

public abstract class ListView : View // MonoBehaviour
{
    public GameObject Prefab;

    // T is gameEntity in most cases
    // but you can use other data types if you need

    public abstract void SetItem<T>(Transform t, T entity, object data = null);
    //public virtual void DebugEntity<T>(T entity) { }

    public void SetItems<T>(T[] entities, object data = null)
    {
        Render(entities, gameObject, data);
    }

    void Render<T>(T[] entities, GameObject Container, object data = null)
    {
        // remove all objects in this list
        foreach (Transform child in transform)
            Destroy(child.gameObject);


        //for (int i = 0; i < entities.Length; i++)
        if (entities == null)
            return;

        foreach (var e in entities)
        {
            if (Prefab == null)
            {
                Debug.Log("No prefab given! " + gameObject.name);
                return;
            }
            var o = Instantiate(Prefab, transform, false);

            SetItem(o.transform, e, data);
        }
    }
}