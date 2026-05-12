using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform player;

    [Header("Vision")]
    public float viewDistance = 5f;

    [Range(0, 360)]
    public float viewAngle = 120f;

    [Header("Rotation")]
    public float rotateSpeed = 5f;

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (player == null)
            return;

        // فاصله تا پلیر
        float distance = Vector3.Distance(transform.position, player.position);

        // اگر بیرون رنج بود
        if (distance > viewDistance)
            return;

        // جهت پلیر
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        // زاویه بین جلو Enemy و پلیر
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        // اگر داخل زاویه دید بود
        if (angle < viewAngle / 2f)
        {
            RotateToPlayer();
        }
    }

    void RotateToPlayer()
    {
        Vector3 lookDir = player.position - transform.position;

        lookDir.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
        
    }
    void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;

    Gizmos.DrawWireSphere(transform.position, viewDistance);
}
}