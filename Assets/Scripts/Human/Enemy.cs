using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Human
{
    [SerializeField] private Transform gunSpawnPoint;
    [SerializeField] private InRangeChacker checker;
    [SerializeField] private Weapon[] availableWeapons;
    [SerializeField] private LayerMask rayCollideLayers;

    private bool isDead;
    private float nextShotTime;
    private void Start()
    {
        SpawnGun(currentWeapon = availableWeapons[Random.Range(0, availableWeapons.Length)]);
        onDieEvent += () =>
         {
             GetComponent<MeshRenderer>().material.color = Color.red;
             currentWeapon.gameObject.SetActive(false);
             isDead = true;
         };
    }


    private void Update()
    {
        if (isDead) return;
        Behaviour();
    }
    private void SpawnGun(Weapon _weapon)
    {
        var weapon = Instantiate(_weapon, transform);
        SwitchWeapon(weapon);
    }
    ////////////////////////////////////////////////////// Behaviour //////////////////////////////////////////////////////

    //////// Проверка на стены, видим ли мы игрока
    private bool IsTargetInVision()
    {
        var distanse = (checker.Target.transform.position - transform.position).magnitude;
        var direction = (checker.Target.transform.position - transform.position).normalized;
        var Hits=Physics.RaycastAll(transform.position, direction, distanse, rayCollideLayers, QueryTriggerInteraction.Ignore);
        foreach (var hit in Hits)
        {
            if (hit.collider.gameObject.IsInLayer(checker.CheckLayer))
            {
                return true;
            }
            else if (hit.collider.gameObject.IsInLayer(rayCollideLayers))
            {
                return false;
            }
        }
        return false;
    }
    ////// Общее поведение врага
    private void Behaviour()
    {
        if (checker.Target != null)
        {
            if (IsTargetInVision())
            {
                RotateToTarget(checker.Target.transform);
                ShootBehaviour();
            }
        }
    }
    //////////////////////// Проверка условий для стрельбы
    private void ShootBehaviour()
    {
        var isOnLine = Physics.Raycast(transform.position, transform.forward, (checker.Target.transform.position-transform.position).magnitude, checker.CheckLayer, QueryTriggerInteraction.Ignore);
        if (isOnLine&& nextShotTime <= Time.time)
        {
            Shoot();
            nextShotTime = Time.time + currentWeapon.FireRateCooldown.CooldownTime + Random.Range(0.1f, 0.4f);
        }
    }
    //////////////////////// Поворот к цели
    private void RotateToTarget(Transform target)
    {
        var direction = (target.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        Rotate(direction);
    }
}
