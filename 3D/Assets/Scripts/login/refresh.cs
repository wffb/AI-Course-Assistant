//by haoran wang
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static login;
using UnityEngine.Networking;
using static OpenAI.ObjectModels.Models;

public class refresh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void aaa_GetSubjectsFromServer()
    {
        if (!string.IsNullOrEmpty(login.user_token))
        {
            StartCoroutine(SendGetRequest());
        }
        else
        {
            Debug.LogError("user_token 为空");
        }
    }

    IEnumerator SendGetRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("localhost:8080/admin/assistant/getSubjects"))
        {
            // 添加Authorization头，携带JWT token
            //www.SetRequestHeader("Authorization", login.user_token);
            www.SetRequestHeader("Authorization", login.user_token);
            Debug.Log("user_token ID: " + login.user_token);

            // 设置下载处理器
            //www.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // 网络错误处理
                Debug.LogError("网络错误: " + www.error);
            }
            else
            {
                // 解析服务器返回的JSON数据
                string jsonResponse = www.downloadHandler.text;
                SubjectResponse response = JsonUtility.FromJson<SubjectResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("身份认证成功: " + response.message);

                    // 将Subject列表存储到subjectList中
                    subjectList = response.data;

                    Debug.Log("subjectList  : " + subjectList);

                    // 输出每个Subject的信息
                    foreach (login.Subject subject in subjectList)
                    {
                        Debug.Log("Subject Name: " + subject.name);
                        Debug.Log("Subject Identify ID: " + subject.identifyId);
                    }
                }
                else
                {
                    Debug.LogError("获取失败: " + response.message);
                }

            }
        }
    }



    //================================================================================
 























}
