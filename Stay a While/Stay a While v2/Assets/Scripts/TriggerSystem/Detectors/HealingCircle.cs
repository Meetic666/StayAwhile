using UnityEngine;
using System.Collections;

public class HealingCircle : CircleDetector2D 
{
    protected override void TriggerEffect()
    {
        base.TriggerEffect();

        for(int i = 0; i < detectedObjects.Count; i++)
        {
            detectedObjects[i].obj.GetComponent<BasePlayer>().Damage(-50.0f * Time.deltaTime);

        }

    }

}
