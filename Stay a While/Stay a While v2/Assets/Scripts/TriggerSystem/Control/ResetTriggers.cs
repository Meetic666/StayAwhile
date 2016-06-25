using UnityEngine;
using System.Collections;

public class ResetTriggers : Triggerable
{
    public int RecursiveLevelsUp;
    protected override void TriggerEffect()
    {
        DeactivateTrigger(RecursiveLevelsUp);
    }

}
