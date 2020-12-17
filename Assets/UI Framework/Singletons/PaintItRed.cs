using UnityEngine;

public class PaintItRed : MonoBehaviour {
    Color BaseColor;
    Renderer Renderer;

	// Use this for initialization
	void Start () {
        Renderer = GetComponent<Renderer>();
        //GetComponent<Renderer>().material.color = Color.green;

        BaseColor = Renderer.material.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Renderer.material.color = Color.red;
            print("ROSES ARE RED!");
        }

        if (Input.GetKeyDown(KeyCode.G))
            Renderer.material.color = Color.green;

        if (Input.GetKeyDown(KeyCode.B))
            Renderer.material.color = BaseColor;

    }
}
