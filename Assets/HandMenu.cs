using UnityEngine;

public class HandMenu : MonoBehaviour
{
    CubeMatrixController matrix;
    public MenuController menuController;

    bool isFirstTime = true;
    public bool isTutoFinished = false;
    private void Start()
    {
        matrix = GetComponentInChildren<CubeMatrixController>();
        menuController = GetComponentInChildren<MenuController>();
    }
    public void Show()
    {
        matrix.StartEffect();
        menuController.ShowHandMenu();
        if (isFirstTime)
        {
            isFirstTime = false;
            GameManager.Instance.HandMenuShowFirstTime();
        }
    }

    public void Hide()
    {
        matrix.EndEffect();
        menuController.HideHandMenu();
        if (isTutoFinished)
        {
            GameManager.Instance.CheerFinishMenuTuto();
            isTutoFinished = false;
        }
    }


}
