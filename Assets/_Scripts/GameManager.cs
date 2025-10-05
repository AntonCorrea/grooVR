using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Grid> grids;
    public XBotController xbot;

    public void StartGrids()
    {
        foreach(Grid grid in grids)
        {
            grid.StartGrid();
        }
    }

    [ContextMenu("OnBtnStart")]
    public void OnBtnStart()
    {
        StartGrids();
        xbot.SetActions(xBotActions.waitToMoveToGreet);
    }
}