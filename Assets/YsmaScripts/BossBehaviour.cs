using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;              // its own player reference for aiming
    [SerializeField] private BossActivationRange activationRange;

    [SerializeField] private int burstCount = 5;
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float burstCooldown = 4f;

    private float fireTimer;
    private float cooldownTimer;
    private int bulletsLeftInBurst;
    private bool isCoolingDown;
    private bool wasInRange;

    void Start()
    {
        ResetBossState();
    }

    void Update()
    {
        if (activationRange == null) return;

        // Simply read the bool from the other script
        if (!activationRange.PlayerInRange)
        {
            if (wasInRange)
            {
                ResetBossState();
                wasInRange = false;
            }
            return;
        }

        if (!wasInRange)
        {
            ResetBossState();
            wasInRange = true;
        }

        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
                bulletsLeftInBurst = burstCount;
                fireTimer = fireInterval;
            }
            return;
        }

        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Shoot();
            bulletsLeftInBurst--;

            if (bulletsLeftInBurst <= 0)
            {
                isCoolingDown = true;
                cooldownTimer = burstCooldown;
            }
            else
            {
                fireTimer = fireInterval;
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || player == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (player.position - firePoint.position).normalized;
        bullet.transform.forward = direction;
    }

    void ResetBossState()
    {
        bulletsLeftInBurst = burstCount;
        fireTimer = fireInterval;
        isCoolingDown = false;
        cooldownTimer = 0f;
    }
}