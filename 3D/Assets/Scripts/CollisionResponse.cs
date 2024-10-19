using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResponse : MonoBehaviour
{
    private Renderer objectRenderer; // Renderer to change the object's color
    private Color originalColor;     // Store the object's original color
    private AudioSource audioSource; // AudioSource to play sounds during collision

    void Start()
    {
        // Get the object's Renderer and AudioSource components
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        audioSource = GetComponent<AudioSource>();
    }

    // Called when a collision starts
    void OnCollisionEnter(Collision collision)
    {
        // If the object has a Renderer, change its color to red
        if (objectRenderer != null)
        {
            objectRenderer.material.color = Color.red; // Change color to red on collision
        }

        // If the object has an AudioSource, play the collision sound
        if (audioSource != null)
        {
            audioSource.Play();
        }

        Debug.Log("Collision detected with " + collision.gameObject.name); // Log collision for debugging
    }

    // Called when a collision ends
    void OnCollisionExit(Collision collision)
    {
        // Restore the object's original color
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }

        Debug.Log("Collision ended with " + collision.gameObject.name); // Log when the collision ends
    }
}
