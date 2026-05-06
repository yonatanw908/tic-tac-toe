using UnityEngine;

namespace TicTacToe
{
    public class DrawCommand : ICommand
    {
        public void Execute()
        {
            GameEvents.GameDrawn?.Invoke();
        }

        public void Undo()
        {
            GameEvents.UndoGameOver?.Invoke();
        }
    }
}
