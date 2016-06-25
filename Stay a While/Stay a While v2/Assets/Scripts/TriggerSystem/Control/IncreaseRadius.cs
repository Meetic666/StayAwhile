using UnityEngine;
using System.Collections;

public class IncreaseRadius : Triggerable {

    float Factor = 10.0f;
    public float MaxFactor = 10.0f;

    public CircleDetector2D[] detectors;
    float[] detectorDefaults;
    void Start()
    {
        detectorDefaults = new float[detectors.Length];

        for(int i = 0; i < detectors.Length; i++)
        {
            detectorDefaults[i] = detectors[i].CircleRadius;
        }

    }

    protected override void TriggerEffect()
    {
        base.TriggerEffect();

        for(int i = 0; i < detectors.Length; i++)
        {
            detectors[i].CircleRadius = (detectorDefaults[i] * (i + 1)) * Factor > MaxFactor ? MaxFactor : (detectorDefaults[i] * (i + 1)) * Factor;
            if(Factor > 0.0f)
                Factor -= 0.03f * Time.deltaTime;
        }
    }

    public void AddToFactor(float increase)
    {
        Factor += increase;
    }

}
