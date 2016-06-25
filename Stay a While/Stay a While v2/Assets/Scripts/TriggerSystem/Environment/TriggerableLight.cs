using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Light))]
public class TriggerableLight : Triggerable
{
    public bool CanBeTriggered;
    public float SetResetTime;
    public bool FlickerWhenSwitching = true;
    public bool FlickerWhenIdle = false;
    public ulong lightPattern;
    public float lightFlickerInterval;
    public bool randomizePatternAfterUse;
    float ResetTimer;
    public ulong lightPatternReset;
    bool LightOn;
    bool RandomizeMe = false;
    Light attachedLight;
    bool inUse = false;
    Component halo;
    void Start()
    {
        attachedLight = GetComponent<Light>();
        LightOn = attachedLight.enabled;

        //attachedLight.enabled = false;
        if (lightPattern.ToString().Contains("2")
            || lightPattern.ToString().Contains("3")
            || lightPattern.ToString().Contains("4")
            || lightPattern.ToString().Contains("5")
            || lightPattern.ToString().Contains("6")
            || lightPattern.ToString().Contains("7")
            || lightPattern.ToString().Contains("8")
            || lightPattern.ToString().Contains("9"))
        {

            ulong temp = (ulong)Mathf.Min((int)lightPattern, 18);

            lightPattern = 1;
            for (ulong i = 0; i < temp; i++)
            {
                lightPattern = (lightPattern * 10) + (ulong)Random.Range(0, 2);

            }
            if (randomizePatternAfterUse)
                lightPatternReset = temp;
            else
            {
                lightPatternReset = lightPattern;
            }
        }

        if (GetComponent("Halo") != null)
        {
            halo = GetComponent("Halo");
        }
    }

    protected override void TriggerEffect()
    {
        base.TriggerEffect();
        ResetTimer = SetResetTime;
        if (CanBeTriggered == true)
        {
            if (Triggered == true)
            {
                if (FlickerWhenSwitching == true)
                {
                    if (LightOn == false)
                    {
                        StartCoroutine(FlickerLight(true));
                    }
                }
                else
                {
                    SwitchLight(true);
                    LightOn = true;
                }
            }
            else
            {
                if (ResetTimer > 0.0f)
                {
                    ResetTimer -= Time.deltaTime;
                }
                else
                {

                    if (LightOn == true)
                    {
                        if (FlickerWhenSwitching == true)
                        {

                            StartCoroutine(FlickerLight(true));

                        }
                        else
                        {
                            SwitchLight(false);

                            LightOn = false;
                        }
                    }
                }
            }

        }
        if (LightOn == true && FlickerWhenIdle == true)
        {
            StartCoroutine(FlickerLight(false));
        }
        Triggered = false;

    }
    void SwitchLight(bool desiredResult)
    {
        attachedLight.enabled = desiredResult;
        if (halo != null)
        {
            halo.GetType().GetProperty("enabled").SetValue(halo, desiredResult, null);
        }
    }


    IEnumerator FlickerLight(bool Switching)
    {
        if (inUse == true)
        {
            yield break;
        }
        inUse = true;
        string Pattern = lightPattern.ToString();
        for (int i = 0; i < Pattern.Length; i = 0)
        {
            if (Pattern.EndsWith("1"))
            {
                SwitchLight(true);
            }
            else
            {
                SwitchLight(false);
            }

            yield return new WaitForSeconds(lightFlickerInterval);

            if (Pattern.Length > 1)
            {
                Pattern = Pattern.Substring(0, Pattern.Length - 1);
                lightPattern = ulong.Parse(Pattern);
            }
            else
            {
                if (randomizePatternAfterUse == true)
                {
                    RandomizeMe = true;
                }
                lightPattern = 0;
            }

            if (lightPattern == 0)
            {
                if (RandomizeMe == true)
                {
                    for (ulong j = 0; j < lightPatternReset; j++)
                    {
                        lightPattern = (lightPattern * 10) + (ulong)Random.Range(0, 2);
                    }
                    RandomizeMe = false;
                    break;
                }
                else
                {
                    lightPattern = lightPatternReset;
                    break;
                }
            }

        }
        if (Switching == true)
        {

            if (LightOn == true) LightOn = false;
            else LightOn = true;
            SwitchLight(LightOn);
        }
        inUse = false;
        yield break;
    }
}