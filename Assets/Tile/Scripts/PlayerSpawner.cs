using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public TilemapIndexer indexer;
    public Transform player;

    public int spawnIndex = 1;

    void Start()
    {
        SpawnAtIndex(spawnIndex);
    }

    public void SpawnAtIndex(int index)
    {
        int maxIndex = indexer.width * indexer.height;

        // محدود کردن
        index = Mathf.Clamp(index, 1, maxIndex);

        Vector3Int cell = indexer.GetCellFromIndex(index);

        // ❌ اگر Tile نداشت
        if (!indexer.tilemap.HasTile(cell))
        {
            Debug.Log("این Tile وجود ندارد!");
            return;
        }

        Vector3 worldPos = indexer.tilemap.GetCellCenterWorld(cell);

        player.position = worldPos + new Vector3(0, 0.5f, 0);
    }
}