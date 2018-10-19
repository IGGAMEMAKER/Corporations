using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAvailable : MonoBehaviour {
    Image background;

	// Use this for initialization
	void Start () {
        GameObject image = new GameObject("background");
        image.AddComponent<Image>();

        var parent = this.gameObject;
        image.transform.SetParent(parent.transform, false);
        background = image.GetComponent<Image>();
        background.sprite = Resources.Load<Sprite>("interrupt-danger");

        // move it to the background layer
        image.transform.Translate(new Vector3(0, 0, 1000));

        RectTransform parentTransform = parent.GetComponent<RectTransform>();
        float width = parentTransform.rect.width + 100f;
        float height = parentTransform.rect.height + 100f;

        RectTransform rect = image.GetComponent<RectTransform>();
        rect.rect.Set(0, 0, width, height);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
