using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanTakeDamage 
{
    event Action onDieEvent;
    void TakeDamage(int damage);

    void TakePeriodicDamage(float delay,int count, int damage);
}
