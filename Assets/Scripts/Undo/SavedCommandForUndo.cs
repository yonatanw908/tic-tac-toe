using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class SavedCommandForUndo : ICommand
    {
        private List<ICommand> commands = new List<ICommand>();

        public SavedCommandForUndo(List<ICommand> commands)
        {
            this.commands = commands;
        }

        public void Execute()
        {
            foreach (ICommand command in commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            for (int i = commands.Count - 1; i >= 0; i--)
            {
                commands[i].Undo();
            }
        }
    }
}
