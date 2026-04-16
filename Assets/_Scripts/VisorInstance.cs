using UnityEngine;

public class VisorInstance : MonoBehaviour
{
    public string modelName;
    public Rigidbody rb;
    public GameObject baseGuizmo;
    public GameObject model;
    public bool lockpositionX, lockpositionY, lockpositionZ, lockRotationX, lockRotationY, lockRotationZ;
    private bool toggledActiveGuizmo = true;

    //public float size = 1f;

    public void LockPositionX()
    {
        if (lockpositionX)
        {
            rb.constraints &=~ RigidbodyConstraints.FreezePositionX;
            lockpositionX = false;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionX;
            lockpositionX = true;
        }
    }

    public void LockPositionY()
    {
        if (lockpositionY)
        {
            rb.constraints &=~ RigidbodyConstraints.FreezePositionY;
            lockpositionY = false;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
            lockpositionY = true;
        }
    }

    public void LockPositionZ()
    {
        if (lockpositionZ)
        {
            rb.constraints &=~ RigidbodyConstraints.FreezePositionZ;
            lockpositionZ = false;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
            lockpositionZ = true;
        }
    }

    public void LockRotationX()
    {
        if (lockRotationX)
        {
            rb.constraints &=~ RigidbodyConstraints.FreezeRotationX;
            lockRotationX = false;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationX;
            lockRotationX = true;
        }
    }

    public void LockRotationY()
    {
        if (lockRotationY)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            lockRotationY = false;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
            lockRotationY = true;
        }
    }

    public void LockRotationZ()
    {
        if (lockRotationZ)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            lockRotationZ = false;
        }
        else
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationZ;
            lockRotationZ = true;
        }
    }


    public void ToggleGuizmo()
    {
        if (toggledActiveGuizmo)
        {
            toggledActiveGuizmo = false;         
        }
        else
        {
            toggledActiveGuizmo = true;
        }
        baseGuizmo.gameObject.SetActive(toggledActiveGuizmo);
    }
}
