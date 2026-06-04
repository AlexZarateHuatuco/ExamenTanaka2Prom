using UnityEngine;

public class BossActivationRange : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float activationRadius = 10f;

    public bool PlayerInRange;  

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        PlayerInRange = distance <= activationRadius;  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}