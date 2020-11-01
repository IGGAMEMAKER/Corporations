using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButtonsContainer : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        //AttachComponent(t, entity as System.Type);
        var TT = entity as System.Type;

        //while (t.gameObject.GetComponents(TT).Length > 0)
        //{
        //    Destroy(t.gameObject.GetComponent(TT));
        //}

        if (t.gameObject.GetComponent(TT) == null)
            t.gameObject.AddComponent(TT);
    }

    //void AttachComponent<T>(Transform t, T type) where T : System.Type
    //{
    //    t.gameObject.AddComponent(type);
    //}

    public void SetComponents(List<System.Type> components)
    {
        SetItems(components.ToArray());
    }
}
