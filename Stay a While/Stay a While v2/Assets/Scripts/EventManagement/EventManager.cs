using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void GameEventDelegate(EventData eventData);

public class EventManager
{
    static EventManager s_Instance;

    public static EventManager Instance
    {
        get
        {
            if(s_Instance == null)
            {
                s_Instance = new EventManager();
            }

            return s_Instance;
        }
    }

    Dictionary<System.Type, GameEventDelegate> m_EventDelegates;

	// Use this for initialization
	EventManager ()
    {
        m_EventDelegates = new Dictionary<System.Type, GameEventDelegate>();
	}

    public void RegisterListener(System.Type type, GameEventDelegate listener)
    {
        if (!m_EventDelegates.ContainsKey(type))
        {
            m_EventDelegates.Add(type, listener);
        }
        else
        {
            m_EventDelegates[type] += listener;
        }
    }

    public void DeregisterListener(System.Type type, GameEventDelegate listener)
    {
        if(m_EventDelegates.ContainsKey(type))
        {
            m_EventDelegates[type] -= listener;
        }
    }

    public void ClearListeners()
    {
        m_EventDelegates.Clear();
    }

    public void SendEvent(EventData eventData)
    {
        System.Type type = eventData.GetType();

        if (m_EventDelegates.ContainsKey(type))
        {
            m_EventDelegates[type](eventData);
        }
    }
}
