using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HexGrid;
using static HexGrid.terrainType;

public class Map
{
    public int CellCountX;
    public int CellCountZ;
    public terrainType[,] layout;
    public Map(int cellCountX, int cellCountZ)
    {
        this.CellCountX = cellCountX;
        this.CellCountZ = cellCountZ;
        this.layout = new terrainType[CellCountX, CellCountZ];
    }
}
public class Maps : MonoBehaviour
{
    public static Maps instance;
    public List<Map> mapList;

    private const int CellCountX = 8;
    private const int CellCountZ = 6;

    private const int map1CellCountX = 52;
    private const int map1CellCountZ = 24;

    string map1Text =
        "WWWWWWWWWWWFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFSSSSFFFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFSSSSFFFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFSSDDDFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFDDDDDDDDDFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFDDDDDDDDDFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ" +
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" +
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" +
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" +
        "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV" +
        "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV" +
        "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV" +
        "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV" +
        "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV" +
        "VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV";


    // Start is called before the first frame update
    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        mapList = new List<Map>();



        Map map1 = new Map(CellCountX, CellCountZ);

        terrainType[,] map2Terrain = ConvertTo2DArray(map1Text, map1CellCountX, map1CellCountZ);
        Map map2 = new Map(map2Terrain.GetLength(1), map2Terrain.GetLength(0));


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
        map2.layout = map2Terrain;
        mapList.Add(map1);
        mapList.Add(map2);

    }

    static terrainType[,] ConvertTo2DArray(string mapText, int width, int height)
    {
        terrainType[,] terrainMap = new terrainType[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                char terrainChar = mapText[i * width + j];
                terrainMap[i, j] = GetTerrainType(terrainChar);
            }
        }

        return terrainMap;
    }

    static terrainType GetTerrainType(char terrainChar)
    {
        switch (terrainChar)
        {
            case 'F':
                return terrainType.forest;
            case 'M':
                return terrainType.mountain;
            case 'S':
                return terrainType.snow;
            case 'D':
                return terrainType.desert;
            case 'W':
                return terrainType.water;
            case 'Z':
                return terrainType.riverZ;
            case 'X':
                return terrainType.riverX;
            case 'C':
                return terrainType.riverXZ;
            case 'V':
                return terrainType.riverZX;
            default:
                throw new ArgumentException($"Invalid terrain character: {terrainChar}");
        }
    }
}



