using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private int maxHealth = 15;
    [SerializeField] private float moveSpeed = 5f;

    protected int CurrentHealth;
    protected float MoveSpeed => moveSpeed;

    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}