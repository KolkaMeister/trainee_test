using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int magazineSize;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected int bulletsInMagazine;
    [SerializeField] protected Cooldown fireRateCooldown;
    [SerializeField] protected float spreadValue;
    [SerializeField] protected float force;
    [SerializeField] protected Bullet bullet;
    public float ReloadTime => reloadTime;
    public bool IsMagazineFull => bulletsInMagazine >= magazineSize;

    public bool IsEmpty => bulletsInMagazine <= 0;

    public Cooldown FireRateCooldown => fireRateCooldown;

    public abstract void Shoot();


    protected void Start()
    {
        LoadMagazine();
    }
    public void LoadMagazine()
    {
        bulletsInMagazine = magazineSize;
    }

}
