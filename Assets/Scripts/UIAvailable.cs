using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAvailable : MonoBehaviour {
	
    // Use this for initialization
	void Start () {
        GameObject image = new GameObject("background");
        Image background = image.AddComponent<Image>();
        Canvas c = image.AddComponent<Canvas>();

        c.overrideSorting = true;
        c.sortingOrder = -1;

        var parent = this.gameObject;
        image.transform.SetParent(parent.transform, false);

        RectTransform parentTransform = parent.GetComponent<RectTransform>();
        float width = parentTransform.rect.width + 100f;
        float height = parentTransform.rect.height + 100f;

        RectTransform rect = image.GetComponent<RectTransform>();
        rect.rect.Set(0, 0, width, height);

        background.sprite = Resources.Load<Sprite>("interrupt-danger");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
