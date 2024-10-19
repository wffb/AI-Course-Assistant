using JetBrains.Annotations;
using NLayer;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Collections.LowLevel.Unsafe.UnsafeStream;
using static Unity.Collections.NativeStream;


namespace tools{

    public  class Tools {
        // private static MpegFile mpegFile = null;
        // private static string _filePath = String.Empty;

        // private static string inFilePath = @"audio/output.mp3";
        // private static string outFilePath =@"audio/output.wav";



        // public static void Trans_mp3_to_wav(string mp3_loc){

        //     using(var reader = new Mp3FileReader(inFilePath))
        //     {
        //         WaveFileWriter.CreateWaveFile(outFilePath,reader);
        //     }
        // }

        //
        public static string Trans_clip_to_mp3(AudioClip audio){

            string tempFilePath = Path.Combine(Application.temporaryCachePath, "temp_audio.mp3");
            EncodeMP3.convert (audio, tempFilePath, 256);

            return tempFilePath;
        }

    //     public byte[] AudioClipToWav(AudioClip clip)
    //     {
    //         var samples = new float[clip.samples * clip.channels];
    //         clip.GetData(samples, 0);

    //         using (var memoryStream = new MemoryStream())
    //         {
    //         using (var writer = new BinaryWriter(memoryStream))
    //         {
    //             writer.Write(new char[4] { 'R', 'I', 'F', 'F' });
    //             writer.Write(36 + samples.Length * 2);
    //             writer.Write(new char[4] { 'W', 'A', 'V', 'E' });
    //             writer.Write(new char[4] { 'f', 'm', 't', ' ' });
    //             writer.Write(16);
    //             writer.Write((ushort)1);
    //             writer.Write((ushort)clip.channels);
    //             writer.Write(clip.frequency);
    //             writer.Write(clip.frequency * clip.channels * 2);
    //             writer.Write((ushort)(clip.channels * 2));
    //             writer.Write((ushort)16);
    //             writer.Write(new char[4] { 'd', 'a', 't', 'a' });
    //             writer.Write(samples.Length * 2);

    //             int rescaleFactor = 32767;
    //             for (int i = 0; i < samples.Length; i++)
    //             {
    //                 writer.Write((short)(samples[i] * rescaleFactor));
    //             }
    //             }
    //             return memoryStream.ToArray();
    //     }
    // }   

    private static MpegFile mpegFile = null;
    private static string _filePath = string.Empty;

    public static AudioClip LoadMp3(string filePath) {
        _filePath = filePath;

        mpegFile = new MpegFile(filePath);

        // assign mpegFile's info into AudioClip
        AudioClip ac = AudioClip.Create(System.IO.Path.GetFileNameWithoutExtension(filePath),
                                    (int)(mpegFile.Length / sizeof(float) / mpegFile.Channels),
                                    mpegFile.Channels,
                                    mpegFile.SampleRate,
                                    true,
                                    OnMp3Read,
                                    OnClipPositionSet);

        return ac;
    }

    // PCMReaderCallback will called each time AudioClip reads data.
    private static void OnMp3Read(float[] data) {
        int actualReadCount = mpegFile.ReadSamples(data, 0, data.Length);
    }

    // PCMSetPositionCallback will called when first loading this audioclip
    private static void OnClipPositionSet(int position) {
        mpegFile = new MpegFile(_filePath);
    }


}

    

    //logLoader
    public class MyLog{



        public static void writeLog(string msg,string head){
        
        //time
        var t_s = System.DateTime.Now.ToString("yyyyMMddhhmmss");
        var t_d = System.DateTime.Now.ToString("yyyyMMddhhmm");  

        var savePath = string.Format("{0}/chat_output_{1}.log", Application.persistentDataPath, t_d);
        //open file object---additional
        if (!File.Exists(savePath))
        {
            var fs = File.Create(savePath);
            fs.Close();
        }


        //add messsage
        using (var sw = File.AppendText(savePath))
        {
            sw.WriteLine("["+t_s+"] "+head+msg,true);
        }


    }

    }

}