using UnityEngine;
using System.Collections;

public class SoundTriggerable : Triggerable
{
    AudioSource source;
    void Start()
    {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
            if (source == null)
            {
                source = gameObject.AddComponent<AudioSource>();
            }
        }
    }
}
