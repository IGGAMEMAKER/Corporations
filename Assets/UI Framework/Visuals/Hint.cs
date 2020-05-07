using Assets;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hint : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public string Text;
    [Tooltip("Will play sound only if this hint is attached to action button")]
    public bool isActionHint;

    bool isHovered;
    private int fontSize = 27;
    private int textSize = 23; // 18
    int renderingDepth = 50;


    Texture2D BackgroundTexture;
    private GUIStyle currentStyle = null;

    void Start () {
        SetHint(Text);

        BackgroundTexture = MakeTex(2, 2, new Color(0f, 1f, 0f, 1f));
    }

    void OnDisable()
    {
        isHovered = false;
    }

    //float GetContentWidth(string text)
    //{
    //    text.Split('\n',);
    //}

    void ShowHint()
    {
        InitStyles();

        //GUIStyle guiStyle = new GUIStyle();
        //Font font = new Font();
        //font
        //guiStyle.font = font;
        //guiStyle.fontSize = 20;

        float mouseX = Input.mousePosition.x;
        float mouseY = Screen.height - Input.mousePosition.y;

        float offsetX = 25f;
        float offsetY = 15f;


        float contentWidth = 300f; // 225
        float contentHeight = 400f; // 300

        float rightSideOfScreenOffset = 0;
        float verticalScreenOffset = 0;

        if (mouseX > Screen.width - contentWidth)
            rightSideOfScreenOffset = contentWidth;

        if (mouseY > Screen.height - contentHeight)
            verticalScreenOffset = contentHeight;

        Rect content = new Rect(mouseX + 2 * offsetX - rightSideOfScreenOffset, mouseY - verticalScreenOffset, contentWidth, contentHeight);
        Rect wrapper = new Rect(mouseX + offsetX - rightSideOfScreenOffset, mouseY - offsetY - verticalScreenOffset, contentWidth + offsetX, contentHeight + offsetY);

        GUI.color = Color.white;

        GUI.backgroundColor = Color.black;

        //GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
        GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = fontSize;

        GUI.depth = renderingDepth;

        //GUI.Label(content, Text);
        //GUI.Box(wrapper, "");


        GUI.skin.box.alignment = TextAnchor.UpperLeft;

        int horizontalOffset = 20;
        int verticalOffset = 15;
        GUI.skin.box.padding = new RectOffset(horizontalOffset, horizontalOffset, verticalOffset, verticalOffset);
        GUI.Box(wrapper, Text, currentStyle);
        //GUI.Label(content, Text);
    }

    bool HasText ()
    {
        return Text.Length > 1;
    }

    private void OnGUI()
    {
        if (isHovered)
        {
            if (HasText())
            {
                ShowHint();
                //Debug.Log("ONGUI HINT " + Text.Length);
            }
        }

        //Debug.Log("ONGui hint " + Text);
    }

    // this code took from 
    // https://forum.unity.com/threads/change-gui-box-color.174609/
    private void InitStyles()
    {
        if (currentStyle == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.wordWrap = true;
            currentStyle.fontSize = textSize;
            //currentStyle.font = Resources.Load()
            currentStyle.normal.background = BackgroundTexture;
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    public void SetHint(string text)
    {
        Text = text.Replace("\\n", "\n");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HasText() && isActionHint)
            SoundManager.PlayOnHintHoverSound();
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
