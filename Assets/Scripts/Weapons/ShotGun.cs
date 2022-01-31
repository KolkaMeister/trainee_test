using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    [SerializeField] private Transform[] bulletSpawnPoints;
    [SerializeField] private int fractionCount;
    public override void Shoot()
    {
        if (fireRateCooldown.IsReady && !IsEmpty)
        {
            fireRateCooldown.Reset();
            bulletsInMagazine--;
            SpawnFractions(CalculateVectors(this.transform.forward));
        }
    }

    public Vector3[] CalculateVectors(Vector3 _originalVector)
    {
        List<Vector3> directions = new List<Vector3>();
        var originalRadAngle = Mathf.Atan2(_originalVector.z, _originalVector.x);
        var spreadRad = Mathf.Deg2Rad * spreadValue;
        var startRadAngle = originalRadAngle - spreadRad;
        var angleSector = spreadRad * 2 / fractionCount;
        for (int i = 0; i < fractionCount; i++)
        {
            var z = Mathf.Sin(startRadAngle + angleSector * i);
            var x = Mathf.Cos(startRadAngle + angleSector * i);
            directions.Add(new Vector3(x, 0, z));
        }
        return directions.ToArray();
    }
    public void SpawnFractions(Vector3[] directions)
    {
        foreach (var direction in directions)
        {
            var spawnedBullet = Instantiate(bullet, bulletSpawnPoints[bulletsInMagazine].position, transform.rotation);
            spawnedBullet.rb.velocity = direction * force;
        }
    }
}



