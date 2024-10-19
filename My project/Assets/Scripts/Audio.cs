using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.ResponseModels;
using System.Net;
using System.Xml;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Audio;
using static OpenAI_API.Audio.TextToSpeechRequest;
using System;
using tools;
using System.IO;

namespace audio{

        class Audio {


        // projectpath\bin\Debug\net8.0\audio\file.mp3'
        private static int num =0;



        private static OpenAI_API.OpenAIAPI? api;
        public static string? output;
        public static bool Successful = true;

        // public enum STATUS {
        //     Success,
        //     GetAswException,
        //     GetAswFailure,

        // }


        // public static STATUS status = STATUS.Success;

        public static void Init(string key){
            api = new OpenAIAPI(key);
        }

        public static async Task<string> Tts(string input){

            string tempFilePath = Path.Combine(Application.temporaryCachePath, "temp_output"+num.ToString()+".mp3");
            Debug.Log("临时地址"+tempFilePath);

            var request = new TextToSpeechRequest()
                {
	                Input = input,
	                ResponseFormat = ResponseFormats.MP3,
	                Model = Models.Tts_1_hd,
	                Voice = Voices.Nova,
	                Speed = 0.8
                };

            try{
                await api.TextToSpeech.SaveSpeechToFileAsync(request, tempFilePath);
            }
            catch(Exception e){
                Successful=false;
                 Debug.Log("存储失败");
                return "";
            }

                Successful=true;  

                num++;
                return  tempFilePath; 
            

        }

        public static async Task<string> Stt(AudioClip audio)
        {
            //加载到缓冲区
            var tempFilePath = Tools.Trans_clip_to_mp3(audio);

            string transcribedText = await api.Transcriptions.GetTextAsync(tempFilePath,"en");

            return transcribedText;   
        }


    }


}