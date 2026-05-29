using UnityEngine;

public class DemonAI : EnemyBase
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private float fireRate = 1f;

    private float nextAttackTime;

    protected override void Awake()
    {
        base.Awake();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position,player.position);

        if (distance <= detectionRange)
        {
            FollowPlayer();
        }

        if (distance <= attackRange)
        {
            Attack();
        }
    }

    private void FollowPlayer()
    {
        Vector3 direction =(player.position - transform.position).normalized;

        transform.position +=direction * MoveSpeed * Time.deltaTime;

        transform.LookAt(player);
    }

    private void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + fireRate;

        Instantiate(projectilePrefab,firePoint.position,firePoint.rotation);
    }
}