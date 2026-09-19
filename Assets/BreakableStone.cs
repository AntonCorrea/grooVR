using UnityEngine;

public class BreakableStone : MonoBehaviour
{
    [Header("No Fractured Pieces")]
    public GameObject noFracturedVersion;
    [Header("Fractured Pieces")]
    public GameObject fracturedVersion;

    [Header("Break Settings")]
    //public float breakForce = 5f;
    public string hammerTag = "pick";

    private bool broken = false;

    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (broken)
            return;

        if (collision.gameObject.CompareTag(hammerTag))
        {
            Break();
        }
    }

    [ContextMenu("Break")]
    private void Break()
    {
        broken = true;
        noFracturedVersion.gameObject.SetActive(false);
        GameObject newStones = Instantiate(fracturedVersion,transform.parent);
        newStones.transform.position = transform.position;
        newStones.transform.rotation = transform.rotation;
        //fracturedVersion.SetActive(true);

        //Rigidbody[] pieces = fracturedVersion.GetComponentsInChildren<Rigidbody>();

        //foreach (Rigidbody piece in pieces)
        //{
        //    piece.AddExplosionForce(
        //        breakForce,
        //        collision.contacts[0].point,
        //        2f,
        //        0.5f,
        //        ForceMode.Impulse
        //    );
        //}

        
    }
}