using UnityEngine;
using System.Collections;

public class FireLight : MonoBehaviour
{
    public CircleDetector2D m_FireCircle;

    Light m_Light;

	// Use this for initialization
	void Start ()
    {
        m_Light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_Light.range = m_FireCircle.CircleRadius * 3.0f;
	}
}
