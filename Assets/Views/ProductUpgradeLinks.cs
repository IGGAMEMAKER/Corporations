using System;
using Michsky.UI.Frost;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductUpgradeLinks : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Toggle Toggle;
    public ToggleAnim ToggleAnim;
    public Hint Hint;
    public CanvasGroup CanvasGroup;

    public Image Background;
    public Image Icon;

    [Space(20)]
    public bool SetInfoAutomatically = false;

    public Sprite IconSprite;
    public string Title2;
    
    private void OnValidate()
    {
        if (SetInfoAutomatically)
        {
            Title.text = Title2;
            Icon.sprite = IconSprite;
        }
    }
}
