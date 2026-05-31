using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyVision : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public TilemapIndexer indexer;

    [Header("Front Vision")]
    public float frontDistance = 6f;

    [Range(0, 360)]
    public float frontAngle = 180f;

    [Header("Back Vision")]
    public float backDistance = 2f;

    [Range(0, 360)]
    public float backAngle = 180f;

    [Header("Chase")]
    public float loseDistance = 10f;

    public float moveSpeed = 3f;

    public float stopDistance = 1f;

    [Header("Rotation")]
    public float rotateSpeed = 5f;

    bool isChasing;
    bool isMoving;

    void Update()
    {
        DetectPlayer();

        if (isChasing)
        {
            RotateToPlayer();

            float dist =
                Vector3.Distance(
                    transform.position,
                    player.position
                );

            // Lose Aggro
            if (dist > loseDistance)
            {
                isChasing = false;
            }

            // Move
            if (!isMoving)
            {
                MoveOneStep();
            }
        }
    }

    void DetectPlayer()
    {
        if (player == null)
            return;

        Vector3 dirToPlayer =
            (player.position - transform.position).normalized;

        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );

        // FRONT
        float frontCheck =
            Vector3.Angle(
                transform.forward,
                dirToPlayer
            );

        bool inFrontVision =
            distance <= frontDistance &&
            frontCheck <= frontAngle / 2f;

        // BACK
        float backCheck =
            Vector3.Angle(
                -transform.forward,
                dirToPlayer
            );

        bool inBackVision =
            distance <= backDistance &&
            backCheck <= backAngle / 2f;

        if (inFrontVision || inBackVision)
        {
            isChasing = true;
        }
    }

    void MoveOneStep()
    {
        Vector3Int enemyCell =
            indexer.tilemap.WorldToCell(transform.position);

        Vector3Int playerCell =
            indexer.tilemap.WorldToCell(player.position);

        List<Vector3Int> path =
            indexer.FindPathPublic(
                enemyCell,
                playerCell
            );

        if (path == null)
            return;

        if (path.Count == 0)
            return;

        float dist =
            Vector3.Distance(
                transform.position,
                player.position
            );

        if (dist <= stopDistance)
            return;

        // فقط اولین Step
        Vector3Int nextCell = path[0];

        Vector3 target =
            indexer.tilemap.GetCellCenterWorld(nextCell);

        StartCoroutine(SmoothMove(target));
    }

    IEnumerator SmoothMove(Vector3 target)
    {
        isMoving = true;

        Vector3 start = transform.position;

        target += new Vector3(0, 0.5f, 0);

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;

            transform.position =
                Vector3.Lerp(start, target, t);

            yield return null;
        }

        transform.position = target;

        isMoving = false;
    }

    void RotateToPlayer()
    {
        Vector3 lookDir =
            player.position - transform.position;

        lookDir.y = 0;

     
            Quaternion targetRotation =
            Quaternion.LookRotation(lookDir);

        transform.rotation =
            Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotateSpeed
            );
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // FRONT
        Handles.color =
            new Color(1, 0, 0, 0.25f);

        Vector3 frontLeft =
            Quaternion.Euler(
                0,
                -frontAngle / 2,
                0
            ) * transform.forward;

        Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            frontLeft,
            frontAngle,
            frontDistance
        );

        // BACK
        Handles.color =
            new Color(0, 0, 1, 0.25f);

        Vector3 backDir =
            -transform.forward;

        Vector3 backLeft =
            Quaternion.Euler(
                0,
                -backAngle / 2,
                0
            ) * backDir;

        Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            backLeft,
            backAngle,
            backDistance
        );
    }
#endif
}