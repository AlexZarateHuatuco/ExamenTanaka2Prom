using UnityEngine;
using System.Collections;

public class RifleShot : WeaponBase
{
    [SerializeField] private Camera playerCamera;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (IsReloading) return;

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
        if (!CanShoot()) return;

        SetNextFireTime();
        ConsumeAmmo();

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.TryGetComponent(out EnemyBase enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}