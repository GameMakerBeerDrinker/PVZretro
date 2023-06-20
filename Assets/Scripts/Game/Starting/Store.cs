using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Store : MonoBehaviour
{
    public static Store instance;

    public GameObject[] cards;
    public GameObject dimImage;
    public Tilemap tilemap;
    //private int row;
    private int column=8;

    public float width;
    public float height;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        tilemap = GetComponent<Tilemap>();
        width = tilemap.cellSize.x;
        height = tilemap.cellSize.y;
    }

    private void Start()
    {
        for(int i=0;i<cards.Length;i++)
        {
            Vector3Int tilePosition = new Vector3Int(i / column, i % column, 0);
            Vector3 position = tilemap.CellToWorld(tilePosition) + new Vector3(width/2, -height/2, 0);
            Instantiate(cards[i], position, Quaternion.Euler(0, 0, 0));
            Instantiate(dimImage, position, Quaternion.Euler(0, 0, 0));
            Instantiate(dimImage, position, Quaternion.Euler(0, 0, 0));
        }
    }
}
