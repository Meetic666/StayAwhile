using UnityEngine;
using System.Collections;

public class TriggerOnce : Triggerable 
{
    public bool Resets = false;
    public float SetResetTimer = 0.0f;
    float ResetTimer;
    bool HasBeenHit;

    protected override void Update()
    {
        if (HasBeenHit == true)
        {
            Triggered = false;
            if (Resets == true)
            {
                if(ResetTimer > 0.0f)
                {
                    ResetTimer -= Time.deltaTime;
                }
                else
                {
                    HasBeenHit = false;
                }
            }
        }
        else
        {
            base.Update();
        }

        if(Triggered == true)
        {
            HasBeenHit = true;
        }
    }

    public override void ResetTrigger()
    {
        HasBeenHit = false;
        base.ResetTrigger();
    }

}
