using System.Collections.Generic;
using UnityEngine;

public class CommandListManager
{
    private static Stack<ICommand> _undoStack = new Stack<ICommand>();

    public static void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _undoStack.Push(command);
    }

    public static void UndoCommand()
    {
        if(_undoStack.Count > 0)
        {
            ICommand command = _undoStack.Pop();
            command.Undo();
        }
    }

    public static void Clear()
    {
        _undoStack.Clear();
    }
}
