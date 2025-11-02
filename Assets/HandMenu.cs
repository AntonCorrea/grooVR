using UnityEngine;

public class HandMenu : MonoBehaviour
{
    CubeMatrixController matrix;
    MenuController menuController;

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
        menuController.Show();
        if (isFirstTime)
        {
            isFirstTime = false;
            GameManager.Instance.HandMenuShowFirstTime();
        }
    }

    public void Hide()
    {
        matrix.EndEffect();
        menuController.Hide();
        if (isTutoFinished)
        {
            GameManager.Instance.StartTeleportTuto();
        }
    }


}
