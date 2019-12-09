using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public partial class PopupView : View
{
    public Text Title;
    public Text Description;
    public PopupButtonsContainer popupButtonsContainer;
    public Text AmountOfMessages;

    PopupMessage PopupMessage;

    List<Type> ButtonComponents = new List<Type>();

    private void OnEnable()
    {
        Render();
    }


    public override void ViewRender()
    {
        Render();
    }

    void Render()
    {
        var messagesCount = NotificationUtils.GetPopups(GameContext).Count;

        if (messagesCount == 0)
            return;

        var popup = NotificationUtils.GetPopupMessage(GameContext) ?? null;

        PopupMessage = popup;

        ButtonComponents.Clear();

        AmountOfMessages.text = "Messages: " + messagesCount;

        RenderPopup(popup);

        popupButtonsContainer.SetComponents(ButtonComponents);
    }

    void SetTitle(string text)
    {
        Title.text = text;
    }

    void SetDescription(string text)
    {
        Description.text = text;
    }

    void AddComponent(Type type)
    {
        ButtonComponents.Add(type);
    }


    void RenderUniversalPopup(string title, string description, params Type[] buttons)
    {
        SetTitle(title);
        SetDescription(description);

        foreach (var b in buttons)
            AddComponent(b);

        if (buttons.Length == 0)
            AddComponent(typeof(ClosePopup));
    }
}
