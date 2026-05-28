using UnityEngine;

public class ShotgunPellet : ProjectileBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }

        Destroy(gameObject);
    }
}