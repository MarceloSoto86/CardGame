namespace TbsFramework.Grid.GridStates
{
    public class CellGridStateGameOver : CellGridState
    {
        public CellGridStateGameOver(CellGrid cellGrid) : base(cellGrid)
        {
        }

        public override CellGridState MakeTransition(CellGridState nextState)
        {
            return this;
        }
    }
}
