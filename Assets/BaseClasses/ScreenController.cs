using UnityEngine;

public class ScreenController : MonoBehaviour {
    float height;
    float width;

    float positionY;

	// Use this for initialization
	void Start () {
        height = 768f;

        width = 0; // 1024f;
        positionY = Constants.GAMEPLAY_OFFSET_Y;

        Render();
    }

    void Render()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);

        transform.localPosition = new Vector3(0, positionY, 0);

        transform.localScale = Vector3.one;
    }

    public void OnEnable()
    {
        Render();
    }
}
