using System.Collections.Generic;
using C17_Ex05.BasicDataTypes;
using C17_Ex05.Game.State;

namespace C17_Ex05.Game.Player.Algorithm
{
    class ReverseTicTacToeAlgo
    {
        private readonly GameLogic r_GameLogic;
        private readonly GameState r_GameState;
        private readonly uint r_OpponentIndex;
        private readonly uint r_OwnIndex;

        public ReverseTicTacToeAlgo(GameBoardCell.eType i_CellType, GameState i_GameState, GameLogic i_GameLogic)
        {
            GameBoardCell.eType opponentCellType = (i_CellType == GameBoardCell.eType.X) ?
                                                        GameBoardCell.eType.O : GameBoardCell.eType.X;
            r_GameLogic = i_GameLogic;
            r_GameState = i_GameState;
            r_OpponentIndex = i_GameLogic.CellTypeToPlayerIndex(opponentCellType);
            r_OwnIndex = i_GameLogic.CellTypeToPlayerIndex(i_CellType);
        }

        // calculates the score of a given cell (The higher the score - the better the chances of winning)
        private uint getPointScore(Point i_Pos)
        {
            uint pointScore = 0;

            // go over all lines that the point resides in
            foreach (BoardLineState line in r_GameState.GetStateLinesOfPoint(i_Pos))
            {
                if ((line.GetAmountOfCellsUsedByPlayer(r_OpponentIndex) > 0) &&
                    (line.GetAmountOfCellsUsedByPlayer(r_OwnIndex) > 0))
                {
                    continue;
                }

                pointScore++;                
            }

            return pointScore;
        }

        // Get best current move by calculating highest score position
        public Point GetMove()
        { 
            uint? highestScore = null;
            Point? bestPointPos = null;
            LinkedList<Point> freePositions = r_GameState.FreeBoardPoints;

            foreach (Point position in freePositions)
            {
                uint pointScore = getPointScore(position);

                if (!highestScore.HasValue)
                {
                    highestScore = pointScore;
                    bestPointPos = position;
                }
                else if (highestScore > pointScore)
                {
                    highestScore = pointScore;
                    bestPointPos = position;
                }
            }

            return bestPointPos.Value;
        }
    }
}
