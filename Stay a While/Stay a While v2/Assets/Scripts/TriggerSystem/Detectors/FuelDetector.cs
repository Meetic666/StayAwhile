using UnityEngine;
using System.Collections;

public class FuelDetector : CircleDetector2D 
{
    public float Fuel = 5.0f;

    protected override void TriggerEffect()
    {
        base.TriggerEffect();

        for (int i = 0; i < detectedObjects.Count; i++)
        {
            detectedObjects[i].obj.GetComponent<BasePlayer>().FuelAmount += Fuel;
            Destroy(gameObject);
        }
    }

}
