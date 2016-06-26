using UnityEngine;
using System.Collections;

public class ResetEvents : MonoBehaviour
{
    void OnDestroy()
    {
        EventManager.Instance.ClearListeners();
    }
}
