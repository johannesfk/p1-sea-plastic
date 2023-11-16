using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexGrid;
using static HexGrid.terrainType;

public class Map : MonoBehaviour
{
    public int CellCountX;
    public int CellCountZ;
    public terrainType[,] layout;
    public Map(int width, int height)
    {
        CellCountX = width;
        CellCountZ = height;
        layout = new terrainType[width, height];
    }
}
public class Maps : MonoBehaviour
{
    public static Maps instance;
    public List<Map> mapList;

    public const int CellCountX = 8;
    private const int CellCountZ = 6;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        mapList = new List<Map>();

        Map map1 = new Map(CellCountX, CellCountZ);


        for (int i = 0; i < map1.layout.GetLength(0); i++)
        {
            for (int j = 0; j < map1.layout.GetLength(1); j++)
            {
                map1.layout[i, j] = terrainType.water;
            }
        }

        terrainType[,] mapLayout = new terrainType[CellCountX, CellCountZ] {
            { water, water, water, water, water, water },
            { water, forest, forest, forest, forest, water },
            { water, forest, mountain, mountain, forest, water },
            { water, forest, forest, mountain, forest, water },
            { water, forest, forest, forest, forest, water },
            { water, boatCleaner, water, water, water, water },
            { water, water, water, water, water, water },
            { water, water, water, water, water, water }
        };

        // Create a new array that is 10x10 larger than the original
        terrainType[,] borderedLayout = new terrainType[mapLayout.GetLength(0) + 10, mapLayout.GetLength(1) + 10];
        // Fill the new array with water
        for (int i = 0; i < borderedLayout.GetLength(0); i++)
        {
            for (int j = 0; j < borderedLayout.GetLength(1); j++)
            {
                borderedLayout[i, j] = terrainType.water;
            }
        }

        // Copy the original map into the middle of the new array
        for (int i = 0; i < mapLayout.GetLength(0); i++)
        {
            for (int j = 0; j < mapLayout.GetLength(1); j++)
            {
                borderedLayout[i + 5, j + 5] = mapLayout[i, j];
            }
        }

        map1.layout = borderedLayout;
        mapList.Add(map1);

    }
}

