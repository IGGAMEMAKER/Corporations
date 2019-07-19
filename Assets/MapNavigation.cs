using Assets.Utils;
using UnityEngine;

public class MapNavigation : View
{
    public float Limit = 550f;
    public float Sensitivity = 750f;

    public float X;
    public float Y;

    public float Zoom = 1f;

    void Update()
    {
        CheckMouseMovement();

        CheckZoom();

        UpdateCanvasPosition();

        RedrawMap();
    }

    float X0 => -Screen.width / 2;
    float Y0 => Screen.height / 2;


    float amountOfNichesSqrt;
    private void OnEnable()
    {
        amountOfNichesSqrt = Mathf.Sqrt(NicheUtils.GetNiches(GameContext).Length);

        X = X0;
        Y = Y0;
    }

    void UpdateCanvasPosition()
    {
        //var mapSize = (Map.IndustrialRadius * 2 + Map.NicheRadius * 2) * Zoom;
        var mapSizeX = Screen.width;
        var mapSizeY = Screen.height;

        var ZoomOffsetX = mapSizeX * (Zoom - 1);
        var ZoomOffsetY = mapSizeY * (Zoom - 1);

        X = Mathf.Clamp(X, X0 - ZoomOffsetX, X0);
        Y = Mathf.Clamp(Y, Y0 - ZoomOffsetY, Y0 + ZoomOffsetY);

        transform.localPosition = new Vector3(X, Y);
        transform.localScale = new Vector3(Zoom, Zoom, 1);
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 700, 100), $"X = {X},  Y = {Y}");
    }

    void CheckZoom()
    {
        var scroll = Input.mouseScrollDelta.y;

        if (scroll == 0)
            return;

        Zoom = Mathf.Clamp(Zoom + scroll / 10, 1f, 5f);

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float modifier = 0.25f;

        // if zoom out, don't move map
        if (scroll < 0)
            return;

        //X += -(mouseX - Screen.width / 2) * modifier;
        //Y += (mouseY - Screen.height / 2) * modifier;
    }

    MarketMapRenderer MarketMapRenderer;
    MarketMapRenderer Map
    {
        get
        {
            if (MarketMapRenderer == null)
                MarketMapRenderer = GetComponent<MarketMapRenderer>();

            return MarketMapRenderer;
        }
    }

    void RedrawMap()
    {
        Map.ViewRender();
    }

    void CheckMouseMovement()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float mul = Time.deltaTime * Sensitivity;

        float minOffset = 15f;

        // left 
        if (mouseX < minOffset)
            X += mul;

        // right
        if (mouseX > Screen.width - minOffset)
            X -= mul;

        // top
        if (mouseY < minOffset)
            Y += mul;

        // bottom
        if (mouseY > Screen.height - minOffset)
            Y -= mul;
    }
}
