using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Transform m_ObjectToFollow;

    BasePlayer m_Player;

    float m_MaxHorizontalScale;

    Vector3 m_Offset;
    Transform m_Transform;

	// Use this for initialization
	void Start ()
    {
        m_Transform = transform;

        m_Offset = m_Transform.position - m_ObjectToFollow.position;

        m_MaxHorizontalScale = m_Transform.localScale.x;

        m_Player = m_ObjectToFollow.GetComponent<BasePlayer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_Transform.position = m_ObjectToFollow.position + m_Offset;

        Vector3 scale = m_Transform.localScale;
        scale.x = m_MaxHorizontalScale * m_Player.Health / m_Player.MaxHealth;
        m_Transform.localScale = scale;
	}
}
