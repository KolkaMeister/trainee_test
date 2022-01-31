using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private Transform bulletSpawnPoint;

    public override void Shoot()
    {
        if (fireRateCooldown.IsReady&&!IsEmpty)
        {
            fireRateCooldown.Reset();
            bulletsInMagazine--;
            var spawnedBullet = Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
            var direction = CalculateVector(spawnedBullet.transform.forward);
            spawnedBullet.rb.velocity= direction * force;
        }
    }
    private Vector3 CalculateVector(Vector3 _originalVector)
    {
        var rad = Mathf.Atan2(_originalVector.z, _originalVector.x);
        rad+= Mathf.Deg2Rad *Random.Range(-spreadValue, spreadValue);
        var z = Mathf.Sin(rad);
        var x = Mathf.Cos(rad);
        return new Vector3(x, 0, z);
    }
}
