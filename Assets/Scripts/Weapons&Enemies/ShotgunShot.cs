//using UnityEngine;
//using System.Collections;

//public class ShotgunShot : WeaponBase
//{
//    [SerializeField] private Transform firePoint;
//    [SerializeField] private GameObject pelletPrefab;
//    [SerializeField] private int pelletCount = 8;
//    [SerializeField] private float spread = 2f;

//    private void Update()
//    {
//        HandleInput();
//    }

//    private void HandleInput()
//    {
//        if (IsReloading)
//            return;

//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            StartCoroutine(Reload());
//            return;
//        }

//        if (Input.GetMouseButtonDown(0))
//        {
//            Shoot();
//        }
//    }

//    public override void Shoot()
//    {
//        if (!CanShoot())
//            return;
//        Debug.Log("Disparando shotgun");


//        SetNextFireTime();
//        ConsumeAmmo();

//        for (int i = 0; i < pelletCount; i++)
//        {
//            SpawnPellet();
//        }
//    }

//    private void SpawnPellet()
//    {
//        Vector3 direction = firePoint.forward;

//        float randomX = Random.Range(-spread, spread) * 0.005f;
//        float randomY = Random.Range(-spread, spread) * 0.005f;

//        direction += firePoint.right * randomX;
//        direction += firePoint.up * randomY;

//        direction.Normalize();

//        Vector3 spawnPos = firePoint.position + firePoint.forward * 0.2f;

//        Instantiate(pelletPrefab, spawnPos, Quaternion.LookRotation(direction));
//    }
//}
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShot : WeaponBase
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject pelletPrefab;
    [SerializeField] private int pelletCount = 8;
    [SerializeField] private float spread = 10f;

    [SerializeField] private Collider playerCollider;

    private List<Collider> spawnedPellets = new List<Collider>();

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

        Vector3 spawnPos = firePoint.position + firePoint.forward * 0.3f;

        GameObject pellet = Instantiate(pelletPrefab, spawnPos, spreadRotation);

        Collider col = pellet.GetComponent<Collider>();

        Physics.IgnoreCollision(col, playerCollider);

        foreach (var other in spawnedPellets)
        {
            if (other != null)
                Physics.IgnoreCollision(col, other);
        }

        spawnedPellets.Add(col);
    }
}