using UnityEngine;
using System.Collections;

public class WaitForObjectActivation : Triggerable 
{

    public GameObject[] WaitForObjectsTo;
    public bool ActivateOrDeactivate = false;

    protected override void Update()
    {
        base.Update();
        if(Triggered == true)
        {
            if(ActivateOrDeactivate == true)
            {
                bool ready2 = true;
                foreach(GameObject go in WaitForObjectsTo)
                {
                    if(go.active == false)
                    {
                        ready2 = false;
                    }
                }
                Triggered = ready2;

            }
            else
            {
                bool ready2 = true;
                foreach (GameObject go in WaitForObjectsTo)
                {
                    if (go != null)
                    {
                        if (go.active == true)
                        {
                            ready2 = false;
                        }
                    }
                }
                Triggered = ready2;
            }
        }
    }

}
