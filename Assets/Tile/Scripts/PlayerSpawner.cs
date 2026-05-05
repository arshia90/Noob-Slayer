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
        Vector3Int cell = indexer.GetCellFromIndex(index);

        Vector3 worldPos = indexer.tilemap.GetCellCenterWorld(cell);

        player.position = worldPos + new Vector3(0, 0.5f, 0);
    }
}