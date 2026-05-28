using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;

    protected float Speed => speed;

    protected virtual void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    protected virtual void Move()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}