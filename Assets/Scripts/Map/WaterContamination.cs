using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static HexGrid;

public class WaterContamination : MonoBehaviour
{
    public static WaterContamination Instance { get; private set; }

    private HexCell[] cells;


    [SerializeField]
    public List<HexCell> contaminatedCells = new List<HexCell>();
    [SerializeField]
    private List<HexCell> cleanCells = new List<HexCell>();
    private int totalContamitableWater;
    // private int mostlyContaminated = 0;

    private terrainType[] waterTypes = {
        terrainType.water,
        terrainType.contaminatedWater,
        terrainType.boatCleaner,
        terrainType.snow,
        terrainType.artic
    };
    private float previousPercentageContaminated = 0;

    [SerializeField]
    int addAmountCount = 0;

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

                GetFreeWater();
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

    public IEnumerator WaitForCells()
    {
        while (cells == null || cells.Length == 0)
        {
            yield return null;
        }
    }

    private void GetFreeWater()
    {
        List<int> indices = new List<int>();
        Queue<(HexCell cell, int distance)> queue = new Queue<(HexCell cell, int distance)>();
        Dictionary<int, int> distances = new Dictionary<int, int>();

        /// Enqueue all cells that are not water.
        /// Set their distance to 0.
        for (int i = 0; i < cells.Length; i++)
        {
            if (!waterTypes.Contains(cells[i].terrainType))
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
                if (neighbor != null && waterTypes.Contains(neighbor.terrainType) && (!distances.ContainsKey(neighbor.index) || distances[neighbor.index] > distance + 1))
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
            if (cells[entry.Key].terrainType == terrainType.water)
            {
                cells[entry.Key].coastDistance = entry.Value;
                cleanCells.Add(cells[entry.Key]);
                totalContamitableWater++;
            }
        }

        // return indices.ToArray();
    }

    public async Task Contaminate(float percentageContaminated)
    {
        while (cleanCells == null || cleanCells.Count == 0)
        {
            Debug.Log("Waiting for clean cells");
            await Task.Delay(100);
        }

        if (percentageContaminated == previousPercentageContaminated || percentageContaminated <= 0 || percentageContaminated >= 100)
        {
            if (percentageContaminated <= 0 && previousPercentageContaminated > 0)
            {
                // Decontaminate all cells
                foreach (HexCell cell in contaminatedCells)
                {
                    cell.SetCellType(terrainType.water);
                    cleanCells.Insert(0, cell);
                }
                contaminatedCells.Clear();
            }
            // The percentage hasn't changed, so return without doing anything
        }
        else
        {
            Debug.Log("Contaminating water to " + percentageContaminated + "%");

            cleanCells = cleanCells
                .GroupBy(cell => cell.coastDistance)
                .OrderBy(group => group.Key)
                .SelectMany(group => group.OrderBy(x => Guid.NewGuid()))
                .ToList();

            // Debug.Log("totalContamitableWater" + totalContamitableWater);

            // int amount = (int)Math.Abs(totalContamitableWater * (percentageContaminated - (contaminatedCells.Count / (float)totalContamitableWater)));
            // int amount = (int)Math.Abs(totalContamitableWater * (percentageContaminated - (contaminatedCells.Count / (float)totalContamitableWater * 100)) / 100);
            int amount = (int)Math.Abs(totalContamitableWater * (percentageContaminated - previousPercentageContaminated) / 100);
            // int amount = (int)(cleanCells.Count * Math.Abs(percentageContaminated - previousPercentageContaminated) / 100);
            // Debug.Log("Amount to add: " + amount);


            /// To deal with floating point errors, we calculate the difference
            /// between the previous percentage and the current percentage.
            /// We then add or subtract the difference from the amount.
            /// 
            /// For example, if the previous percentage was 55 and the current
            /// percentage is 52, the difference is 0.1. We then add 0.1 to the
            /// amount, so that the amount is 1.1. This way, we can contaminate
            /// 1.1 cells instead of 1 cell.
            // int difference = (int)(previousPercentageContaminated - (contaminatedCells.Count / (float)totalContamitableWater) * 100f);
            // amount += Math.Sign(difference) * difference;
            // amount -= difference;


            amount = (int)(amount / 2.5);



            // Debug.Log("Contaminated cells: " + contaminatedCells.Count);
            // Debug.Log("Clean cells: " + cleanCells.Count);
            // Debug.Log("percentageContaminated " + percentageContaminated + " previousPercentageContaminated " + previousPercentageContaminated + " Real: " + ((float)contaminatedCells.Count / cleanCells.Count * 100));


            if (percentageContaminated > previousPercentageContaminated)
            {
                Debug.Log("Contaminating " + amount + " water");
                // Contaminate cells
                for (int i = 0; i < amount && i < cleanCells.Count; i++)
                {
                    HexCell cell = cleanCells[i];
                    if (cell.terrainType != terrainType.boatCleaner)
                    {
                        cell.SetCellType(terrainType.contaminatedWater);
                        contaminatedCells.Add(cell);
                    }
                }

                // Remove the contaminated cells from the clean water list
                cleanCells.RemoveRange(0, Math.Min(amount, cleanCells.Count));
            }
            else
            {
                Debug.Log("Decontaminating " + amount + " water");
                // Decontaminate cells
                for (int i = 0; i < amount && i < contaminatedCells.Count; i++)
                {
                    HexCell cell = contaminatedCells[i];
                    cell.SetCellType(terrainType.water);
                    cleanCells.Insert(0, cell);
                }
                // Remove the decontaminated cells from the contaminated water list
                contaminatedCells.RemoveRange(0, Math.Min(amount, contaminatedCells.Count));
            }
            previousPercentageContaminated = percentageContaminated;
        }
    }

    public async Task ContaminateWater()
    {
        addAmountCount += 10;
        await Contaminate(addAmountCount);
    }
}
