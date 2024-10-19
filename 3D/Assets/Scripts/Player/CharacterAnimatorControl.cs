using System.Collections;
using UnityEngine;

public class CharacterAnimatorControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the object.");
        }
    }

    public void StartTalking(AudioSource audioSource)
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
            animator.SetBool("isTalk", true);
            StartCoroutine(WaitAndStopTalking(audioSource.clip.length));
        }
    }

    private IEnumerator WaitAndStopTalking(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isTalk", false);
    }
}
