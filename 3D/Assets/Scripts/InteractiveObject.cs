using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    private Rigidbody rb; // Reference to the Rigidbody component

    // References to tooltips and audio components
    public TextMeshProUGUI itemNameText; // Associated tooltip UI objects
    private AudioSource audioSource;

    void Start()
    {
        // Get the Rigidbody and AudioSource components
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Ensure Rigidbody is not kinematic to allow gravity
        if (rb != null)
        {
            rb.isKinematic = true; // Make kinematic when not dragging
        }
    }

    void OnMouseDown()
    {
        // Playback of clicked audio feedback
        if (audioSource != null)
        {
            audioSource.Play();
        }

        string formattedName = ExtractObjectName(this.name);
        itemNameText.text = "[The user is holding a " + formattedName + "]";

        // Start dragging
        isDragging = true;

        // Disable Rigidbody physics while dragging
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Get the position of the object on the screen
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // Calculate the offset of a mouse click from the center of an object
        offset = gameObject.transform.position -
                 Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        // Drag the object
        if (isDragging)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = cursorPosition;
        }
    }

    void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;

        // Enable Rigidbody physics after releasing the object
        if (rb != null)
        {
            rb.isKinematic = false; // Enable gravity and physics again
        }

        itemNameText.text = "";
    }

    string ExtractObjectName(string fullName)
    {
        Match match = Regex.Match(fullName, @"^([a-zA-Z_]+)(?:[\s\._-]*(?:\(\d+\)|\d+))?$");
        if (match.Success)
        {
            return match.Groups[1].Value.Replace('_', ' ').ToLower();
        }
        return fullName.ToLower();
    }
}
