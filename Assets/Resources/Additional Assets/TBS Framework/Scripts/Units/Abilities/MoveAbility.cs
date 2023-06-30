using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TbsFramework.Cells;
using TbsFramework.Grid;
using TbsFramework.Grid.GridStates;

namespace TbsFramework.Units.Abilities
{
    public class MoveAbility : Ability
    {
        public Cell Destination { get; set; }
        private List<Cell> currentPath;
        public HashSet<Cell> availableDestinations;

        public override IEnumerator Act(CellGrid cellGrid)
        {
            if (CanPerform(cellGrid) && availableDestinations.Contains(Destination))
            {
                var path = UnitReference.FindPath(cellGrid.Cells, Destination);
                UnitReference.Move(Destination, path);
                while (UnitReference.IsMoving)
                {
                    yield return 0;
                }
            }
            yield return 0;
        }
        public override void Display(CellGrid cellGrid)
        {
            if (CanPerform(cellGrid))
            {
                foreach (var cell in availableDestinations)
                {
                    cell.MarkAsReachable();
                }
            }
        }

        public override void OnUnitClicked(Unit unit, CellGrid cellGrid)
        {
            if (cellGrid.GetCurrentPlayerUnits().Contains(unit))
            {
                cellGrid.CellGridState = new CellGridStateAbilitySelected(cellGrid, unit, unit.GetComponents<Ability>().ToList());
            }
        }
        public override void OnCellClicked(Cell cell, CellGrid cellGrid)
        {
            if (availableDestinations.Contains(cell))
            {
                Destination = cell;
                StartCoroutine(HumanExecute(cellGrid));
            }
            else
            {
                cellGrid.CellGridState = new CellGridStateWaitingForInput(cellGrid);
            }
        }
        public override void OnCellSelected(Cell cell, CellGrid cellGrid)
        {
            if (CanPerform(cellGrid) && availableDestinations.Contains(cell))
            {
                currentPath = UnitReference.FindPath(cellGrid.Cells, cell);
                currentPath.ForEach(u => u.MarkAsPath());
            }
        }
        public override void OnCellDeselected(Cell cell, CellGrid cellGrid)
        {
            if (CanPerform(cellGrid) && availableDestinations.Contains(cell))
            {
                currentPath?.ForEach(u =>
                {
                    u.MarkAsReachable();
                });
            }
        }

        public override void OnAbilitySelected(CellGrid cellGrid)
        {
            UnitReference.CachePaths(cellGrid.Cells);
            availableDestinations = UnitReference.GetAvailableDestinations(cellGrid.Cells);
        }

        public override void CleanUp(CellGrid cellGrid)
        {
            foreach (var cell in availableDestinations)
            {
                cell.UnMark();
            }
        }

        public override bool CanPerform(CellGrid cellGrid)
        {
            return UnitReference.ActionPoints > 0 && UnitReference.GetAvailableDestinations(cellGrid.Cells).Count > 0;
        }
    }
}
