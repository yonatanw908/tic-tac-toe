using System;
using UnityEngine;

namespace TicTacToe
{
    public static class GameEvents
    {
        public static Action<Cell> CellClicked;
        public static Action MoveMade;
        public static Action InvalidMove;
        public static Action<string> GameWon;
        public static Action GameDrawn;
        public static Action<int, int> ScoreChanged;
        public static Action<string> ResultReady;
        public static Action<string> UndoPlay;
        public static Action<string> UndoScore;
        public static Action UndoGameOver;
        public static Action RestartGame;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlay()
        {
            CellClicked = null;
            MoveMade = null;
            InvalidMove = null;
            GameWon = null;
            GameDrawn = null;
            ScoreChanged = null;
            ResultReady = null;
            UndoPlay = null;
            UndoScore = null;
            UndoGameOver = null;
            RestartGame = null;
        }
    }
}
