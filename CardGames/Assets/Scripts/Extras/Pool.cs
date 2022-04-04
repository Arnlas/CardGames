using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int minPoolSize = 2;
    private Queue<GameObject> objects;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        objects = new Queue<GameObject>();
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent)
    {
        GameObject obj = null;
        if (objects.Count > minPoolSize)
        {
            
            obj = objects.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation; 
            obj.transform.SetParent(parent);
        }
        else
        {
            obj = Instantiate(prefab, position, rotation, parent);
        }
        obj.transform.localScale = scale;
        
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(this.transform);
        if (!objects.Contains(obj)) objects.Enqueue(obj);
    }

    public void Disable(GameObject obj)
    {
        obj.SetActive(false);
    }
}
