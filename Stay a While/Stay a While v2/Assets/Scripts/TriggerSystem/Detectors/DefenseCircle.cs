using UnityEngine;
using System.Collections;

public class DefenseCircle : CircleDetector2D 
{

    public float DefenseAmount = 0.0001f;
    protected override void TriggerEffect()
    {
        base.TriggerEffect();

        for(int i = 0; i < detectedObjects.Count; i++)
        {
           detectedObjects[i].obj.GetComponent<BasePlayer>().AddDefense(CircleRadius * Time.deltaTime * DefenseAmount);
        }

    }

}
