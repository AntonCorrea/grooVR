using UnityEngine;

public class HandMenu : MonoBehaviour
{
    public CubeMatrixController matrix;
    public MenuController menuController;
    public FlexibleFollower flexibleFollower;

    public void Show()
    {
        matrix.StartEffect();
        menuController.ShowHandMenu();
    }

    public void Hide()
    {
        matrix.EndEffect();
        menuController.HideHandMenu();
    }


}
