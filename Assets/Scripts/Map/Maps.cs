using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HexGrid;
using static HexGrid.terrainType;

public class CellData
{
    public terrainType terrainType;
    public int region;
};

public class Map
{
    public int CellCountX;
    public int CellCountZ;
    public CellData[,] layout;
    public Map(int cellCountX, int cellCountZ)
    {
        this.CellCountX = cellCountX;
        this.CellCountZ = cellCountZ;
        this.layout = new CellData[CellCountX, CellCountZ];
    }
}
public class Maps : MonoBehaviour
{
    public static Maps instance;
    public List<Map> mapList;

    private const int CellCountX = 8;
    private const int CellCountZ = 6;


    /// <summary>
    /// F: Forest
    /// P: Plains
    /// M: Mountain
    /// S: Snow
    /// D: Desert
    /// .: Water
    /// Z: River Z
    /// X: River X
    /// C: River XZ
    /// V: River ZX
    /// R: Arctic
    /// </summary>
    /// 

    readonly string map1TerrainsText =
        "....S.SSS.....S.............\n" +
        "FFFFS..SSS........S.S......." +
        "SSFFSS.SS.......SSSSSSS....." +
        "MFFFFFS.S.P..FFFFPPPPPFF...." +
        "FPPFFCF.....FFFFFFFPZZF....." +
        ".PFFFCF....P..FFFFFFPPFFFF.." +
        "PPFFFF......PPPPPFFFPPFFF.F." +
        ".MPPFF......PPPPPPDMMPPFF.F." +
        ".DDP........FMMFFDDFMPPFF..." +
        "..D........PPMF.DDDPPPFFFF.." +
        "..F.............DDDPPPFFFF.." +
        "...Z.......DDDDVDDD.PPFFFF.." +
        "..PPFF....DDDDDVDD..PP.F...." +
        "..FFFFF...PPDPPDV....P..F..." +
        "..FFFFFP.FFFFPPPD........F.." +
        "...FFFPP..FFFFVPD......FF..." +
        "...DFFP.....FMVP........F.FF" +
        "....DFP......FFP.P.........." +
        "....MP......FFF.P......DD..." +
        "....MP.......PP........DDDD." +
        "....M........P........DDDDP." +
        ".....P.................D.DP." +
        ".........................P.F" +
        "...........................M" +
        "............................" +
        "..........RRRRRRRR.........." +
        "..RRRRRRRRRRRRRRRRRRRRRR....";

    /// <summary>
    ///  E: Europe
    ///  N: North America
    ///  S: South America
    ///  C: Asia
    ///  O: Oceania
    ///  A: Africa & Middle East
    /// </summary>
    readonly string map1RegionsText =
        "....N.NNN.....E.............\n" +
        "NNNNN..NNN........E.C......." +
        "NNNNNN.NN.......EECCCCC....." +
        "NNNNNNN.N.E..EEEEECCCCCC...." +
        "NNNNNNN.....EEEEEECCCCC....." +
        ".NNNNNN....E..EEEECCCCCCCC.." +
        "NNNNNN......EEEEECCCCCCCC.C." +
        ".NNNNN......EEEEECCCCCCCC.C." +
        ".NNN........EEEEECCCCCCC...." +
        "..N........EEEE.AAACCCCCCC.." +
        "..S.............AAACCCCCCC.." +
        "...S.......AAAAAAAA.CCCCCC.." +
        "..SSSS....AAAAAAAA..CC.C...." +
        "..SSSSS...AAAAAAA....C..C..." +
        "..SSSSSS.AAAAAAAA........O.." +
        "...SSSSS..AAAAAAA......OO..." +
        "...SSSS.....AAAA........O.OO" +
        "....SSS......AAA.A.........." +
        "....SS......AAA.A......OO..." +
        "....SS.......AA........OOOO." +
        "....S........A........OOOOO." +
        ".....S.................O.OO." +
        ".........................O.O" +
        "...........................O" +
        "............................" +
        "............................" +
        "............................";


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
        if (instance == null)
        {
            instance = this;
        }


        int map1CellCountX = map1TerrainsText.IndexOf('\n');
        int map1CellCountZ = (map1TerrainsText.Length - 1) / map1CellCountX;

