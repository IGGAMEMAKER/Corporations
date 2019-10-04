using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButtonsContainer : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        AttachComponent(t, entity as Component);
    }

    void AttachComponent<T>(Transform t, T Component) where T : Component
    {
        t.gameObject.AddComponent<Component>();
    }

    public void SetMessage(PopupMessage popupMessage)
    {
        var components = new List<Component>();

        switch (popupMessage.PopupType)
        {
            case PopupType.CloseCompany:
                components.Add(new CloseCompanyController());
                components.Add(new ClosePopup());
                break;
        }

        SetItems(components.ToArray());
    }
}
