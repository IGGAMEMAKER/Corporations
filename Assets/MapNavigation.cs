using Assets.Utils;
using UnityEngine;

public class MapNavigation : View
{
    public float Limit = 550f;
    public float Sensitivity = 750f;

    public float X;
    public float Y;

    public float Zoom = 1f;

    float X0 => -Screen.width / 2;
    float Y0 => Screen.height / 2;

    void Update()
    {
        CheckMouseMovement();

        CheckZoom();
    }

    void Start()
    {
        Zoom = minZoom;

        RedrawMap();
    }

    float amountOfNichesSqrt;
    private void OnEnable()
    {
        amountOfNichesSqrt = Mathf.Sqrt(Markets.GetNiches(GameContext).Length);

        X = X0;
        Y = Y0;

        RedrawMap();
    }



    float mapSize => (Map.IndustrialRadius * 2 + Map.NicheRadius * 2 + Map.CompanyRadius * 2);
    float mapSizeX => Mathf.Max(Screen.width, mapSize);
    float mapSizeY => Mathf.Max(Screen.height, mapSize);

    void UpdateCanvasPosition()
    {
        //var mapSizeX = Screen.width; // max from Calculated size and Screen size. Map cannot be smaller than Screen size!
        //var mapSizeY = Screen.height;

        var ZoomOffsetX = mapSizeX * Zoom - Screen.width;
        var ZoomOffsetY = mapSizeY * Zoom - Screen.height;

        X = Mathf.Clamp(X, X0 - ZoomOffsetX, X0);
        Y = Mathf.Clamp(Y, Y0, Y0 + ZoomOffsetY);

        transform.localPosition = new Vector3(X, Y);
        transform.localScale = new Vector3(Zoom, Zoom, 1);
    }

    //private void OnGUI()
    //{
    //    float mouseX = Input.mousePosition.x;
    //    float mouseY = Input.mousePosition.y; // - Constants.GAMEPLAY_OFFSET_Y;

    //    var deltaX = (mouseX - Screen.width / 2);
    //    var deltaY = (mouseY - Screen.height / 2);

    //    //GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 700, 100), $"X = {X},  Y = {Y}");
    //    GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 700, 100), $"dX = {deltaX}, dY = {deltaY}");
    //}


    float minZoom => Mathf.Max(Screen.width / mapSizeX, Screen.height / mapSizeY); //  0.33f;
    float maxZoom = 5f;


    void CheckZoom()
    {
        var scroll = Input.mouseScrollDelta.y;

        if (scroll == 0)
            return;

        var deltaScroll = scroll / 10;

        bool zoomIn = deltaScroll > 0;

        var prevZoom = Zoom;
        if (prevZoom == maxZoom && zoomIn || prevZoom == minZoom && !zoomIn)
            return;

        Zoom = Mathf.Clamp(Zoom + deltaScroll, minZoom, maxZoom);

        // if zoom out, don't move map
        if (scroll > 0)
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y + Constants.GAMEPLAY_OFFSET_Y;

            var deltaX = (mouseX - Screen.width / 2);
            var deltaY = (mouseY - Screen.height / 2);

            float modifier = 0.085f * Zoom;

            X += -deltaX * modifier; // * -Mathf.Sign(scroll);
            Y += -deltaY * modifier; // * -Mathf.Sign(scroll);
        }

        RedrawMap();
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
        //Debug.Log("RedrawMap");

        UpdateCanvasPosition();

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
        {
            X += mul;
            RedrawMap();
        }

        // right
        if (mouseX > Screen.width - minOffset)
        {
            X -= mul;
            RedrawMap();
        }

        // top
        if (mouseY < minOffset)
        {
            Y += mul;
            RedrawMap();
        }

        // bottom
        if (mouseY > Screen.height - minOffset)
        {
            Y -= mul;
            RedrawMap();
        }
    }
}
