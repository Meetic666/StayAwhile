using UnityEngine;
using System.Collections;

public class FireAnimation : MonoBehaviour
{
    public CircleDetector2D m_FireCircle;

    public AudioClip m_BurningClip;
    public AudioClip m_DeadClip;

    Animator m_Animator;

    string m_Alive;

    bool m_PreviousAlive;

    AudioSource m_AudioSource;

	// Use this for initialization
	void Start ()
    {
        m_Animator = GetComponent<Animator>();

        m_AudioSource = GetComponent<AudioSource>();

        m_Alive = "Alive";
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool alive = m_FireCircle.CircleRadius > 3.0f;

        m_Animator.SetBool(m_Alive, alive);

        if(m_PreviousAlive != alive)
        {
            SwitchSoundState();
        }

        m_PreviousAlive = alive;
	}

    void SwitchSoundState()
    {
        m_AudioSource.clip = !m_PreviousAlive ? m_BurningClip : m_DeadClip;

        m_AudioSource.Play();
    }
}
