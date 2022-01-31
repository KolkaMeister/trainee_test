using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour,ICanTakeDamage
{
    [SerializeField] private GameObject damageIdentificator;
    [SerializeField] protected Weapon currentWeapon;
    [SerializeField] protected int hp;
    private Material material;

    protected Coroutine reloadRoutine;

    public event Action onDieEvent;

    private Coroutine periodicDamageCoroutine;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        onDieEvent += StopReload;
    }
    ///////////////// Health ////////////////////
    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp = Mathf.Max(0, hp - damage);
        if (hp <= 0) onDieEvent?.Invoke();
    }
    private void TakePeriodicDamageIdentificator()
    {
        var instance = Instantiate(damageIdentificator, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), transform.rotation, transform);
        Destroy(instance, 0.5f);
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
            TakePeriodicDamageIdentificator();
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
        currentWeapon.Shoot();
        if (currentWeapon.IsEmpty)
        {
            Reload();
            return;
        }
    }
    protected void SwitchWeapon(Weapon _weapon)
    {
        if (currentWeapon == _weapon) return;
        StopReload();
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
        material.color = Color.yellow;
        while (Time.time < delay)
        {
            yield return null;
        }
        material.color = Color.white;
        currentWeapon.LoadMagazine();
        yield return null;
        reloadRoutine = null;
    }
    private void StopReload()
    {
        if (reloadRoutine != null)
        {
        StopCoroutine(reloadRoutine);
        reloadRoutine = null;
        material.color = Color.white;
        }
    }
}
