using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHistoryManager : MonoBehaviour
{

    private Stack<IAction> history = new Stack<IAction>();
    private Stack<IAction> redoHistory = new Stack<IAction>();

    public void ExecuteCommand (IAction action)
    {
        action.ExecuteCommand();
        history.Push(action);
        redoHistory.Clear();
    }
    
    public void UndoCommand ()
    {
        if (history.Count > 0)
        {
            redoHistory.Push(history.Peek());
            history.Pop().UndoCommand();
        }
    }

    public void RedoCommand()
    {
        if (redoHistory.Count > 0)
        {
            history.Push(redoHistory.Peek());
            redoHistory.Pop().ExecuteCommand();
        }
    }

}
