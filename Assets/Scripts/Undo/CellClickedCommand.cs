using UnityEngine;

namespace TicTacToe
{
    public class CellClickedCommand : ICommand
    {
        private Cell _position;
        private string _mark;
        private string _previousPlayer;

        public CellClickedCommand(Cell position, string mark, string previousPlayer)
        {
            _position = position;
            _mark = mark;
            _previousPlayer = previousPlayer;
        }

        public void Execute()
        {
            _position.SetMark(_mark);
        }

        public void Undo()
        {
            _position.Clear();
            GameEvents.UndoPlay?.Invoke(_previousPlayer);
        }
    }
}