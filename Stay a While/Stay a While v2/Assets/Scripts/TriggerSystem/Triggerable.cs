using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public enum TriggerType
{
    WaitForAll,
    WaitForOne,
    TriggerUntilOne,
    TriggerUntilAll,
    LessThan,
    Equal,
    MoreThan
}
[DisallowMultipleComponent]
public class Triggerable : MonoBehaviour
{
    [SerializeField]
    public List<Triggerable> ToTriggerList = new List<Triggerable>();

    public TriggerType type;
    public int CompareNumber = 0;


    [HideInInspector]
    public List<Triggerable> BeTriggerList = new List<Triggerable>();

    [HideInInspector]
    public bool Triggered = false;
    public bool ToDespawn = false;
    protected virtual void Update()
    {
        if (ToDespawn == true)
        {
            Destroy(gameObject);
        }

        bool ready = false;
        switch (type)
        {
            case TriggerType.WaitForOne:
                ready = false;
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == true)
                        ready = true;
                }
                break;
            case TriggerType.WaitForAll:
                ready = true;
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == false)
                        ready = false;
                }
                break;
            case TriggerType.TriggerUntilOne:
                ready = true;
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == true)
                    {
                        ready = false;
                    }
                }


                break;
            case TriggerType.TriggerUntilAll:
                ready = true;
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == false)
                    {
                        ready = false;
                    }
                }


                break;
            case TriggerType.LessThan:
                ready = false;
                int countLess = 0;
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == true)
                    {
                        countLess++;
                    }
                }
                if (countLess < CompareNumber)
                {
                    ready = true;
                }

                break;
            case TriggerType.Equal:
                {
                    ready = false;
                    int countEqual = 0;
                    for (int i = 0; i < BeTriggerList.Count; i++)
                    {
                        if (BeTriggerList[i].Triggered == true)
                        {
                            countEqual++;
                        }
                    }
                    if (countEqual == CompareNumber)
                    {
                        ready = true;
                    }

                    break;
                }
            case TriggerType.MoreThan:
                ready = false;
                int countMore = 0;
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == true)
                    {
                        countMore++;
                    }
                }
                if (countMore > CompareNumber)
                {
                    ready = true;
                }

                break;

            default:
                return;
        }
        Triggered = ready;
    }

    protected virtual void LateUpdate()
    {
        if (Triggered == true)
        {
            TriggerEffect();
        }
    }

    public virtual void DeactivateTrigger(int recursiveLevelsUp)
    {
        if (recursiveLevelsUp > 0)
        {
            if (BeTriggerList.Count > 0)
            {
                for (int i = 0; i < BeTriggerList.Count; i++)
                {

                    BeTriggerList[i].DeactivateTrigger(recursiveLevelsUp - 1);
                    BeTriggerList[i].Triggered = false;
                }
            }
        }
        Triggered = false;
    }
    virtual protected void TriggerEffect() 
    {
        if(ObjectSingleton.Instance.ResetTriggers.Contains(this) == false)
            ObjectSingleton.Instance.ResetTriggers.Add(this);
    }

    virtual public void ResetTrigger()
    {
        Triggered = false;
    }


#if UNITY_EDITOR
    void OnValidate()
    {
        for (int i = 0; i < BeTriggerList.Count; i++)
        {
            if (BeTriggerList[i] != null)
            {
                if (!BeTriggerList[i].ToTriggerList.Contains(this))
                    BeTriggerList.Remove(BeTriggerList[i]);
            }
            else
            {
                Debug.LogWarning(gameObject.name + " Trigger Script Error: BeTriggerList Null Reference in Validation");
            }
        }


        for (int i = 0; i < ToTriggerList.Count; i++)
        {
            if (ToTriggerList[i] != null)
            {
                if (ToTriggerList[i].BeTriggerList.Count > 0)
                {

                    if (!ToTriggerList[i].BeTriggerList.Contains(this))
                        ToTriggerList[i].BeTriggerList.Add(this);
                }
                else
                    ToTriggerList[i].BeTriggerList.Add(this);
            }
            else
            {
                Debug.LogWarning(gameObject.name + " Trigger Script Error: ToTriggerList Null Reference in Validation");
            }
        }
        Debug.ClearDeveloperConsole();

    }

    virtual protected void OnDrawGizmos()
    {
        OnValidate();
        
        if (Selection.Contains(gameObject))
        {
            if (BeTriggerList.Count > 0)
            {
                for (int i = 0; i < BeTriggerList.Count; i++)
                {
                    if (BeTriggerList[i].Triggered == true)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawSphere(BeTriggerList[i].gameObject.transform.position, 0.2f);
                        Gizmos.DrawLine(gameObject.transform.position, BeTriggerList[i].transform.position);
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(BeTriggerList[i].gameObject.transform.position, 0.2f);
                        Gizmos.DrawLine(gameObject.transform.position, BeTriggerList[i].transform.position);
                    }
                }
            }
            if (ToTriggerList.Count > 0)
            {
                for (int i = 0; i < ToTriggerList.Count; i++)
                {
                    if (ToTriggerList[i] != null)
                    {
                        if (ToTriggerList[i].Triggered == true)
                        {
                            Gizmos.color = Color.blue;
                            Gizmos.DrawSphere(ToTriggerList[i].gameObject.transform.position, 0.2f);
                            Gizmos.DrawLine(gameObject.transform.position, ToTriggerList[i].transform.position);
                        }
                        else
                        {
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawSphere(ToTriggerList[i].gameObject.transform.position, 0.2f);
                            Gizmos.DrawLine(gameObject.transform.position, ToTriggerList[i].transform.position);
                        }
                    }
                    else
                    {
                        Debug.LogWarning(gameObject.name + " Trigger Script Error: ToTriggerList Null Reference during Gizmo Drawing");
                    }
                }
            }
        }
    }





#endif
    void OnDestroy()
    {
        for (int i = 0; i < BeTriggerList.Count; i++)
        {
            if (BeTriggerList[i].ToTriggerList.Contains(this))
                BeTriggerList[i].ToTriggerList.Remove(this);
        }
        for (int i = 0; i < ToTriggerList.Count; i++)
        {
            if (ToTriggerList[i].BeTriggerList.Contains(this))
                ToTriggerList[i].BeTriggerList.Remove(this);
        }
    }


}
