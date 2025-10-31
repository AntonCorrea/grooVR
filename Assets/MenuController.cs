using TMPro;
using UnityEngine;

//public enum OptionsBtn
//{
//    Vehiculos, Modelos_3D, Procedimientos, Ajustes, Camioneta_4x4
//}

public class MenuController : MonoBehaviour
{
    public GameObject menuRoot;
    public TextMeshProUGUI title;
    public bool hideAtStart = false;
    public MenuInstance[] menus;
    public CellController[] cells;

    public float menuDelay;


    private void Start()
    {   
        //cells = GetComponentsInChildren<CellController>();

        OpenMenu(0);

        if (hideAtStart)
        {
            Hide();
        }
    }

    private void SetActiveCellsButtons(int cellsActive, bool v)
    {
        for (int i = 0; i < cellsActive; i++)
        {
            cells[i].physicButton.enabled = v;
        }
    }

    private void SetActiveCells(int cellsActive, bool v)
    {
        for (int i = 0; i < cellsActive; i++)
        {
            cells[i].gameObject.SetActive(v);
        }
    }

    public void Show()
    {
        menuRoot.SetActive(true);
    }

    public void Hide()
    {
        menuRoot.SetActive(false);
    }

    public void OpenMenu(int option)
    {
        print("OPEN MENU "+option);

        title.text = menus[option].menuTitle;

        

        SetActiveCellsButtons(6, false);


        GameManager.Instance.actionDelay = () => OpenMenuDelay(option);
        _ = StartCoroutine(GameManager.Instance.InvokeWithDelay(menuDelay));
    }

    void OpenMenuDelay(int option)
    {
        SetActiveCells(6, false);

        int currentCellsLenght = menus[option].options.Length;

        for (int i = 0; i < currentCellsLenght; i++)
        {

            cells[i].textMesh.text = menus[option].options[i].ToString();

            int index = i;
            cells[i].buttonAction = () => menus[option].functions[index].Invoke();

            cells[i].physicButton.transform.localPosition = Vector3.zero;

            //if (menus[option].models.Length > i)
            //{
            //    cells[i].SetModel(menus[option].models[i]);
            //}
            //else
            //{
            //    cells[i].SetModel(null);
            //}
        }

        if (menus[option].indexReturnToMenu != -1)
        {
            int i = currentCellsLenght;

            cells[i].textMesh.text = "Volver";

            int index = menus[option].indexReturnToMenu;
            cells[i].buttonAction = () => OpenMenu(index);

            currentCellsLenght += 1;
        }

        SetActiveCells(currentCellsLenght, true);
        SetActiveCellsButtons(currentCellsLenght, true);

        //GameManager.Instance.actionDelay = () => SetActiveCells(currentCellsLenght, true);
        //_ = StartCoroutine(GameManager.Instance.InvokeWithDelay(1f));
    }

        

    

}
