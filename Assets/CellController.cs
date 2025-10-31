using Autohand;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CellController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public Transform modelPlace;
    public GameObject modelPrefab;
    public GameObject model;
    public GameObject defaultModel;
    public UnityAction buttonAction;
    public PhysicsGadgetButton physicButton;

    public bool isPressed = false;
    public void SetModel(GameObject m)
    {
        if (m)
        {
            model = Instantiate(modelPrefab, modelPlace);
        }
        else
        {
            model = Instantiate(defaultModel, modelPlace);
        }
    }

   
    public void OnBtnPress()
    {
        print("onbtn press "+gameObject.name);
        isPressed = true;
    }

    public void OnBtnUnpress()
    {
        print("onbtn unpress " + gameObject.name);
        if (isPressed)
        {
            buttonAction.Invoke();
            isPressed = false;
        }
        
    }

    [ContextMenu("OnInvokeAction")]

    public void OnInvoke()
    {
        buttonAction.Invoke();
    }
}
