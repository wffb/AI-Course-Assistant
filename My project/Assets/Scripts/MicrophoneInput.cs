using System;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

// This is the Unity Scirpt that handle frontend espcially the Microphone Input 
public class MicrophoneRecorder : MonoBehaviour
{
    // Player's properties 
    public AudioSource audioSource;
    //public TextMeshProUGUI  agentTextObj;
    //public TextMeshProUGUI gpt;
    public TextMeshProUGUI yourMessage;
    //public TextMeshProUGUI teacherMessage;
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

    // 使用StringBuilder来优化字符串的重复构造
    StringBuilder m_logStr = new StringBuilder();
    // 日志文件存储位置
    string m_logFileSavePath;
    
    // This method will run once when unity start. We init the front and backend here 
    void Start()
    {
        //log 
        var t = System.DateTime.Now.ToString("yyyyMMddhhmmss");
        m_logFileSavePath = string.Format("{0}/output_{1}.log", Application.persistentDataPath, t);
        Debug.Log(m_logFileSavePath);
        Application.logMessageReceived += OnLogCallBack;

        System.Random random = new Random();
        connectionTime = random.Next(1, 5);
        try
        {
            // Try connect to OpenAI
           // _backend.SetUpGPT();
            gptConnection = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //gpt.color = Color.red;
            gptConnection = false;
            //gpt.SetText("Connection failed in GPT");
            throw;
        }
        // Get the attached AudioSource component that records vioce 
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null) { 
            audioSource = gameObject.AddComponent<AudioSource>(); 
            Debug.Log("AudioSource component added automatically."); 
        }
        Backend.Init(audioSource);
        //Console.WriteLine(audioSource);


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

        //this.text = this.agentTextObj.text;
    }

    // This method run every frames with Unity application 

    void Update() 
    { 
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            if (!isRecording && micName != null) { 
                StartRecording(); 
            } 
        } 
        if (Input.GetKeyUp(KeyCode.Space)) 
        { 
            if (isRecording) 
            { 
                StopRecording(); 
            } 
        } 
    }

    //void Update()
    //{
    //    // Check if connected to OpenAI 
    //    if (!showConnection)
    //    {
    //        connectionTimer += Time.deltaTime;
    //        if (connectionTimer >= connectionTime)
    //        {
    //            showConnection = true;
    //            //gpt.color = new Color(0.227f, 0.749f, 0.153f);
    //            //gpt.SetText("Connected: gpt \n" + "Connected: whisper");
    //        }
    //    }


    //    // Hold space for record voice 
    //    if (Input.GetKey(KeyCode.Space) && isRecording)
    //    {
    //        timer += Time.deltaTime;
    //        if (timer >= 0.4f)
    //        {
    //            timer = 0f;
    //            dotCount = (this.dotCount + 1) % 4;
    //            UpdateStatusText();
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (!isRecording && micName != null)
    //        {
    //            StartRecording();
    //        }
    //    }

    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        if (isRecording)
    //        {
    //            StopRecording();
    //            //this.agentTextObj.SetText(this.text);
    //        }
    //    }
    //}


    void StartRecording() 
    { 
        if (isRecording || micName == null || audioSource == null) return; 
        try { 
            audioSource.clip = Microphone.Start(micName, false, 10, 44100); 
            audioSource.loop = false; 
            isRecording = true; 
            Debug.Log("Recording started..."); 
        } 
        catch (System.Exception e) { 
            Debug.LogError("Error starting recording: " + e.Message); 
        } 
    }


    void StopRecording()
    {
        if (!isRecording || micName == null) return;

        // Stop recording
        Microphone.End(micName);
        isRecording = false;
        Debug.Log("Recording stopped...");

        // Play the recorded audio
        //audioSource.Play();
         //_backend.Test3(audioSource.clip);
        StartTranscript(audioSource.clip);
        
    }

    // Transcript audio to text
    async void StartTranscript(AudioClip audio)
    {
        AudioClip clip= await Backend.Run(audio);

        //播放
        audioSource.pitch= 0.75F;
        audioSource.clip=clip;
        audioSource.Play();
        
    }
    
    // Update the UI message box 
    void UpdateYourMessage(string message)
    {
        //yourMessage.SetText("You: " + message);
        //_backend.ResponseFromGPT(message, teacherMessage, audioSource);
    }
    
    // Update Teacher Status 
    void UpdateStatusText()
    {
        string dots = new string('.', dotCount);
        //agentTextObj.SetText(baseMessage  + dots);
    }

       private void OnLogCallBack(string condition, string stackTrace, LogType type)
    {
        m_logStr.Append(condition);
        m_logStr.Append("\n");
        m_logStr.Append(stackTrace);
        m_logStr.Append("\n");

        if (m_logStr.Length <= 0) return;
        if (!File.Exists(m_logFileSavePath))
        {
            var fs = File.Create(m_logFileSavePath);
            fs.Close();
        }
        using (var sw = File.AppendText(m_logFileSavePath))
        {
            sw.WriteLine(m_logStr.ToString());
        }
        m_logStr.Remove(0, m_logStr.Length);
    }

}