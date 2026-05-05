using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TilemapIndexer : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform cube;

    public int width = 6;
    public int height = 6;

    public float moveSpeed = 5f;

    void Update()
    {
        HandleMouse();
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3Int cell = tilemap.WorldToCell(hit.point);

                int index = GetIndex(cell);

                Debug.Log("Index: " + index);

                MoveStepByStep(index);
            }
        }
    }

    // 🔢 Cell → Index
    int GetIndex(Vector3Int cellPos)
    {
        int x = cellPos.x - tilemap.cellBounds.xMin;
        int y = cellPos.y - tilemap.cellBounds.yMin;

        int invertedX = (width - 1) - x;

        return y * width + invertedX + 1;
    }

    // 🔄 Index → Cell
    public Vector3Int GetCellFromIndex(int index)
    {
        int maxIndex = width * height;

        // محدود کردن
        index = Mathf.Clamp(index, 1, maxIndex);

        index -= 1;

        int y = index / width;
        int x = index % width;

        int realX = (width - 1) - x;

        return new Vector3Int(
            realX + tilemap.cellBounds.xMin,
            y + tilemap.cellBounds.yMin,
            0
        );
    }

    // 🚶 حرکت پله‌ای
    public void MoveStepByStep(int targetIndex)
    {
        int maxIndex = width * height;

        // محدود کردن
        targetIndex = Mathf.Clamp(targetIndex, 1, maxIndex);

        StopAllCoroutines();
        StartCoroutine(MoveRoutine(targetIndex));
    }

    IEnumerator MoveRoutine(int targetIndex)
    {
        Vector3Int currentCell = tilemap.WorldToCell(cube.position);
        Vector3Int targetCell = GetCellFromIndex(targetIndex);

        // ❌ اگر مقصد Tile نداشت
        if (!tilemap.HasTile(targetCell))
        {
            Debug.Log("maghsad vojod nadard");
            yield break;
        }

        while (currentCell != targetCell)
        {
            // حرکت مورب
            if (currentCell.x < targetCell.x)
                currentCell.x += 1;
            else if (currentCell.x > targetCell.x)
                currentCell.x -= 1;

            if (currentCell.y < targetCell.y)
                currentCell.y += 1;
            else if (currentCell.y > targetCell.y)
                currentCell.y -= 1;

            // ❌ اگر مسیر Tile نداشت
            if (!tilemap.HasTile(currentCell))
            {
                Debug.Log("masir ghat shode");
                yield break;
            }

            Vector3 worldPos = tilemap.GetCellCenterWorld(currentCell);

            yield return StartCoroutine(SmoothMove(worldPos));
        }
    }

    // 🎬 حرکت نرم
    IEnumerator SmoothMove(Vector3 target)
    {
        Vector3 start = cube.position;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            cube.position = Vector3.Lerp(start, target + new Vector3(0, 0.5f, 0), t);
            yield return null;
        }
    }
}