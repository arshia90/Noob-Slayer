using UnityEngine;
using UnityEngine.Tilemaps;

public class GridMover : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform cube;

    public int moveStep = 1;

    void Update()
    {
        HandleKeyboard();
        HandleMouse();
    }

    void HandleKeyboard()
    {
          if (Input.GetKeyDown(KeyCode.D))
        Move(Vector3Int.left, moveStep); 

          if (Input.GetKeyDown(KeyCode.A))
        Move(Vector3Int.right, moveStep); 

          if (Input.GetKeyDown(KeyCode.W))
        Move(new Vector3Int(0, -1, 0), moveStep); 

         if (Input.GetKeyDown(KeyCode.S))
         Move(new Vector3Int(0, 1, 0), moveStep); 


         if (Input.GetKeyDown(KeyCode.E))
         Move(new Vector3Int(-1, -1, 0), moveStep);

         if (Input.GetKeyDown(KeyCode.Q))
         Move(new Vector3Int(1, -1, 0), moveStep);
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3Int clickedCell = tilemap.WorldToCell(hit.point);
                MoveToCell(clickedCell);
            }
        }
    }

    public void Move(Vector3Int direction, int steps)
    {
        Vector3Int currentCell = tilemap.WorldToCell(cube.position);
        Vector3Int targetCell = currentCell + direction * steps;

        MoveToCell(targetCell);
    }

    public void MoveToCell(Vector3Int targetCell)
    {
        Vector3 targetWorld = tilemap.GetCellCenterWorld(targetCell);
        cube.position = targetWorld + new Vector3(0, 0.5f, 0);
    }
}