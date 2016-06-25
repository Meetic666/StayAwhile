using UnityEngine;
using System.Collections;

public class TestPooling : MonoBehaviour
{
    public GameObject m_Prefab;

	// Use this for initialization
	void Start ()
    {
        ObjectPool.Instance.RegisterPrefab(m_Prefab, 5);
	}

    [ContextMenu("Instantiate Object")]
	void InstantiateObject()
    {
        ObjectPool.Instance.Instantiate(m_Prefab, transform.position, transform.rotation);
    }
}
