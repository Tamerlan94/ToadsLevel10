using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand = true;

}

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> itemsToPool;



    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amounToPool;
    public bool shouldExpand = true;
    public Transform parent;


    // Use this for initialization
    void Start()
    {

        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                obj.transform.SetParent(parent);
                pooledObjects.Add(obj);
            }
        }


        //для отдельных
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amounToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            obj.transform.SetParent(parent);
            pooledObjects.Add(obj);
        }

    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(tag))
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.CompareTag(tag))
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    obj.transform.SetParent(parent);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }



    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (shouldExpand)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            obj.transform.SetParent(parent);
            pooledObjects.Add(obj);
            return obj;
        }
        else
        {
            return null;
        }

        //GameObject obj = (GameObject)Instantiate(objectToPool);
        //obj.SetActive(false);
        //pooledObjects.Add(obj);
        //return obj;

    }
}
