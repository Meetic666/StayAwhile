using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance
    {
        get
        {
            if(s_Instance == null)
            {
                GameObject go = new GameObject("ObjectPool");
                s_Instance = go.AddComponent<ObjectPool>();
            }


            return s_Instance;
        }
    }

    static ObjectPool s_Instance;

    public int m_NumberOfPrefabsToInstantiateWhenRunningShort = 4;

    Dictionary<GameObject, List<GameObject>> m_ObjectPool;

    Transform m_Transform;

	// Use this for initialization
	void Awake ()
    {
        s_Instance = this;

        m_ObjectPool = new Dictionary<GameObject, List<GameObject>>();

        m_Transform = transform;
	}

    public void RegisterPrefab(GameObject prefab, int numberOfInstancesToRegister)
    {
        if(!m_ObjectPool.ContainsKey(prefab))
        {
            m_ObjectPool.Add(prefab, new List<GameObject>(numberOfInstancesToRegister));
        }

        int initialCount = m_ObjectPool[prefab].Count;

        if (initialCount < numberOfInstancesToRegister)
        {
            AddNewInstances(prefab, numberOfInstancesToRegister - initialCount);
        }
    }
	
	public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!m_ObjectPool.ContainsKey(prefab))
        {
            m_ObjectPool.Add(prefab, new List<GameObject>(m_NumberOfPrefabsToInstantiateWhenRunningShort));

            AddNewInstances(prefab, m_NumberOfPrefabsToInstantiateWhenRunningShort);
        }

        GameObject instance = null;
        GameObject currentObject = null;

        int listCount = m_ObjectPool[prefab].Count;

        for (int i = 0; i < listCount; i++)
        {
            currentObject = m_ObjectPool[prefab][i];

            if (!currentObject.activeSelf)
            {
                instance = currentObject;

                break;
            }
        }

        if(instance == null)
        {
            AddNewInstances(prefab, m_NumberOfPrefabsToInstantiateWhenRunningShort);

            instance = m_ObjectPool[prefab][listCount];
        }

        instance.SetActive(true);

        instance.transform.position = position;
        instance.transform.rotation = rotation;

        return instance;
    }

    void AddNewInstances(GameObject prefab, int numberToCreate)
    {
        for (int i = 0; i < numberToCreate; i++)
        {
            GameObject newObject = (GameObject)GameObject.Instantiate(prefab, Vector3.left * 10000000.0f, Quaternion.identity);

            m_ObjectPool[prefab].Add(newObject);

            newObject.transform.parent = m_Transform;

            newObject.SetActive(false);
        }
    }
}
