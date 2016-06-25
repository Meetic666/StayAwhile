using UnityEngine;
using System.Collections;

public class WaitForCounter : Triggerable
{
    int counter;

    protected override void Update() { }

    public void AddToCounter(int number)
    {
        counter += number;
        switch (type)
        {
            case TriggerType.LessThan:
                {
                    if (counter < CompareNumber)
                    {
                        base.Update();
                    }
                    break;
                }
            case TriggerType.Equal:
                {
                    if(counter == CompareNumber)
                    {
                        base.Update();
                    }
                    break;
                }
            case TriggerType.MoreThan:
                {
                    if(counter > CompareNumber)
                    {
                        base.Update();
                    }
                    break;
                }
            default:
                {
                    Debug.LogWarning(gameObject.name + " WaitForCounter TriggerableScript Error: Script does not use this Trigger Type");
                    return;
                }


        }

    }
}
