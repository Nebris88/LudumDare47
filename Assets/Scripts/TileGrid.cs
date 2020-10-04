using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class TileGrid : MonoBehaviour
{
    public GameObject tilePrefab;

    public GameObject passiveTiles;

    public int minWidth, maxWidth, minHeight, maxHeight;

    private GameObject[,] tiles;

    public void initializeGrid()
    {
        Debug.Log($"Init grid ");

        List<GameObject> foundTiles = new List<GameObject>();

        Vector3 origin;
        for (int x = minWidth; x < maxWidth; x++)
        {
            for (int y = minHeight; y < maxHeight; y++)
            {
                origin = new Vector3(x + 0f, y + 0f, -1);
                RaycastHit2D[] hit2D = Physics2D.GetRayIntersectionAll(new Ray(origin, Vector3.forward));

                for(int i = 0; i < hit2D.Length; i++)
                {
                    if (hit2D[i].collider.gameObject.layer == 8)
                    {
                        if (!foundTiles.Contains(hit2D[i].collider.gameObject)) foundTiles.Add(hit2D[i].collider.gameObject);

                        GameObject newTile = Instantiate(tilePrefab);
                        newTile.transform.SetParent(passiveTiles.transform);
                        newTile.transform.position = new Vector3(x,y,0);
                        newTile.name = $"Tile at {x}, {y}";
                        //Debug.Log($"Hit at {x}, {y}");
                        //Debug.DrawRay(origin, Vector3.forward * 10, Color.red);
                    }
                }
            }
        }

        foundTiles.ForEach(ft => 
        {
            DestroyImmediate(ft);
        });
    }

}
