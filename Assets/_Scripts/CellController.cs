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

    public GameObject activeBtn;
    public GameObject deactiveBtn;

    public bool setDeactive = false;

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

    public void SetActive(bool v)
    {
        gameObject.SetActive(v);
    }

    public void SetEnabledPhysicButton(bool v)
    {


        physicButton.transform.localPosition = Vector3.forward * 0.05f;

        //physicButton.body.isKinematic = !v;
        //physicButton.body.linearVelocity = Vector3.zero;
        //physicButton.body.angularVelocity = Vector3.zero;

        physicButton.enabled = v;
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

            }
            else
            {
                activeBtn.SetActive(true);
                deactiveBtn.SetActive(false);
                physicButton.enabled = true;
                setDeactive = false;
            }
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
