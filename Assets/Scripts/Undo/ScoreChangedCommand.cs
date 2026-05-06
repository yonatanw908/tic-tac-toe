using UnityEngine;

namespace TicTacToe
{
    public class ScoreChangedCommand : ICommand
    {
        private string _winner;

        public ScoreChangedCommand(string winner)
        {
            _winner = winner;
        }

        public void Execute()
        {
            GameEvents.GameWon?.Invoke(_winner);
        }

        public void Undo()
        {
            GameEvents.UndoScore?.Invoke(_winner);
            GameEvents.UndoGameOver?.Invoke();
        }
    }
}
