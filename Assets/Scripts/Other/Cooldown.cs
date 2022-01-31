using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cooldown 
{
    [SerializeField] private float cooldownTime;

    private float upSetTime;

    public float CooldownTime => cooldownTime;
    public bool IsReady => Time.time >= upSetTime;

    public void Reset()
    {
        upSetTime = Time.time + cooldownTime;
    }

}
