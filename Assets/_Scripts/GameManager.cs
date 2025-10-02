using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] menu_arr;
    public GameObject activeWristMenu;

    public GameObject[] grids;
    public GameObject gridStaticFloor;

    public Material[] skyboxs;
    void Start()
    {
        activeWristMenu.gameObject.SetActive(false);
        ShowMenu(0);
    }

    public void ShowMenu(int index)
    {
        foreach (GameObject menu in menu_arr)
        {
            StartCoroutine(DeactivateAfterFrames(menu, menu_arr, index, 1)); // Change 3 to desired frame delay
        }

    }

    private IEnumerator DeactivateAfterFrames(GameObject menu_c, GameObject[] menu_arr_c, int index, int frameDelay)
    {
        for (int i = 0; i < frameDelay; i++)
        {
            yield return null; // Wait one frame
        }

        menu_c.SetActive(false);
        menu_arr_c[index].SetActive(true);
    }

    public void ButtonStart()
    {
        ShowMenu(1);
        activeWristMenu.gameObject.SetActive(true);
        SetGrinds(true);
        AnimateGrids("GridAnimation_ShowLines");
    }

    public void SetSky(int index)
    {
        SetGrinds(false);
        RenderSettings.skybox = skyboxs[index];
    }

    public void SetGrinds(bool show)
    {
        if (show)
        {
            AnimateGrids("GridAnimation_ShowLines");
            gridStaticFloor.gameObject.SetActive(false);
        }
        else
        {
            AnimateGrids("GridAnimation_HideAll");
            gridStaticFloor.gameObject.SetActive(true);
        }
    }

    public void AnimateGrids(string animation)
    {
        foreach(GameObject grid in grids)
        {
            grid.GetComponent<Animation>().Play(animation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
