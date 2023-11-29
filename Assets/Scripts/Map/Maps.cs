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
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWW\n" +
        "WWWWWWWSSSWWWWWWWWSWSWWWWWWW" +
        "SSSWWWWSSWWWWWWWSSSSSSSWWWWW" +
        "SSFFSWWWSWFWWFFFFFFFFFFFWWWW" +
        "FFFFSSWWWWWWFFFFFFFFFFFWWWWW" +
        "WFFFFFFFWWWFWWFFFFFFFFFFWWWW" +
        "FFFFFFFWWWWWFMMFFDDDDFFWFWWW" +
        "WFFFFFWWWWWFFMFWWDDDDDFWFWWW" +
        "WFFFWWWWWWWWWWWDDDDDMFFWWWWW" +
        "WWFWWWWWWWWDDDDVDWDDDFFFWWWW" +
        "WFWWWWWWWWDDDDDVDWFFWFWWWWWW" +
        "WWXWWWWWWWDDDDDDVWWFWWFWWWWW" +
        "WWFFFFWWWFFFFFFFDWWWWWWWWWWW" +
        "WWFFFFFWWWFFFFFFFWWWWWFFWWWW" +
        "WWFFFFFFWWWWFMFFWWWWWWFWWFFW" +
        "WWWFFFFFWWWWWFFFWWWWWWWWWWFW" +
        "WWWFFFFWWWWWFFFWFWWWWWWDDWWW" +
        "WWWWFFFWWWWWWFFWFWWWWWWDDDDW" +
        "WWWWFFWWWWWWWFWWWWWWWWDDDDDW" +
        "WWWWFFWWWWWWWWWWWWWWWWWDWDDW" +
        "WWWWFWWWWWWWWWWWWWWWWWWWWDWF" +
        "WWWWWFWWWWWWWWWWWWWWWWWWWWWF" +
        "WWWWWWWWWWWWWWWWWWWWWWWWWWWW" +
        "WWWWWWWWWWSSSSSSSSWWWWWWWWWW" +
        "WWSSSSSSSSSSSSSSSSSSSSSSWWWW";

    readonly string map0Text =
        "WWWWWWWW\n" +
        "FFFFFSSS" +
        "FFFFFSSS" +
        "FFFFFFFS" +
        "FFFFFFFD" +
        "FFFFFFFD" +
        "FFFFFFFD" +
        "FFFFFFFD" +
        "FFFFFFFD";


    // Start is called before the first frame update
    void OnEnable()
    {

        int map0Width = map0Text.IndexOf('\n');
        int map0Height = (map0Text.Length - 1) / map0Width;

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


        Debug.Log("map 0 " + map0Width + "x" + map0Height);

        terrainType[,] map0Terrain = ConvertTo2DArray(map0Text, map0Width, map0Height);

        terrainType[,] map1Terrain = ConvertTo2DArray(map1Text, map1CellCountX, map1CellCountZ);





        Debug.Log(map1Terrain.GetLength(1) + " ✕ " + map1Terrain.GetLength(0));


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



        // Map 1 end
        Map map0 = new Map(map0Width, map0Height);
        Map map1 = new Map(map1Terrain.GetLength(1), map1Terrain.GetLength(0));
        Map map2 = new Map(CellCountX, CellCountZ);
        map0.layout = map0Terrain;
        map1.layout = AddWorldBorder(map1Terrain, 2);
        map2.layout = mapLayout;
        mapList.Add(map0);
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

    private terrainType[,] AddWorldBorder(terrainType[,] originalMap, int borderSize)
    {
        /// Checks if bordersize is even, if not, make it even.
        /// This is to prevent the map layout to shift by 1 cell every other row.
        if ((int)Math.Ceiling((double)borderSize) % 2 != 0)
        {
            borderSize++;
        }
        // Create a new array that is 10x10 larger than the original
        terrainType[,] borderedMap = new terrainType[originalMap.GetLength(0) + borderSize * 2, originalMap.GetLength(1) + borderSize * 2];
        // Fill the new array with water
        for (int i = 0; i < borderedMap.GetLength(0); i++)
        {
            for (int j = 0; j < borderedMap.GetLength(1); j++)
            {
                if (i >= originalMap.GetLength(0) + borderSize)
                {
                    borderedMap[i, j] = terrainType.snow; // Add snow to the bottom
                }
                else
                {
                    borderedMap[i, j] = terrainType.water; // Add water to the rest
                }
            }
        }

        // Copy the original map into the middle of the new array
        for (int i = 0; i < originalMap.GetLength(0); i++)
        {
            for (int j = 0; j < originalMap.GetLength(1); j++)
            {
                borderedMap[i + borderSize, j + borderSize] = originalMap[i, j];
            }
        }
        return borderedMap;
    }
}



