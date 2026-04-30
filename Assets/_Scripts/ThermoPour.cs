using UnityEngine;

public class ThermoPour : MonoBehaviour
{
    public ParticleSystem pourParticles; // Asignalo desde el inspector
    public Transform upReference; // Dirección del “arriba” global (puede ser la cámara o Vector3.up)
    public float pourAngleThreshold = 100f; // Ángulo a partir del cual se empieza a cebar

    private bool isPouring = false;

    void Update()
    {
        Vector3 termoUp = transform.up;
        Vector3 worldUp = upReference ? upReference.up : Vector3.up;

        float angle = Vector3.Angle(termoUp, worldUp);

        if (angle > pourAngleThreshold)
        {
            if (!isPouring)
            {
                pourParticles.Play();
                isPouring = true;
            }

            // Ajustar dirección de emisión si querés más realismo
            //var main = pourParticles.main;
            //main.startSpeed = Mathf.Lerp(0.5f, 2.5f, (angle - pourAngleThreshold) / 60f);
        }
        else
        {
            if (isPouring)
            {
                pourParticles.Stop();
                isPouring = false;
            }
        }
    }
}
