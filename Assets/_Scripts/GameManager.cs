using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Grid> grids;
    public XBotController xbot;
    public HandMenu handMenu;
    public Action actionDelay;

    public static GameManager Instance;
    void Awake()
    {
        // Check if another instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes
    }

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


    public IEnumerator InvokeWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionDelay.Invoke();
    }
}