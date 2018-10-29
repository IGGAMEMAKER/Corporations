using System.Collections.Generic;
using UnityEngine;

public abstract class ListRenderer : MonoBehaviour
{
    public GameObject PrefabInstance;
    List<GameObject> gameObjects;

    public abstract int itemsPerLine { get; set; }
    public abstract Vector2 spacing { get; set; }

    private void Awake()
    {
        gameObjects = new List<GameObject>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void RemoveCurrentObjects()
    {
        for (int i = 0; i < gameObjects.Count; i++)
            Destroy(gameObjects[i]);

        gameObjects.Clear();
    }

    GameObject InstantiateObject(int x, int y)
    {
        Vector3 pos = new Vector3(x * spacing.x, -y * spacing.y, 0);
        GameObject g = Instantiate(PrefabInstance, pos, Quaternion.identity);

        g.transform.SetParent(this.gameObject.transform, false);

        gameObjects.Add(g);

        return g;
    }

    abstract public void RenderObject(GameObject obj, object item, int index, Dictionary<string, object> parameters);

    public void UpdateList<T> (List<T> objects, Dictionary<string, object> parameters = null)
    {
        RemoveCurrentObjects();

        for (var i = 0; i < objects.Count; i++)
        {
            int x = i % itemsPerLine;
            int y = i / itemsPerLine;

            GameObject g = InstantiateObject(x, y);
            RenderObject(g, objects[i], i, parameters);
        }
    }
}
