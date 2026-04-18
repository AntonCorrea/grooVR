using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Animator animator;
    public float croosfadeTime;

    public TextMeshProUGUI subsText;
    public List<SpeakActionElement> speakList;
    public AudioSource audioSource;

    public float speed = 2f;
    public float rotationSpeed = 1f;
    public List<MoveActionElement> moveList;
    private Transform currentTarget;
    private bool isMoving = false;

    public void PlayAnimation(string animation)
    {
        animator.CrossFade(animation, croosfadeTime);
    }

    public void TriggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public void PlayVoice(AudioClip clip)
    {
        
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetSubsText(string text)
    {
        subsText.text = text;
    }

    public void MoveTo(Transform target)
    {
        currentTarget = target;
        isMoving = true;
        animator.SetBool("isWalking",true);
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (isMoving && currentTarget != null)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
                );
            }

            yield return null;
        }
    }

    public bool HasReachedTarget(float threshold = 0.1f)
    {
        if (currentTarget == null) return true;

        return Vector3.Distance(transform.position, currentTarget.position) <= threshold;
    }

    public void Stop()
    {
        animator.SetBool("isWalking", false);
        isMoving = false;
    }
}
