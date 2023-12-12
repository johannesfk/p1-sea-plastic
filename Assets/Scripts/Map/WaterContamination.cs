using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexGrid;

public class WaterContamination : MonoBehaviour
{
    public static WaterContamination Instance { get; private set; }
    // Start is called before the first frame update

    // private HexCell[] cells;
    private HexCell[] cells;
    private int[] contamitableWaterIndices;
    private int totalContamitableWater;
    private int contaminateIndex = 0;
    private int mostlyContaminated = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple WaterContamination instances");
        }
    }
    void Start()
    {
        StartCoroutine(WaitForCellsAndProcess());
    }
    private IEnumerator WaitForCellsAndProcess()
    {
        HexGrid hexGrid = FindObjectOfType<HexGrid>();
        if (hexGrid != null)
        {
            yield return StartCoroutine(hexGrid.WaitForCells());
            cells = hexGrid.cells;
            if (cells != null)
            {
                for
                (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i].terrainType == terrainType.water)
                    {
                        totalContamitableWater++;
                    }
                }

                contamitableWaterIndices = GetWater();



                // Debug.Log("Contamitable water indices: " + contamitableWaterIndices.Length);
                // Iterate over the array
                /* foreach (int index in contamitableWaterIndices)
                {
                    HexCell cell = cells[index];
                    Debug.Log("Contaminating " + index);
                } */
            }
            else
            {
                Debug.LogError("Cells not found");
            }
            // Process cells...
        }
        else
        {
            Debug.LogError("HexGrid not found");
        }
    }

    private int[] GetWater()
    {
        int[] waterIndices = GetFreeWater();
        System.Random rng = new System.Random();
        int n = waterIndices.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = waterIndices[k];
            waterIndices[k] = waterIndices[n];
            waterIndices[n] = value;
        }
        contaminateIndex = 0;
        return waterIndices;
    }

    private int[] GetFreeWater()
    {
        List<int> indices = new List<int>();
        Queue<(HexCell cell, int distance)> queue = new Queue<(HexCell cell, int distance)>();
        Dictionary<int, int> distances = new Dictionary<int, int>();

        // int bufferDistance = !mostlyContaminated ? 2 : 1;

        // Debug.Log("Buffer distance: " + bufferDistance);

        /// Enqueue all cells that are not water.
        /// Set their distance to 0.
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].terrainType != terrainType.water)
            {
                queue.Enqueue((cells[i], 0));
                distances[i] = 0;
            }
        }

        /// Breadth-first search
        /// https://en.wikipedia.org/wiki/Breadth-first_search
        /// https://www.redblobgames.com/pathfinding/a-star/introduction.html
        /// 
        /// While the queue is not empty, dequeue the first cell
        /// and enqueue all of its neighbors.
        while (queue.Count > 0)
        {
            (HexCell cell, int distance) = queue.Dequeue();

            foreach (HexCell neighbor in cell.neighbors)
            {
                if (neighbor != null && (neighbor.terrainType == terrainType.water || neighbor.terrainType == terrainType.contaminatedWater) && (!distances.ContainsKey(neighbor.index) || distances[neighbor.index] > distance + 1))
                {
                    distances[neighbor.index] = distance + 1;
                    queue.Enqueue((neighbor, distance + 1));
                }
            }
        }

        /// Add all cells with a distance greater than
        /// or equal to the buffer distance, to the indices list.
        foreach (KeyValuePair<int, int> entry in distances)
        {
            /* if (entry.Value >= bufferDistance)
            {
                indices.Add(entry.Key);
            } */
            switch (mostlyContaminated)
            {
                case 0:
                    if (entry.Value == 1)
                    {
                        indices.Add(entry.Key);
                    }
                    break;
                case 1:
                    if (entry.Value >= 1 && entry.Value < 2)
                    {
                        indices.Add(entry.Key);
                    }
                    break;
                case 2:
                    if (entry.Value >= 1)
                    {
                        indices.Add(entry.Key);
                    }
                    break;
                default:
                    break;
            }
        }

        return indices.ToArray();
    }

    public void Contaminate(float percentage)
    {
        int amount;

        amount = (int)(Math.Round((decimal)totalContamitableWater / 100) * (decimal)Math.Round(percentage));

        Debug.Log("Can Contaminate " + totalContamitableWater);
        Debug.Log("contaminateIndex" + contaminateIndex);


        /// If there are more cells to contaminate than the amount
        /// specified, contaminate the amount specified,
        /// else if there are more cells to contaminate than there are
        /// contamitable cells, contaminate all contamitable cells.
        if (contaminateIndex < contamitableWaterIndices.Length)
        {
            amount = Math.Min(amount, contamitableWaterIndices.Length - contaminateIndex);
            //Debug.Log("Contaminating " + amount + " water");

            for (int i = 0; i < amount; i++)
            {
                HexCell cell = cells[contamitableWaterIndices[contaminateIndex]];
                cell.terrainType = terrainType.contaminatedWater;
                cell.SetCellType(terrainType.contaminatedWater);
                contaminateIndex++;
            }
        }
        else
        {
            if (mostlyContaminated < 2)
            {
                Debug.Log("Stage " + mostlyContaminated + " contaminated");
                mostlyContaminated++;
                StartCoroutine(WaitForCellsAndProcess());
            }
            else
            {
                Debug.Log("All contamitable water contaminated");
            }
        }
    }
}
