using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private int damage = 5;

    private Rigidbody rb;
    protected int Damage => damage;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
        }

        Destroy(gameObject, lifeTime);
    }
}