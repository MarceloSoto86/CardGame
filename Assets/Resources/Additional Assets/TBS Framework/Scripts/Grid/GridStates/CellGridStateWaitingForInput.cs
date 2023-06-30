using System.Linq;
using TbsFramework.Units;
using TbsFramework.Units.Abilities;

namespace TbsFramework.Grid.GridStates
{
    class CellGridStateWaitingForInput : CellGridState
    {
        public CellGridStateWaitingForInput(CellGrid cellGrid) : base(cellGrid)
        {
        }

        public override void OnUnitClicked(Unit unit)
        {
            if (_cellGrid.GetCurrentPlayerUnits().Contains(unit))
            {
                _cellGrid.CellGridState = new CellGridStateAbilitySelected(_cellGrid, unit, unit.GetComponents<Ability>().ToList());
            }
        }
    }
}
