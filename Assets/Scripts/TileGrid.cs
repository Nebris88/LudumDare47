using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class TileGrid : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite backGroundSprite;
    public Sprite climbableSprite;

    public GameObject background, passiveTiles;

    public int minWidth, maxWidth, minHeight, maxHeight;

    private GameObject[,] tiles;

    public void initializeGrid()
    {
        Debug.Log($"Init grid ");

        List<GameObject> foundTiles = new List<GameObject>();

        Vector3 origin;
        bool foundBackground = false;
        bool foundPassive = false;
        for (int x = minWidth; x < maxWidth; x++)
        {
            for (int y = minHeight; y < maxHeight; y++)
            {
                foundBackground = false;
                foundPassive = false;
                origin = new Vector3(x + 0f, y + 0f, -1);
                RaycastHit2D[] hit2D = Physics2D.GetRayIntersectionAll(new Ray(origin, Vector3.forward));

                for (int i = 0; i < hit2D.Length; i++)
                {
                    if (hit2D[i].collider.gameObject.layer == 10)
                    {
                        foundPassive = true;
                    }
                    if (hit2D[i].collider.gameObject.layer == 8 && !foundPassive)
                    {
                        if (!foundTiles.Contains(hit2D[i].collider.gameObject)) foundTiles.Add(hit2D[i].collider.gameObject);

                        GameObject newTile = Instantiate(tilePrefab);
                        newTile.transform.SetParent(passiveTiles.transform);
                        newTile.transform.position = new Vector3(x, y, 0);
                        newTile.name = $"Tile at {x}, {y}";
                        newTile.layer = 10;
                        foundPassive = true;
                        if (hit2D[i].collider.gameObject.tag == "Climbable")
                        {
                            newTile.GetComponent<SpriteRenderer>().sprite = climbableSprite;
                        }
                        //Debug.Log($"Hit at {x}, {y}");
                        //Debug.DrawRay(origin, Vector3.forward * 10, Color.red);
                    }
                    if (hit2D[i].collider.gameObject.layer == 9)
                    {
                        //Destroy any background tiles if there is a passive tile in front
                        if (foundPassive)
                        {
                            DestroyImmediate(hit2D[i].collider.gameObject);
                        }
                        else
                        {
                            foundBackground = true;
                        }
                    }
                }

                if (!foundBackground && !foundPassive)
                {
                    //Create new background tile if no passive tile in front and bg tile doesn't already exist
                    GameObject newTile = Instantiate(tilePrefab);
                    newTile.transform.SetParent(background.transform);
                    newTile.transform.position = new Vector3(x, y, 0);
                    newTile.name = $"Wall at {x}, {y}";
                    newTile.layer = 9;
                    newTile.GetComponent<SpriteRenderer>().sprite = backGroundSprite;//.color = new Color(.5f, .5f, .5f);
                    newTile.GetComponent<SpriteRenderer>().sortingOrder = -5;
                    DestroyImmediate(newTile.GetComponent<BoxCollider2D>());
                }
            }
        }

        foundTiles.ForEach(ft => DestroyImmediate(ft));
    }
}
