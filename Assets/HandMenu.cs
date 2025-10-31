using UnityEngine;

public class HandMenu : MonoBehaviour
{
    CubeMatrixController matrix;
    MenuController menuController;

    private void Start()
    {
        matrix = GetComponentInChildren<CubeMatrixController>();
        menuController = GetComponentInChildren<MenuController>();
    }
    public void Show()
    {
        matrix.StartEffect();
        menuController.Show();
    }

    public void Hide()
    {
        matrix.EndEffect();
        menuController.Hide();
    }
}
