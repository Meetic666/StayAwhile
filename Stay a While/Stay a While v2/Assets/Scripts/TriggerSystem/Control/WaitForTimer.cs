using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class WaitForTimer : Triggerable
{
    public float SetTimer;
    public bool ResetOnFinish = true;
    float timer;
    void Start()
    {
        timer = SetTimer;
    }
    protected override void Update()
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

        if(ready == true)
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                Triggered = ready;
                if (ResetOnFinish)
                    timer = SetTimer;
            }
        }

    }

    public override void ResetTrigger()
    {
        timer = SetTimer;
        base.ResetTrigger();
        
    }
}
