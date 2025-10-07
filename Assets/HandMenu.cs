using UnityEngine;

public class HandMenu : MonoBehaviour
{
    CubeMatrixController matrix;
    MenuController menu;

    private void Start()
    {
        matrix = GetComponentInChildren<CubeMatrixController>();
        menu = GetComponentInChildren<MenuController>();
    }
    public void Show()
    {
        matrix.StartEffect();
        menu.Show();
    }

    public void Hide()
    {
        matrix.EndEffect();
        menu.Hide();
    }
}
