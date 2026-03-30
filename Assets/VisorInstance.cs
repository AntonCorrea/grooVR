using UnityEngine;

public class VisorInstance : MonoBehaviour
{
    public string modelName;
    Rigidbody rb;
    public GameObject baseGuizmo;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void LockPositionX(bool val)
    {
        if (val)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
        }
        else
        {
            rb.constraints &=~ RigidbodyConstraints.FreezePositionX;
        }
    }

    public void LockPositionY(bool val)
    {
        if (val)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        }
    }

    public void LockPositionZ(bool val)
    {
        if (val)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    public void LockRotationX(bool val)
    {
        if (val)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationX;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        }
    }

    public void LockRotationY(bool val)
    {
        if (val)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
    }

    public void LockRotationZ(bool val)
    {
        if (val)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        }
    }


    public void SetGuizmo(bool val)
    {
        baseGuizmo.gameObject.SetActive(val);
    }
}
