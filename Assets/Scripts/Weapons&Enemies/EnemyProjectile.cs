using UnityEngine;

public class EnemyProjectile : ProjectileBase
{
    [SerializeField] private int damage = 3;

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}