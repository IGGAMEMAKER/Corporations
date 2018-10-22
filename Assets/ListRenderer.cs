using System.Collections.Generic;
using UnityEngine;

public abstract class ListRenderer : MonoBehaviour
{
    public GameObject PrefabInstance;
    List<GameObject> gameObjects;

    public abstract int itemsPerLine { get; set; }

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
        float spacing = 150f;

        Vector3 pos = new Vector3(x, y, 0) * spacing;
        GameObject g = Instantiate(PrefabInstance, pos, Quaternion.identity);

        g.transform.SetParent(this.gameObject.transform, false);

        gameObjects.Add(g);

        return g;
    }

    abstract public void UpdateObject<T>(GameObject gameObject, T item, int index);

    public void UpdateList<T> (List<T> objects)
    {
        RemoveCurrentObjects();

        for (var i = 0; i < objects.Count; i++)
        {
            int x = i % itemsPerLine;
            int y = i / itemsPerLine;

            GameObject g = InstantiateObject(x, y);
            UpdateObject(g, objects, i);
        }
    }
}
