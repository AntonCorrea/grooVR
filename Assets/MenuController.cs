using UnityEngine;

public class MenuController : MonoBehaviour
{


    private void Start()
    {
        
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
