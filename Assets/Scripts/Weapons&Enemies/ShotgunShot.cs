using UnityEngine;
using System.Collections;

public class ShotgunShot : WeaponBase
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject pelletPrefab;
    [SerializeField] private int pelletCount = 8;
    [SerializeField] private float spread = 10f;

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
        Debug.Log("Disparando shotgun");


        SetNextFireTime();
        ConsumeAmmo();

        for (int i = 0; i < pelletCount; i++)
        {
            SpawnPellet();
        }
    }

    private void SpawnPellet()
    {
        Quaternion spreadRotation = firePoint.rotation *Quaternion.Euler(Random.Range(-spread, spread),Random.Range(-spread, spread),0f);

        Instantiate(pelletPrefab,firePoint.position,spreadRotation);
    }
}
