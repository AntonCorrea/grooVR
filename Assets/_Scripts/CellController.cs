using Autohand;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CellController : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public GameObject visuals;
    public UnityEvent unityEvent;
    public PhysicsGadgetButton physicButton;

    public GameObject activeBtn;
    public GameObject pressedBtn;
    public GameObject deactiveBtn;

    public bool setDeactive = false;

    public bool isPressed = false;

    public bool toggled = false;

    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }

    public void SetPositionPhysicButton()
    {
        physicButton.transform.localPosition = Vector3.forward * 0.05f;
    }

    public void SetDisabledButton(bool v)
    {
        if (setDeactive)
        {
            if (v)
            {
                activeBtn.SetActive(false);
                deactiveBtn.SetActive(true);
                physicButton.enabled = false;
                physicButton.body.isKinematic = true;

            }
            else
            {
                activeBtn.SetActive(true);
                deactiveBtn.SetActive(false);
                physicButton.enabled = true;
                physicButton.body.isKinematic = false;
                setDeactive = false;
            }
        }
    }

    public void ToggleButton()
    {
        if(toggled == false)
        {
            toggled = true;
            activeBtn.gameObject.SetActive(false);
            pressedBtn.gameObject.SetActive(true);           
        }
        else
        {
            toggled = false;
            activeBtn.gameObject.SetActive(true);
            pressedBtn.gameObject.SetActive(false);
        }
    }

    public void ToggleButton(bool val)
    {
        toggled = val;
        activeBtn.gameObject.SetActive(!val);
        pressedBtn.gameObject.SetActive(val);
    }

    //metodos llamado por los metodos asignados dinamicamente en menucontroller
    public void OnBtnPress()
    {
        print("onbtn press " + gameObject.name);
        isPressed = true;
    }

    public void OnBtnUnpress()
    {
        print("onbtn unpress " + gameObject.name);
        if (isPressed)
        {
            unityEvent.Invoke();
            isPressed = false;
        }

    }

    [ContextMenu("OnInvokeAction")]
    public void OnInvoke()
    {
        unityEvent.Invoke();
    }
}
