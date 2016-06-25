using UnityEngine;
using System.Collections;

public class TestEventData : EventData
{
    public int m_Whatever;
}

public class TestEvent : MonoBehaviour
{
    public int m_Whatever;

    GameEventDelegate m_TestDelegate;

	// Use this for initialization
	void Start ()
    {
        m_TestDelegate = new GameEventDelegate(ReceiveTestEvent);

        EventManager.Instance.RegisterListener(typeof(TestEventData), m_TestDelegate);
	}

    // For clean up
    void OnDestroy()
    {
        EventManager.Instance.DeregisterListener(typeof(TestEventData), m_TestDelegate);
    }

    void ReceiveTestEvent(EventData data)
    {
        Debug.Log(((TestEventData)data).m_Whatever);
    }

    [ContextMenu("SendEvent")]
    void SendEvent()
    {
        TestEventData data = new TestEventData();
        data.m_Whatever = m_Whatever;

        EventManager.Instance.SendEvent(data);
    }
}
