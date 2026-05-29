using UnityEngine;

public class PlayerBullet : ProjectileBase

{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

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