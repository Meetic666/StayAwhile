using UnityEngine;
using System.Collections;

public class DeactivateTriggers : Triggerable {

    public int RecursiveLevelsUp = 0;

    protected override void TriggerEffect()
    {
        Triggered = false;
        foreach(Triggerable trig in ToTriggerList)
        {
            trig.DeactivateTrigger(RecursiveLevelsUp);

        }
    }
}
