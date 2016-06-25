using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance
    {
        get
        {
            return s_Instance;
        }
    }

    static ObjectPool s_Instance;

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
            for (int i = 0; i < numberOfInstancesToRegister - initialCount; i++)
            {
                GameObject newObject = (GameObject)Object.Instantiate(prefab, Vector3.left * 10000.0f, Quaternion.identity);

                m_ObjectPool[prefab].Add(newObject);

                newObject.transform.parent = m_Transform;

                newObject.SetActive(false);
            }
        }
    }
	
	public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!m_ObjectPool.ContainsKey(prefab))
        {
            m_ObjectPool.Add(prefab, new List<GameObject>(4));

            for (int i = 0; i < 4; i++)
            {
                GameObject newObject = (GameObject)GameObject.Instantiate(prefab, Vector3.left * 10000000.0f, Quaternion.identity);

                m_ObjectPool[prefab].Add(newObject);

                newObject.SetActive(false);
            }
        }

        GameObject instance = null;
        GameObject currentObject = null;

        for(int i = 0; i < m_ObjectPool[prefab].Count; i++)
        {
            currentObject = m_ObjectPool[prefab][i];

            if (!currentObject.activeSelf)
            {
                currentObject.SetActive(true);

                currentObject.transform.position = position;
                currentObject.transform.rotation = rotation;

                instance = currentObject;

                break;
            }
        }

        return instance;
    }
}
