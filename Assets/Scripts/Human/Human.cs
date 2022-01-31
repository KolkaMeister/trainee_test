using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour,ICanTakeDamage
{
    [SerializeField] protected Weapon currentWeapon;
    [SerializeField] protected int hp;

    protected Coroutine reloadRoutine;

    public event Action onDieEvent;

    private Coroutine periodicDamageCoroutine;

    ///////////////// Health ////////////////////
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp = Mathf.Max(0, hp - damage);
        if (hp <= 0) onDieEvent?.Invoke();
    }

    public void TakePeriodicDamage(float delay, int count, int damage)
    {
        if (hp <= 0) return;
        if (periodicDamageCoroutine!=null)
        {
            StopCoroutine(periodicDamageCoroutine);
            periodicDamageCoroutine = null;
        }
        periodicDamageCoroutine = StartCoroutine(PeriodicDamageCoroutine(delay,count,damage));
    }

    public IEnumerator PeriodicDamageCoroutine(float delay, int count, int damage)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay);
            TakeDamage(damage);
        }
        periodicDamageCoroutine = null;
    }
    ///////////////// Rotate ////////////////////

    protected void Rotate(Vector3 rotateDirection)
    {
        transform.rotation = Quaternion.LookRotation(rotateDirection);
    }

    ///////////////// Weapons ////////////////////


    protected void Shoot()
    {
        if (reloadRoutine != null) return;
        if (currentWeapon.IsEmpty)
        {
            Reload();
            return;
        }
        currentWeapon.Shoot();
    }
    protected void SwitchWeapon(Weapon _weapon)
    {
        if (currentWeapon == _weapon) return;

        if (reloadRoutine != null)
        {
            StopCoroutine(reloadRoutine);
            reloadRoutine = null;
        }
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = _weapon;
        currentWeapon.gameObject.SetActive(true);
        if (currentWeapon.IsEmpty) Reload();

    }

    protected void Reload()
    {
        if (reloadRoutine != null) return;
        if (currentWeapon.IsMagazineFull) return;
        reloadRoutine = StartCoroutine(ReloadCoroutine(currentWeapon));
    }

    private IEnumerator ReloadCoroutine(Weapon weapon)
    {
        var delay = Time.time + weapon.ReloadTime;

        while (Time.time < delay)
        {
            yield return null;
        }
        currentWeapon.LoadMagazine();
        yield return null;
        reloadRoutine = null;
    }
}
