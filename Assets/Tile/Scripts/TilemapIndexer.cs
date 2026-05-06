using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TilemapIndexer : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform cube;

    public int width = 6;
    public int height = 6;

    public float moveSpeed = 5f;

    bool isMoving = false;

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

                TryMove(index);
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

    // 🚶 حرکت امن
    void TryMove(int targetIndex)
    {
        int maxIndex = width * height;
        targetIndex = Mathf.Clamp(targetIndex, 1, maxIndex);

        Vector3Int currentCell = tilemap.WorldToCell(cube.position);
        Vector3Int targetCell = GetCellFromIndex(targetIndex);

        // ❌ روی خودش
        if (currentCell == targetCell)
            return;

        // ❌ Tile مقصد وجود ندارد
        if (!tilemap.HasTile(targetCell))
            return;

        // 🔍 اول مسیر رو پیدا کن
        List<Vector3Int> path = FindPath(currentCell, targetCell);

        if (path == null || path.Count == 0)
            return;

        // ✅ حالا حرکت رو شروع کن
        StopAllCoroutines();
        StartCoroutine(MoveRoutine(path));
    }

    // 🧠 حرکت
    IEnumerator MoveRoutine(List<Vector3Int> path)
    {
        isMoving = true;

        foreach (var cell in path)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(cell);
            yield return StartCoroutine(SmoothMove(worldPos));
        }

        isMoving = false;
    }

    // 🔍 Pathfinding (با مورب + جلوگیری از باگ)
    List<Vector3Int> FindPath(Vector3Int start, Vector3Int target)
    {
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        queue.Enqueue(start);
        cameFrom[start] = start;

        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.right,
            Vector3Int.left,
            new Vector3Int(0,1,0),
            new Vector3Int(0,-1,0),

            new Vector3Int(1,1,0),
            new Vector3Int(-1,1,0),
            new Vector3Int(1,-1,0),
            new Vector3Int(-1,-1,0)
        };

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            if (current == target)
                break;

            foreach (var dir in directions)
            {
                Vector3Int next = current + dir;

                if (!tilemap.HasTile(next))
                    continue;

                // ❌ جلوگیری از رد شدن از گوشه
                if (dir.x != 0 && dir.y != 0)
                {
                    Vector3Int side1 = new Vector3Int(current.x + dir.x, current.y, 0);
                    Vector3Int side2 = new Vector3Int(current.x, current.y + dir.y, 0);

                    if (!tilemap.HasTile(side1) || !tilemap.HasTile(side2))
                        continue;
                }

                if (cameFrom.ContainsKey(next))
                    continue;

                queue.Enqueue(next);
                cameFrom[next] = current;
            }
        }

        if (!cameFrom.ContainsKey(target))
            return null;

        List<Vector3Int> path = new List<Vector3Int>();
        Vector3Int temp = target;

        while (temp != start)
        {
            path.Add(temp);
            temp = cameFrom[temp];
        }

        path.Reverse();
        return path;
    }

    // 🎬 حرکت نرم + فیکس نهایی
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

        // 👇 خیلی مهم
        cube.position = target + new Vector3(0, 0.5f, 0);
    }
    public List<Vector3Int> FindPathPublic(Vector3Int start, Vector3Int target)
{
    return FindPath(start, target);
}

}