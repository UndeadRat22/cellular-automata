using System;
using CellularAutomata;
using CellularAutomata.Grids;
using CellularAutomata.Grids.Cells;
using CellularAutomata.Iterators;
using UnityEngine;
using Random = UnityEngine.Random;

public class Automata : MonoBehaviour
{
    public GameObject cellPrefab;
    // Start is called before the first frame update
    [Range(3, 100)]
    public int X;
    [Range(3, 100)] 
    public int Y;
    [Range(3, 100)]
    public int Z;

    [Range(60, 240)] 
    public int Ticks;

    private GridAdvancer _gridAdvancer;
    private Random _random = new Random();
    private GameObject[,,] _objectGrid;
    void Start()
    {
        var grid = new AutomataGrid((X, Y, Z));
        _objectGrid = new GameObject[X, Y, Z];
        foreach (var (x, y, z) in IterationProvider.Iterate((0, 0, 0), (X , Y, Z)))
        {
            var cellObject = Instantiate(cellPrefab, new Vector3(x, y, z), Quaternion.identity);
            cellObject.transform.localScale = new Vector3(0, 0, 0);
            var cell = new Cell
            {
                Data = new CellData
                {
                    Magnitude = Random.Range(0, 100),
                },
                Position = (x, y, z)
            };
            grid.SetCellUnsafe(cell);
        }
        grid.AssignNeighbors();
        _gridAdvancer = new GridAdvancer(grid);
    }

    private int _currentTicks;
    void Update()
    {
        _currentTicks = (_currentTicks + 1) % Ticks;
        if (_currentTicks == 0)
        {
            _gridAdvancer.Advance();
            Draw();
        }
    }

    private void Draw()
    {
        foreach (var (x, y, z) in IterationProvider.Iterate((0, 0, 0), (X, Y, Z)))
        {
            var cell = _gridAdvancer.AutomataGrid.GetCellUnsafe(x, y, z);
            var scale = cell.Data.Magnitude / 100f;
            _objectGrid[x, y, z].transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
