using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;

    [SerializeField] private int burstCount = 5;
    [SerializeField] private float fireInterval = 1f;
    [SerializeField] private float burstCooldown = 4f;

    private float fireTimer;
    private float cooldownTimer;
    private int bulletsLeftInBurst;
    private bool isCoolingDown;

    void Start()
    {
        bulletsLeftInBurst = burstCount;
        fireTimer = fireInterval;
        isCoolingDown = false;
        cooldownTimer = 0f;
    }
    void Update()
    {

        if (isCoolingDown==true)
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        //amigis gonna b honest jere - me robe el codigo de aribe de un proyecto viejo corr//obotado por... 
        //tahdah!!! codigo ajeno de un foro. pls dounnut hate m3 uwu
        Vector3 direction = (player.position - firePoint.position).normalized;
        bullet.transform.forward = direction;
    }
}
