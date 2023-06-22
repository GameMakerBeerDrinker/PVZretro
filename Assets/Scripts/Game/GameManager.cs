using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Tilemap tilemap;

    private int column;
    private int row;
    private Cell[,] field;

    private float width;
    private float height;

    public Card pickedUpCard;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        tilemap = GetComponent<Tilemap>();

        NewGame();
        width = tilemap.cellSize.y;
        height = tilemap.cellSize.x;
        Debug.Log(width);
    }

    private void NewGame()
    {
        column = 9;
        row = 5;
        field = new Cell[row, column];

        //Cell[x,y]=cell.position(x,y) tilemap.WorldToCell().x,.y

        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector2Int(x, y);
                cell.cellType = Cell.CellType.Grass;
                field[x, y] = cell;
            }
        }

        

        pickedUpCard = null;
    }

    private void Update()
    {
        /*for (float i = 0; i < 10; i++)
            Debug.DrawLine(new Vector3(-7 + 1.6f * i, -6, 0), new Vector3(-7 + 1.6f * i, 4, 0));
        for (float j = 0; j < 6; j++)
            Debug.DrawLine(new Vector3(-7, -6 + 2 * j, 0), new Vector3(7.4f, -6 + 2 * j, 0));*/

        //Debug.Log(pickedUpCard);
        PutPlant();
    }

    private void PutPlant()
    {
        if(pickedUpCard != null&&Input.GetMouseButtonDown(0))
        {
            Vector3Int cellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Cell cell = GetCell(cellPosition.x, cellPosition.y);

            if (cell.cellType == Cell.CellType.Invalid)
            {
                Debug.Log("invalid");

                //Card
                pickedUpCard.cardState = Card.CardState.Ready;

                pickedUpCard = null;
            }
            else if(cell.plant==null)
            {
                Debug.Log("plant");
                float x = Random.Range(-0.08f, 0.08f);
                float y = Random.Range(-0.1f, 0.1f);

                Vector3 plantPosition = tilemap.CellToWorld(cellPosition) + new Vector3(width/2, -height/2, 0) + new Vector3(x, y, 0);
                cell.plant = Instantiate(pickedUpCard.plant, plantPosition, Quaternion.Euler(0, 0, 0));
                SunManager.instance.sun -= pickedUpCard.cost;

                field[cellPosition.x, cellPosition.y] = cell;

                //Card
                pickedUpCard.rechargingTimer = pickedUpCard.recharge;
                pickedUpCard.cardState = Card.CardState.Recharging;

                pickedUpCard = null;
            }
            
        }
        else if(pickedUpCard != null && Input.GetMouseButtonDown(1))
        {
            //Card
            pickedUpCard.cardState = Card.CardState.Ready;

            pickedUpCard = null;
        }
    }

    public void RemovePlant()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if(cell.cellType==Cell.CellType.Invalid)
        {
            return;
        }
        if(cell.plant!=null)
        {
            Destroy(cell.plant);
            cell.plant = null;
        }
    }

    private Cell GetCell(int x,int y)
    {
        if(x >= 0 && x < row && y >= 0 && y < column)
        {
            return field[x, y];
        }
        else
        {
            return new Cell();
        }
    }
}
