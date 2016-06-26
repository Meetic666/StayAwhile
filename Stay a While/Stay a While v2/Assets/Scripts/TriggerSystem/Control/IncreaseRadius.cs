using UnityEngine;
using System.Collections;

public class IncreaseRadius : Triggerable {

    public float StartFactor = 50.0f;
    public float MaxFactor = 10.0f;

    public float m_FactorDecreaseRate = 0.03f;

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
            detectors[i].CircleRadius = (detectorDefaults[i] * (i + 1)) * StartFactor > MaxFactor ? MaxFactor : (detectorDefaults[i] * (i + 1)) * StartFactor;
            if(StartFactor > 0.0f)
                StartFactor -= m_FactorDecreaseRate * Time.deltaTime;
        }
    }

    public void AddToFactor(float increase)
    {
        StartFactor += increase;
    }

}
