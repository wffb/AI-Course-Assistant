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
            Debug.LogError("user_token Ϊ��");
        }
    }

    IEnumerator SendGetRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("localhost:8080/admin/assistant/getSubjects"))
        {
            // ���Authorizationͷ��Я��JWT token
            //www.SetRequestHeader("Authorization", login.user_token);
            www.SetRequestHeader("Authorization", login.user_token);
            Debug.Log("user_token ID: " + login.user_token);

            // �������ش�����
            //www.downloadHandler = new DownloadHandlerBuffer();

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                // ���������
                Debug.LogError("�������: " + www.error);
            }
            else
            {
                // �������������ص�JSON����
                string jsonResponse = www.downloadHandler.text;
                SubjectResponse response = JsonUtility.FromJson<SubjectResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("�����֤�ɹ�: " + response.message);

                    // ��Subject�б�洢��subjectList��
                    subjectList = response.data;

                    Debug.Log("subjectList  : " + subjectList);

                    // ���ÿ��Subject����Ϣ
                    foreach (login.Subject subject in subjectList)
                    {
                        Debug.Log("Subject Name: " + subject.name);
                        Debug.Log("Subject Identify ID: " + subject.identifyId);
                    }
                }
                else
                {
                    Debug.LogError("��ȡʧ��: " + response.message);
                }

            }
        }
    }



    //================================================================================
 























}
