using UnityEngine;
using System.Collections;

public class ShotgunShot : WeaponBase
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private int pellets = 8;
    [SerializeField] private float spread = 0.1f;

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

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        if (!CanShoot())
            return;

        SetNextFireTime();
        ConsumeAmmo();

        for (int i = 0; i < pellets; i++)
        {
            ShootPellet();
        }
    }

    private void ShootPellet()
    {
        Vector3 direction = playerCamera.transform.forward;

        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);

        Ray ray = new Ray(playerCamera.transform.position,direction);

        if (Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            if (hit.collider.TryGetComponent(out EnemyBase enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}