using System;
using TbsFramework.Grid.GameResolvers;

public class GameEndedArgs : EventArgs
{
    public GameResult gameResult { get; set; }

    public GameEndedArgs(GameResult result)
    {
        gameResult = result;
    }
}
