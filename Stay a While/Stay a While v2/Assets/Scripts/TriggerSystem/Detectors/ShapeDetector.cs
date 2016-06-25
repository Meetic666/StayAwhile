using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShapeDetector : Triggerable
{
    [System.Serializable]
    public struct objAdder
    {
        public GameObject obj;
        public float timer;
    }
    public bool TriggerOnEnter;
    public bool TriggerOnStay;
    public bool TriggerOnLeave;
    public List<objAdder> detectedObjects = new List<objAdder>();
    
    protected override void Update()
    {
        Triggered = false;
        CheckDetector();
        for (int i = 0; i < detectedObjects.Count; i++)
        {
            if (detectedObjects[i].timer > 0.0f)
            {
                objAdder temp = new objAdder();
                temp.obj = detectedObjects[i].obj;
                temp.timer = detectedObjects[i].timer - Time.deltaTime;
                detectedObjects[i] = temp;
            }
            else
            {
                OnDetectionEnd(detectedObjects[i].obj);
                detectedObjects.Remove(detectedObjects[i]);
            }
        }
    }

    protected virtual void OnDetectionStart(GameObject detectedObj)
    {
        Debug.Log("Detection Start");
        objAdder temp = new objAdder();
        temp.obj = detectedObj;
        temp.timer = 0.1f;
        detectedObjects.Add(temp);

        if (TriggerOnEnter == true)
        {
            base.Update();
        }
    }
    protected virtual void OnDetectionStay(GameObject detectedObj)
    {
        Debug.Log("Detection Stay");
        for(int i = 0; i < detectedObjects.Count; i++)
        {
            if(detectedObjects[i].obj == detectedObj)
            {
                objAdder temp = new objAdder();
                temp.obj = detectedObj;
                temp.timer = 0.1f;
                detectedObjects[i] = temp;

            }
        }

        if (TriggerOnStay == true)
        {
            base.Update();
        }
    }
    protected virtual void OnDetectionEnd(GameObject detectedObj)
    {
        Debug.Log("Detection End");
        if (TriggerOnLeave == true)
        {
            base.Update();
        }
    }

    protected virtual void CheckDetector() { }
}
