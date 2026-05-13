using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyVision : MonoBehaviour
{
    public Transform player;

    [Header("Front Vision")]
    public float frontDistance = 5f;

    [Range(0, 360)]
    public float frontAngle = 180f;

    [Header("Back Vision")]
    public float backDistance = 2f;

    [Range(0, 360)]
    public float backAngle = 180f;

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

        // جهت پلیر
        Vector3 dirToPlayer =
            (player.position - transform.position).normalized;

        // فاصله تا پلیر
        float distance =
            Vector3.Distance(transform.position, player.position);

        // زاویه دید جلو
        float frontCheck =
            Vector3.Angle(transform.forward, dirToPlayer);

        // زاویه دید پشت
        float backCheck =
            Vector3.Angle(-transform.forward, dirToPlayer);

        // دید جلو
        if (distance <= frontDistance &&
            frontCheck <= frontAngle / 2f)
        {
            RotateToPlayer();
        }

        // دید پشت
        if (distance <= backDistance &&
            backCheck <= backAngle / 2f)
        {
            RotateToPlayer();
        }
    }

    void RotateToPlayer()
    {
        Vector3 lookDir =
            player.position - transform.position;

        lookDir.y = 0;

        Quaternion targetRotation =
            Quaternion.LookRotation(lookDir);

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotateSpeed
        );
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // ================= FRONT =================

        Handles.color = new Color(1, 0, 0, 0.25f);

        Vector3 frontLeft =
            Quaternion.Euler(0, -frontAngle / 2, 0)
            * transform.forward;

        Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            frontLeft,
            frontAngle,
            frontDistance
        );

        // ================= BACK =================

        Handles.color = new Color(0, 0, 1, 0.25f);

        Vector3 backDirection = -transform.forward;

        Vector3 backLeft =
            Quaternion.Euler(0, -backAngle / 2, 0)
            * backDirection;

        Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            backLeft,
            backAngle,
            backDistance
        );

        // ================= FORWARD LINE =================

        Gizmos.color = Color.green;

        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * frontDistance
        );
    }
#endif
}