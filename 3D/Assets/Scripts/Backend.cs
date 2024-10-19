using OpenAI_API;
using OpenAI_API.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using tools;
using audio;
using my_assistant;
using System;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEditor;

public class Backend: MonoBehaviour {
    private static  AudioSource audioSource;

    // enter your key here
    private static string OPENAI_API_KEY = "my key";
    
    private static OpenAIAPI _api = new OpenAIAPI(OPENAI_API_KEY);



    public static async void Init(AudioSource audio,string assistantId, List<string> fileIds)
    {
        Audio.Init(OPENAI_API_KEY);
        // LLM.Init(OPENAI_API_KEY);
        await Assistant.Init(OPENAI_API_KEY,assistantId,fileIds);

        audioSource = audio;
    }



    public async static Task<AudioClip> Run(AudioClip audio, string userHoldingItem)
    {

        string text = await Audio.Stt(audio);
        text = userHoldingItem + text;

        //取出结果
        await Assistant.Run(text);

        string asw;
        if(Assistant.isSuccess)
            asw = Assistant.reply;
        else
            asw = "Sorry, I didn't hear you clearly, can you say it again?";    

       //专门记录对话
        MyLog.writeLog(text,"Q:");
        MyLog.writeLog(asw,"A:");

        Debug.Log("Q:"+text);
        Debug.Log("A:"+asw);
        
        string path= await Audio.Tts(asw);
        Debug.Log(path);


        AudioClip clip = tools.Tools.LoadMp3(path);


        return clip;
    }

        public async static Task<AudioClip> RunWithTwxt(string text){


        //取出结果
        await Assistant.Run(text);

        string asw;
        if(Assistant.isSuccess)
            asw = Assistant.reply;
        else
            asw = "Sorry, I didn't hear you clearly, can you say it again?";    

       //专门记录对话
        MyLog.writeLog(text,"Q:");
        MyLog.writeLog(asw,"A:");
        Debug.Log(asw);
        
        string path= await Audio.Tts(asw);
        Debug.Log(path);


        AudioClip clip = tools.Tools.LoadMp3(path);


        return clip;
    }








}