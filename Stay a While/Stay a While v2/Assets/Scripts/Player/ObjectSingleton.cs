using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectSingleton : MonoBehaviour
{
    public static ObjectSingleton Instance
    {
        get
        {
            if(s_ObjectSingleton == null)
            {
                GameObject go = new GameObject("ObjectSingleton");
                s_ObjectSingleton = go.AddComponent<ObjectSingleton>();
            }

            return s_ObjectSingleton;
        }
    }

    //public variables and references

    public List<GameObject> playerList = new List<GameObject>();
    public List<Triggerable> ResetTriggers = new List<Triggerable>();

    //private variables and references
    static ObjectSingleton s_ObjectSingleton;
    public void ResetAllTriggers()
    {
        foreach(Triggerable trig in ResetTriggers)
        {
            trig.ResetTrigger();
        }
    }
    
    void Awake()
    {
        s_ObjectSingleton = this;
    }
    
       
}
