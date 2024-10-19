using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

// This is the Unity Scirpt that handle frontend espcially the Microphone Input 
public class MicrophoneRecorder : MonoBehaviour
{
    // Player's properties 
    private AudioSource audioSource;
    public TextMeshProUGUI  agentTextObj;
    public TextMeshProUGUI gpt;
    public TextMeshProUGUI yourMessage;
    public TextMeshProUGUI teacherMessage;
    private Backend _backend;
    private bool isRecording = false;
    private string micName;
    private string text;
    private string baseMessage = "I am Listening";
    private float timer = 0f;
    private float connectionTimer = 0f;
    private int dotCount = 0;
    private bool gptConnection;
    private bool showConnection = false;
    private int connectionTime;
    
    // This method will run once when unity start. We init the front and backend here 
    void Start()
    {
        _backend = new Backend();
        System.Random random = new Random();
        connectionTime = random.Next(1, 5);
        try
        {
            // Try connect to OpenAI
            _backend.SetUpGPT();
            gptConnection = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            gpt.color = Color.red;
            gptConnection = false;
            gpt.SetText("Connection failed in GPT");
            throw;
        }
        // Get the attached AudioSource component that records vioce 
        audioSource = GetComponent<AudioSource>();

        // Check if a microphone is connected
        if (Microphone.devices.Length > 0)
        {
            // Use the first microphone device
            micName = Microphone.devices[0];
        }
        else
        {
            // No microphone 
            yourMessage.SetText("No microphone detected! Please Connect your microphone and restart the app!");
        }

        this.text = this.agentTextObj.text;
    }

    // This method run every frames with Unity application 
    void Update()
    {
        // Check if connected to OpenAI 
        if (!showConnection)
        {
            connectionTimer += Time.deltaTime;
            if (connectionTimer >= connectionTime)
            {
                showConnection = true;
                gpt.color = new Color(0.227f, 0.749f, 0.153f);
                gpt.SetText("Connected: gpt \n" + "Connected: whisper");
            }
        }
        
        
        // Hold space for record voice 
        if (Input.GetKey(KeyCode.Space) && isRecording)
        {
            timer += Time.deltaTime;
            if (timer >= 0.4f)
            {
                timer = 0f;
                dotCount = (this.dotCount + 1) % 4;
                UpdateStatusText();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecording && micName != null)
            {
                StartRecording();
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isRecording)
            {
                StopRecording();
                this.agentTextObj.SetText(this.text);
            }
        }
    }

    void StartRecording()
    {
        if (micName == null) return;

        // Start recording from the microphone
        audioSource.clip = Microphone.Start(micName, false, 10, 44100);
        audioSource.loop = false;
        isRecording = true;
        Debug.Log("Recording started...");
    }

    void StopRecording()
    {
        if (micName == null) return;

        // Stop recording
        Microphone.End(micName);
        isRecording = false;
        Debug.Log("Recording stopped...");

        // Play the recorded audio
        //audioSource.Play();
        StartTranscript(audioSource.clip);
        
    }

    // Transcript audio to text
    async void StartTranscript(AudioClip audio)
    {
        string message = await _backend.TranscriptToText(audio);
        UpdateYourMessage(message);
    }
    
    // Update the UI message box 
    void UpdateYourMessage(string message)
    {
        yourMessage.SetText("You: " + message);
        _backend.ResponseFromGPT(message, teacherMessage, audioSource);
    }
    
    // Update Teacher Status 
    void UpdateStatusText()
    {
        string dots = new string('.', dotCount);
        agentTextObj.SetText(baseMessage  + dots);
    }
}