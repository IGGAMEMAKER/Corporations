using Assets.Core;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public partial class PopupView : View
{
    public Text Title;
    public Text Description;
    public PopupButtonsContainer popupButtonsContainer;
    public Text AmountOfMessages;

    List<Type> ButtonComponents = new List<Type>();

    public override void ViewRender()
    {
        var messagesCount = NotificationUtils.GetPopups(Q).Count;

        if (messagesCount == 0)
            return;

        AmountOfMessages.text = "Messages: " + messagesCount;


        ButtonComponents.Clear();

        var popup = NotificationUtils.GetPopupMessage(Q);
        
        // calls RenderUniversalPopup
        // which fills ButtonComponents list
        RenderPopup(popup);

        // attaches button components to buttonContainer
        popupButtonsContainer.SetComponents(ButtonComponents);

        // close modals if has any
    }

    void RenderUniversalPopup(string title, string description, params Type[] buttons)
    {
        Title.text = title;
        Description.text = description;

        foreach (var b in buttons)
            ButtonComponents.Add(b);

        // default button to close popup
        if (buttons.Length == 0)
            ButtonComponents.Add(typeof(ClosePopupCancel));
    }
}
