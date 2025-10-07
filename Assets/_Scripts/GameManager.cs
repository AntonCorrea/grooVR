using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Grid> grids;
    public XBotController xbot;
    public HandMenu handMenu;
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

    public void ShowHandMenu()
    {
        handMenu.Show();
    }

    public void HideHandMenu()
    {
        handMenu.Hide();
    }
}