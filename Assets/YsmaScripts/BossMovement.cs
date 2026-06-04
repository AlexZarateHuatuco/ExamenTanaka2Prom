using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationRadi = 8f;
    [SerializeField] private float speedSpin = 45f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float doNotHitWall = 2f;
    [SerializeField] private float wallDetectionProbe = 3f;
    [SerializeField] private LayerMask obstacleMask = ~0;
    private bool phase2Movement;

    private float currentAngle;
    private Vector3 desiredPosition;

    void Start()
    {
        currentAngle = Random.Range(0f, 360f);
        desiredPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        currentAngle += speedSpin * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        Vector3 idealPos = CalculateOrbitPosition();
        Vector3 adjustedPos = AvoidWalls(idealPos);
        transform.position = Vector3.MoveTowards(transform.position, adjustedPos, moveSpeed * Time.deltaTime);
    }

    Vector3 CalculateOrbitPosition()
    {
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad)) * rotationRadi;
        Vector3 desired = player.position + offset;
        desired.y = transform.position.y;
        return desired;
    }

    Vector3 AvoidWalls(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToTarget, out hit, Mathf.Min(wallDetectionProbe, distanceToTarget), obstacleMask))
        {
            Vector3 avoidDirection = Vector3.Cross(Vector3.up, hit.normal).normalized;
            Vector3 leftDir = avoidDirection;
            Vector3 rightDir = -avoidDirection;

            if (!Physics.Raycast(transform.position, leftDir, doNotHitWall, obstacleMask))
                return transform.position + leftDir * moveSpeed * Time.deltaTime;
            else if (!Physics.Raycast(transform.position, rightDir, doNotHitWall, obstacleMask))
                return transform.position + rightDir * moveSpeed * Time.deltaTime;
            else
                return transform.position;
        }

        return target;
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.position, rotationRadi);
    }
}
