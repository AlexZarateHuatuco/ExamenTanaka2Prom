using UnityEngine;
using System.Collections;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private int damage = 5;

    protected int CurrentAmmo;
    protected bool IsReloading;
    protected float NextFireTime;

    protected int Damage => damage;

    protected virtual void Awake()
    {
        CurrentAmmo = maxAmmo;
    }

    protected virtual bool CanShoot()
    {
        return !IsReloading &&CurrentAmmo > 0 &&Time.time >= NextFireTime;
    }

    protected virtual void ConsumeAmmo()
    {
        CurrentAmmo--;
    }

    protected bool HasAmmo()
    {
        return CurrentAmmo > 0;
    }

    protected virtual void SetNextFireTime()
    {
        NextFireTime = Time.time + fireRate;
    }

    protected virtual IEnumerator Reload()
    {
        IsReloading = true;
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmo = maxAmmo;
        IsReloading = false;

        Debug.Log("Recargando...");
        Debug.Log("Recarga completa");
    }

    public abstract void Shoot();
}