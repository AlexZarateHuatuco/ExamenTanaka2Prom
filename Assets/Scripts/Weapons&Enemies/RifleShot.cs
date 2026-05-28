using UnityEngine;
using System.Collections;

public class RifleShot : WeaponBase
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (IsReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (!CanShoot())
            return;

        Debug.Log("Disparando");

        SetNextFireTime();
        ConsumeAmmo();

        Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
    }
}