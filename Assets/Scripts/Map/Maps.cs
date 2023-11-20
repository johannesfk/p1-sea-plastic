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


    readonly string map1Text =
        "WWWWWWWWWWWFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM\n" +
        "FFFFFSSSSFFFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFSSSSFFFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFSSDDDFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFDDDDDDDDDFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFDDDDDDDDDFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "FFFFFFFFFFFFFFFFFFFFMMMMMMMMMMMMMMMMMMWMMMMMMMMMMMMMMM" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
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

    /* readonly string map1Text =
        "FFWFF\n" +
        "MMWMM" +
        "SSWSS" +
        "DDWDD" +
        "XXWXX"; */


    // Start is called before the first frame update
    void OnEnable()
    {

        int map1CellCountX = map1Text.IndexOf('\n');
        int map1CellCountZ = (map1Text.Length - 1) / map1CellCountX;

        /* Debug.Log("Cell collumns " + map1CellCountX);
        Debug.Log("cell rows " + map1CellCountZ);
        Debug.Log("cell count " + map1CellCountX * map1CellCountZ); */

        if (instance == null)
        {
            instance = this;
        }
        mapList = new List<Map>();



        Map map1 = new Map(CellCountX, CellCountZ);

        terrainType[,] map2Terrain = ConvertTo2DArray(map1Text, map1CellCountX, map1CellCountZ);
        Map map2 = new Map(map2Terrain.GetLength(1), map2Terrain.GetLength(0));

        Debug.Log(map2Terrain.GetLength(1) + " ✕ " + map2Terrain.GetLength(0));

        /* Debug.Log
        (
            map2Terrain[0, 0] + " " + map2Terrain[0, 1] + " " + map2Terrain[0, 2] + " " + map2Terrain[0, 3] + "\n" +
            map2Terrain[1, 0] + " " + map2Terrain[1, 1] + " " + map2Terrain[1, 2] + " " + map2Terrain[1, 3] + "\n" +
            map2Terrain[2, 0] + " " + map2Terrain[2, 1] + " " + map2Terrain[2, 2] + " " + map2Terrain[2, 3] + "\n" +
            map2Terrain[3, 0] + " " + map2Terrain[3, 1] + " " + map2Terrain[3, 2] + " " + map2Terrain[3, 3] + "\n" +
            map2Terrain[4, 0] + " " + map2Terrain[4, 1] + " " + map2Terrain[4, 2] + " " + map2Terrain[4, 3]
        ); */



        /// Map 1 
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

        // Map 1 end

        map1.layout = borderedLayout;
        map2.layout = map2Terrain;
        mapList.Add(map1);
        mapList.Add(map2);

    }

    static terrainType[,] ConvertTo2DArray(string mapText, int width, int height)
    {
        terrainType[,] terrainMap = new terrainType[height, width];
        int charIndex = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                while (mapText[charIndex] == '\n' || mapText[charIndex] == '\r')
                {
                    charIndex++;
                }
                char terrainChar = mapText[charIndex];
                terrainMap[i, j] = GetTerrainType(terrainChar);
                charIndex++;
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



