using Assets.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
public abstract partial class ButtonController : BaseClass
{
    Button Button;
    [Tooltip("Sets the color of link")]
    public bool IsLink = false;

    public abstract void Execute();
    public virtual void ButtonStart() { }

    void ExecuteAndUpdateScreen()
    {
        Execute();

        UpdatePage();
    }

    // start
    public virtual void Initialize()
    {
        Button = GetComponentInChildren<Button>() ?? GetComponent<Button>();

        Button.onClick.AddListener(ExecuteAndUpdateScreen);

        ButtonStart();

        if (IsLink)
        {
            var text = GetComponentInChildren<TextMeshProUGUI>();

            var linkColor = Visuals.GetColorFromString(Colors.COLOR_LINK);

            if (text != null)
                text.color = linkColor;
            else
                GetComponentInChildren<Text>().color = linkColor;
        }
    }

    /// <summary>
    /// CANNOT OVVERRIDE IN INHERITED CLASSES
    /// </summary>
    void OnEnable()
    {
        Initialize();
    }

    // destroy
    void OnDisable()
    {
        RemoveListener();
    }

    void RemoveListener()
    {
        if (Button)
            Button.onClick.RemoveListener(ExecuteAndUpdateScreen);
        else
            Debug.LogWarning("This component is not assigned to Button. It is assigned to " + gameObject.name);
    }

    public void UpdatePage()
    {
        ScreenUtils.UpdateScreen(Q);
    }
}
