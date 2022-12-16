using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerControl : MonoBehaviour
{
    public new Light light;

    float timeOn;
    [Range(0.1f, 5)]
    public float minTimeOn;
    [Range(0.1f, 10)]
    public float maxTimeOn;

    float timeOff;
    [Range(0.1f, 5)]
    public float minTimeOff;
    [Range(0.1f, 10)]
    public float maxTimeOff;

    private float changeTime = 0;

    void Start()
    {
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }

    void Update()
    {
        timeOff = UnityEngine.Random.Range(minTimeOff, maxTimeOff);
        timeOn = UnityEngine.Random.Range(minTimeOn, maxTimeOn);

        if (Time.time > changeTime)
        {
            light.enabled = !light.enabled;
            if (light.enabled)
            {
                changeTime = Time.time + timeOn;
            }
            else
            {
                changeTime = Time.time + timeOff;
            }
        }
    }
}