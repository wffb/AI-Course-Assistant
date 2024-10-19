//by haoran wang
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class login : MonoBehaviour
{

    public TMP_InputField usernameField;  // ���������û���
    public TMP_InputField passwordField;  // ������������

    public TMP_Text feedbackText;

    public static string loginUrl = "localhost:8080/login";  // ��¼API��URL��ַ

    public static login Instance { get; private set; }

    public static string user_token; //�ڵ�¼���õ�JWT token

    public static List<Subject> subjectList = new List<Subject>();  // ���ڴ洢���������ص�Subject�б�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ��֤�ڳ����л�ʱ��������
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // ����GET�����Э��














    public void aaaa_login_check()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        //testing
        //username = "werb";
        //password = "12345";

        StartCoroutine(LoginRequest(username, password));
    }

    IEnumerator LoginRequest(string username, string password)
    {
        // JSON
        string jsonData = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";

        // Encoding
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //Encoding

        //  UnityWebRequest   �ֶ�����Content-TypeΪapplication/json
        using (UnityWebRequest www = new UnityWebRequest(loginUrl, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(postData);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // ��������
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("�������: " + www.error);
                feedbackText.text = "WEB ERROR: " + www.error;
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                // 
                if (response.code == 200)
                {
                    Debug.Log("��¼�ɹ�, ��ӭ " + response.data.user.username);

                    user_token = response.data.token;

                    Debug.Log("user_token ID: " + user_token);

                    SceneManager.LoadScene("selection");  // 
                }
                else
                {
                    Debug.LogError("��¼ʧ��: " + response.message);
                    feedbackText.text = "LOGIN ERROR: " + "Incorrect Username or Password";
                }
            }
        }
    }

    [System.Serializable]
    public class Subject
    {
        // ɾ��id�ֶΣ���ΪJSON�в�û���ṩid�ֶ�
        public string name;
        public string identifyId; // Subject ʶ��ID
        public string description;  //���Ի�ȡ ��Ŀ   description
    }

    [System.Serializable]
    public class SubjectResponse
    {
        public int code;         // ��Ӧ��
        public string message;   // ��Ӧ��Ϣ
        public List<Subject> data; // ֱ��ʹ��List<Subject>��Ϊdata�ֶ�
    }

    //================================================================================

    // ��Ӧ��  ���ڽ������������ص�JSON����
    [System.Serializable]
    public class LoginResponse
    {
        public int code;  // 200��500
        public string message;  
        public LoginData data; 
    }

    [System.Serializable]
    public class LoginData
    {
        public string isTeacher;  
        public User user;  // �û���Ϣ
        public string token;    //token
    }

    [System.Serializable]
    public class User
    {
        public string username;  // �û���
        public bool isTeacher;  
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void aaaa_SwitchScene()
    {
        // ʹ��SceneManager���س���
        SceneManager.LoadScene("3D");
    }









}

