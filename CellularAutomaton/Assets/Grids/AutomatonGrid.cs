using System.Collections.Generic;
using System.Linq;
using Assets.Grids.Cells;
using Assets.Infrastructure.Extensions;
using UnityEngine;

namespace Assets.Grids
{
    public class AutomatonGrid
    {
        public Vector3Int Size;
        public Cell[,,] Cells;

        public static AutomatonGrid Default(Vector3Int size)
        {
            var grid = new AutomatonGrid
            {
                Size = size,
                Cells = new Cell[size.x, size.y, size.z]
            };
            var flatCells = Vector3Int.zero.IterateTo(size)
                .Select(Cell.Default);

            foreach (var cell in flatCells)
            {
                grid.Cells[cell.Position.x, cell.Position.y, cell.Position.z] = cell;
            }

            foreach (var cell in grid.Cells)
            {
                grid.AssignNeighboringCells(cell, GridSettings.SurroundSelectionType.VonNeumann);
            }

            return grid;
        }

        public Cell GetCellSafe(Vector3Int position)
        {
            var adjusted = position.Wrap(Size);
            return Cells[adjusted.x, adjusted.y, adjusted.z];
        }

        public Cell GetCellUnsafe(Vector3Int position)
        {
            return Cells[position.x, position.y, position.z];
        }

        public void AssignNeighboringCells(Cell cell, GridSettings.SurroundSelectionType selectionTypeType)
        {
            cell.Neighbors = cell.Position
                .GetSurroundingVectors(selectionTypeType)
                .Select(GetCellSafe)
                .ToArray();
        }

        public void Update(AutomatonGrid from)
        {
            foreach (var cell in from.Cells)
            {
                var targetCell = GetCellUnsafe(cell.Position);
                targetCell.Magnitude = cell.Neighbors.Average(n => n.Magnitude);
            }
        }

        public void Copy(AutomatonGrid from)
        {
            foreach (var cell in from.Cells)
            {
                var targetCell = GetCellUnsafe(cell.Position);
                targetCell.Magnitude = cell.Magnitude;
                targetCell.Direction = cell.Direction;
            }
        }

        public double GetMinMagnitude()
        {
            return GetAllCells().Min(cell => cell.Magnitude);
        }

        private IEnumerable<Cell> GetAllCells()
        {
            foreach (var cell in Cells)
            {
                yield return cell;
            }
        }
    }
}