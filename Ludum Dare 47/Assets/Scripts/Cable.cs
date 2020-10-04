using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{

    public Vector2 pos0;
    public Vector2 pos1;

    public LineRenderer display;

    public void Setup (Vector2 start, Vector2 end)
    {
        pos0 = start;
        pos1 = end;

        display.SetPosition(0, pos0);
        display.SetPosition(1, pos1);
    }

    public void Show ()
    {
        display.enabled = true;
    }

    public void Hide ()
    {
        display.enabled = false;
    }

}
