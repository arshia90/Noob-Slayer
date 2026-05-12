 using UnityEngine;
 using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TilemapIndexer indexer;
    public Transform player;

    public Text stepText;

    void Update()
    {
        UpdateStepUI();
    }

    void UpdateStepUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3Int targetCell = indexer.tilemap.WorldToCell(hit.point);
            Vector3Int currentCell = indexer.tilemap.WorldToCell(player.position);

            var path = indexer.FindPathPublic(currentCell, targetCell);

            if (path != null)
            {
                stepText.text = "Steps: " + path.Count;
            }
            else
            {
                stepText.text = "No Path";
            }
        }
    }
}