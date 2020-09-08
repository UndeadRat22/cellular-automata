using Assets.Grids;
using Assets.Infrastructure.Extensions;
using UnityEngine;

public class Automaton : MonoBehaviour
{
    public GameObject cellPrefab;
    public Vector3Int GridSize;
    [Range(1, 20)]
    public float TickSpeed;
    [Range(1, 1000)]
    public int MaxScale;
    [Range(0, 0.125f)]
    public float FillPercentage;

    public Color MinColor;
    public Color MaxColor;

    private GameObject[,,] _gridObjects;
    private AutomatonGrid _grid;
    private AutomatonGrid _previousGrid;

    public void Start()
    {
        _grid = AutomatonGrid.Default(GridSize);
        _previousGrid = AutomatonGrid.Default(GridSize);

        _gridObjects = new GameObject[GridSize.x, GridSize.y, GridSize.z];
        foreach (var pos in Vector3Int.zero.IterateTo(GridSize))
        {
            var gameObject = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
            gameObject.SetActive(false);
            _gridObjects[pos.x, pos.y, pos.z] = gameObject;
        }
        FillGrid();
    }

    private float _timeSinceUpdate;
    public void Update()
    {
        _timeSinceUpdate += Time.deltaTime;
        if (_timeSinceUpdate > TickSpeed)
        {
            _timeSinceUpdate -= TickSpeed;
            UpdateGrid();
            UpdateGameObjectGrid();
        }
    }

    //cells might overlap randomly (be 'filled' twice) but that's fine.
    private void FillGrid()
    {
        var totalCellCount = GridSize.x * GridSize.y * GridSize.z;
        var neededToFillCellsCount = (int)(totalCellCount * FillPercentage);
        while (neededToFillCellsCount > 0)
        {
            var randomPosition = new Vector3Int(
                Random.Range(0, GridSize.x),
                Random.Range(0, GridSize.y),
                Random.Range(0, GridSize.z));

            _grid.GetCellSafe(randomPosition).Magnitude = MaxScale - 1;

            neededToFillCellsCount--;
        }
    }

    private void UpdateGrid()
    {
        _previousGrid.Update(_grid);

        var temp = _grid;
        _grid = _previousGrid;
        _previousGrid = temp;
    }

    private void UpdateGameObjectGrid()
    {
        var (min, max) = _grid.GetEdgeMagnitudes();
        foreach (var gridCell in _grid.Cells)
        {
            var gridObject = _gridObjects[gridCell.Position.x, gridCell.Position.y, gridCell.Position.z];
            //TODO: better padding (this just "shows" or "hides" the object based on how close it's to the min magnitude)
            //now its the "coldest" 10% ar inactive regions
            gridObject.SetActive(gridCell.Magnitude - (MaxScale * 0.1) > min);

            gridObject.GetComponent<Renderer>().material.color = Color.LerpUnclamped(MinColor, MaxColor, (float)(gridCell.Magnitude / max));
            //gridObject.transform.localScale = Vector3.one * (float)gridCell.Magnitude / MaxScale;
        }
    }
}
