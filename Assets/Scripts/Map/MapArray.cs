using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapArray : MonoBehaviour
{   
    [SerializeField] public Transform[,] currentMapArray;
    [SerializeField] public Transform[,] newMapArray;
    [SerializeField] Transform[] tiles;

    public int columns;
    public int rows;
    public Transform map;
    private Vector3 _camerOffset;

    private float _sizeX;
    private float _sizeY;

    private void Start()
    {
        currentMapArray = new Transform[columns, rows];
        newMapArray = new Transform[columns, rows];
        tiles = new Transform[9];
        int counter = 0;
        foreach (Transform tile in transform)
        {
            tiles[counter] = tile;
            counter++;
        }
        counter = 0;
        for (int j = 0; j < columns; j++)
        {
            for (int k = 0; k < rows; k++)
            {
                currentMapArray[j, k] = tiles[counter];
                counter++;
            }
        }

        _sizeX = currentMapArray[0, 0].GetComponent<BoxCollider2D>().size.x * map.localScale.x;
        _sizeY = currentMapArray[0, 0].GetComponent<BoxCollider2D>().size.y * map.localScale.y;   

        newMapArray = currentMapArray;
        //UpdateMap();
    }

    private void Update()
    {
        bool outOfScreen = Camera.main.CheckLocationWithScreenBounds(currentMapArray[1, 1].position, out CameraExtensions.Side side);

        if (outOfScreen)
        {
            switch (side)
            {
                //Sides are reversed
                case CameraExtensions.Side.Left:
                    RemapGridLeft();
                    break;
                case CameraExtensions.Side.Right:
                    RemapGridRight();
                    break;
                case CameraExtensions.Side.Top:
                    RemapGridTop();
                    break;
                case CameraExtensions.Side.Bottom:
                    RemapGridBottom();
                    break;
                case CameraExtensions.Side.None:
                    break;
                default:
                    break;
            }
        }
    }
    void RemapGridLeft()
    {
        for (int i = 0; i < rows; i++)
        {
            (newMapArray[0, i], newMapArray[1, i], newMapArray[2, i]) = (newMapArray[1, i], newMapArray[2, i], newMapArray[0, i]);
            UpdateMap(new Vector2(1, i), new Vector2(i, 2));
        }
        Debug.Log("Left");
    }
    void RemapGridRight()
    {
        for (int i = 0; i < rows; i++)
        {
            (newMapArray[0, i], newMapArray[1, i], newMapArray[2, i]) = (newMapArray[2, i], newMapArray[0, i], newMapArray[1, i]);
        }
    }

    void RemapGridTop()
    {
        for (int i = 0; i < rows; i++)
        {
            (newMapArray[i, 0], newMapArray[i, 1], newMapArray[i, 2]) = (newMapArray[i, 1], newMapArray[i, 2], newMapArray[i, 0]);
        }
    }

    void RemapGridBottom()
    {
        for (int i = 0; i < rows; i++)
        {
            (newMapArray[i, 0], newMapArray[i, 1], newMapArray[i, 2]) = (newMapArray[i, 2], newMapArray[i, 0], newMapArray[i, 1]);
        }
    }

    public void UpdateMap(Vector2 direction, Vector2 tile)
    {
        //float offset = _sizeX * 2;

        //Vector2 targetPos = new Vector2(newMapArray[(int)tile.x, (int)tile.y].position.x + (offset * direction.x), newMapArray[(int)tile.x, (int)tile.y].position.y + (offset * direction.y));

        //newMapArray[(int)tile.x, (int)tile.y].position = targetPos;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                newMapArray[i, j].position = new Vector3(i * _sizeX - _sizeX, -j * _sizeY + _sizeY, 0);
            }
        }
        currentMapArray = newMapArray;
    }
}