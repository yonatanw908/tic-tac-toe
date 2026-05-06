using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Cell[] _cells;

        private string _currentPlayer = "X";
        private bool _isGameOver;

        private static readonly int[][] WinningLines = new int[][]
        {
            new[] { 0, 1, 2 },
            new[] { 3, 4, 5 },
            new[] { 6, 7, 8 },
            new[] { 0, 3, 6 },
            new[] { 1, 4, 7 },
            new[] { 2, 5, 8 },
            new[] { 0, 4, 8 },
            new[] { 2, 4, 6 },
        };

        private void OnEnable()
        {
            GameEvents.CellClicked += OnCellClicked;
            GameEvents.UndoPlay += RevertPlayer;
            GameEvents.UndoGameOver += OnUndoGameOver;
            GameEvents.RestartGame += ResetBoard;
        }

        private void OnDisable()
        {
            GameEvents.CellClicked -= OnCellClicked;
            GameEvents.UndoPlay -= RevertPlayer;
            GameEvents.UndoGameOver -= OnUndoGameOver;
            GameEvents.RestartGame -= ResetBoard;
        }

        private void RevertPlayer(string previousPlayer)
        {
            _currentPlayer = previousPlayer;
        }

        private void OnUndoGameOver()
        {
            _isGameOver = false;
        }

        private void OnCellClicked(Cell cell)
        {
            if (_isGameOver)
            {
                GameEvents.InvalidMove?.Invoke();
                return;
            }

            if (!cell.IsEmpty())
            {
                GameEvents.InvalidMove?.Invoke();
                return;
            }

            string previousPlayer = _currentPlayer;

            cell.SetMark(_currentPlayer);
            string winner = CheckWinner();
            bool isDraw = IsBoardFull();
            cell.Clear();

            List<ICommand> turnCommands = new List<ICommand>();

            CellClickedCommand cellCommand = new CellClickedCommand(cell, _currentPlayer, previousPlayer);
            turnCommands.Add(cellCommand);

            if (winner != "")
            {
                _isGameOver = true;
                turnCommands.Add(new ScoreChangedCommand(winner));
            }
            else if (isDraw)
            {
                _isGameOver = true;
                turnCommands.Add(new DrawCommand());
            }

            CommandListManager.ExecuteCommand(new SavedCommandForUndo(turnCommands));
            GameEvents.MoveMade?.Invoke();

            if (!_isGameOver)
            {
                SwitchPlayer();
            }
        }

        private void SwitchPlayer()
        {
            _currentPlayer = _currentPlayer == "X" ? "O" : "X";
        }

        private void ResetBoard()
        {
            CommandListManager.Clear();
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].Clear();
            }
            _currentPlayer = "X";
            _isGameOver = false;
        }

        private string CheckWinner()
        {
            for (int i = 0; i < WinningLines.Length; i++)
            {
                int[] line = WinningLines[i];
                string a = _cells[line[0]].Mark;
                string b = _cells[line[1]].Mark;
                string c = _cells[line[2]].Mark;

                if (a != "" && a == b && b == c)
                {
                    return a;
                }
            }
            return "";
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
