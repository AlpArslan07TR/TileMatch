using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCommandInvoker
{
    private readonly Stack<ITileCommand>  _commands = new ();

    public void AddCommand(ITileCommand command,SubmitBlock block)
    {
        command.Execute(block);
        _commands.Push(command);
    }

    public void RemoveCommand()
    {
        if (_commands.Count <= 0) return;
        var command = _commands.Pop();
        command.Undo();
    }

    public bool HasCommand()
    {
        return _commands.Count > 0;
    }
}
