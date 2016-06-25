using UnityEngine;
using System.Collections;

public class AddToFactor : CircleDetector2D 
{
    public IncreaseRadius rad;

    protected override void TriggerEffect()
    {
        base.TriggerEffect();
        for (int i = 0; i < detectedObjects.Count; i++)
        {
            rad.AddToFactor(detectedObjects[i].obj.GetComponent<BasePlayer>().FuelAmount);
            detectedObjects[i].obj.GetComponent<BasePlayer>().FuelAmount = 0;
        }
    }
}
