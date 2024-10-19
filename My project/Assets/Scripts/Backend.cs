using OpenAI_API;
using OpenAI_API.Audio;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using tools;
using audio;
using llm;
using my_assistant;
using System;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEditor;

public class Backend: MonoBehaviour {
    private static  AudioSource audioSource;

    private static string OPENAI_API_KEY = "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";
    private static OpenAIAPI _api = new OpenAIAPI(OPENAI_API_KEY);



    public static async void Init(AudioSource audio)
    {
        Audio.Init(OPENAI_API_KEY);
        // LLM.Init(OPENAI_API_KEY);
        await Assistant.Init(OPENAI_API_KEY);

        audioSource = audio;
    }



    public async static Task<AudioClip> Run(AudioClip audio){

        string text = await Audio.Stt(audio);

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