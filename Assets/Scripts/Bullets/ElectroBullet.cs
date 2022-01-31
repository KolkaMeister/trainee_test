using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBullet : Bullet
{
    [SerializeField] private float damageDelay;
    [SerializeField] private int ticTimes;
    [SerializeField] private int damagePerTic;

    protected override void OnCollisionEnter(Collision collision)
    {
        var _interface = collision.gameObject.GetComponent<ICanTakeDamage>();
        if (_interface != null)
        {
            _interface.TakeDamage(damage);
            _interface.TakePeriodicDamage(damageDelay, ticTimes, damage);
        }
        Destroy(gameObject);
    }
}