        mapList = new List<Map>();

        CellData[,] map1Data = ConvertTo2DArray(map1TerrainsText, map1RegionsText, map1CellCountX, map1CellCountZ);

        // Map 1 end
        //Map map0 = new Map(map0Width, map0Height);
        Map map1 = new Map(map1Data.GetLength(1), map1Data.GetLength(0));
        // Map map2 = new Map(CellCountX, CellCountZ);
        //map0.layout = map0Terrain;
        map1.layout = AddWorldBorder(map1Data, 5);
        map1.CellCountX = map1.layout.GetLength(0);
        map1.CellCountZ = map1.layout.GetLength(1);
        // map1.layout = map1Terrain;
        // map2.layout = mapLayout;
        //mapList.Add(map0);
        mapList.Add(map1);
        // mapList.Add(map2);
    }



    static CellData[,] ConvertTo2DArray(string mapText, string regionText, int width, int height)
    {
        CellData[,] map = new CellData[height, width];
        int charIndex = 0;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                /// Skip newlines and carriage returns
                while (mapText[charIndex] == '\n' || mapText[charIndex] == '\r')
                {
                    charIndex++;
                }
                char terrainChar = mapText[charIndex];
                // Debug.Log("terrainChar " + terrainChar + " @ " + charIndex);
                char regionChar = regionText[charIndex];
                map[i, j] = new CellData
                {
                    terrainType = GetTerrainType(terrainChar),
                    region = GetRegion(regionChar)
                };
                charIndex++;
            }
        }
        return map;
    }

    static terrainType GetTerrainType(char terrainChar)
    {
        switch (terrainChar)
        {
            case 'F':
                return terrainType.forest;
            case 'P':
                return terrainType.plains;
            case 'M':
                return terrainType.mountain;
            case 'S':
                return terrainType.snow;
            case 'D':
                return terrainType.desert;
            case '.':
                return terrainType.water;
            case 'Z':
                return terrainType.riverWE;
            case 'X':
                return terrainType.riverNS;
            case 'C':
                return terrainType.riverNE;
            case 'V':
                return terrainType.riverNW;
            case 'R':
                return terrainType.artic;
            default:
                throw new ArgumentException($"Invalid terrain character: {terrainChar}");
        }
    }

    private CellData[,] AddWorldBorder(CellData[,] originalMap, int borderSize)
    {
        /// Checks if bordersize is even, if not, make it even.
        /// This is to prevent the map layout to shift by 1 cell every other row.
        if ((int)Math.Ceiling((double)borderSize) % 2 != 0)
        {
            borderSize++;
        }
        // Create a new array that is 10x10 larger than the original
        CellData[,] borderedMap = new CellData[originalMap.GetLength(0) + borderSize * 2, originalMap.GetLength(1) + borderSize * 2];
        // Fill the new array with water
        for (int i = 0; i < borderedMap.GetLength(0); i++)
        {
            for (int j = 0; j < borderedMap.GetLength(1); j++)
            {
                if (i >= originalMap.GetLength(0) + borderSize)
                {
                    borderedMap[i, j] = new CellData();
                    borderedMap[i, j].terrainType = terrainType.snow; // Add snow to the bottom
                }
                else
                {
                    borderedMap[i, j] = new CellData();
                    borderedMap[i, j].terrainType = terrainType.water; // Add water to the rest
                }
            }
        }

        // Copy the original map into the middle of the new array
        for (int i = 0; i < originalMap.GetLength(0); i++)
        {
            for (int j = 0; j < originalMap.GetLength(1); j++)
            {
                // Debug.Log("i " + i + " j " + j);
                borderedMap[i + borderSize, j + borderSize] = originalMap[i, j];
            }
        }
        return borderedMap;
    }

    static int GetRegion(char regionChar)
    {
        switch (regionChar)
        {
            case 'E':
                return 1;
            case 'N':
                return 2;
            case 'S':
                return 3;
            case 'C':
                return 4;
            case 'O':
                return 5;
            case 'A':
                return 6;
            default:
                return 0;
        }
    }
}



