using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GridGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TilemapIndexer indexer;

    [Header("Tiles")]
    public TileBase floorTile; // تایلی که قرار است کف زمین باشد

    [Header("Settings")]
    public int startIndex = 31;
    public int endIndex = 6;
    
    // 👇 این درصدِ فضاهای خالی است (مثلاً 0.4 یعنی 40 درصد تایل‌ها حذف شوند)
    [Range(0, 1)] public float emptyDensity = 0.4f; 

    // لیست حاشیه که نباید دست بخورد
    private HashSet<int> borderIndices = new HashSet<int>
    {
        31, 32, 33, 34, 35, 36, 25, 19, 13, 7, 1, 2, 3, 4, 5, 6, 12, 18, 24, 30
    };

    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        if (tilemap == null || indexer == null || floorTile == null) return;

        // 1. ابتدا همه تایل‌های داخلی را پر کن
        FillAllInternalWithFloor();

        // 2. حالا به صورت تصادفی بعضی‌ها را خالی (null) کن
        RandomlyRemoveTiles();
        
        Debug.Log("✅ نقشه با فضاهای خالی ایجاد شد.");
    }

    void FillAllInternalWithFloor()
    {
        for (int i = 1; i <= indexer.width * indexer.height; i++)
        {
            Vector3Int cell = indexer.GetCellFromIndex(i);
            // حاشیه و مرکز همه فعلاً پر می‌شوند
            tilemap.SetTile(cell, floorTile);
        }
    }

    void RandomlyRemoveTiles()
    {
        for (int i = 1; i <= indexer.width * indexer.height; i++)
        {
            // اگر جزو حاشیه بود، به هیچ وجه حذفش نکن
            if (borderIndices.Contains(i)) continue;

            Vector3Int cell = indexer.GetCellFromIndex(i);

            // شانس خالی شدن
            if (Random.value < emptyDensity)
            {
                // تایل را حذف کن
                tilemap.SetTile(cell, null);

                // چک کن که آیا هنوز راه از 31 به 6 باز هست؟
                var path = indexer.FindPathPublic(indexer.GetCellFromIndex(startIndex), indexer.GetCellFromIndex(endIndex));
                
                if (path == null) 
                {
                    // اگر راه بسته شد، تایل را برگردان
                    tilemap.SetTile(cell, floorTile);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateLevel();
        }
    }
}
