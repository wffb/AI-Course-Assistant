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

    public TMP_InputField usernameField;  // 用于输入用户名
    public TMP_InputField passwordField;  // 用于输入密码

    public TMP_Text feedbackText;

    public static string loginUrl = "localhost:8080/login";  // 登录API的URL地址

    public static login Instance { get; private set; }

    public static string user_token; //在登录后获得的JWT token

    public static List<Subject> subjectList = new List<Subject>();  // 用于存储服务器返回的Subject列表

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保证在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // 启动GET请求的协程














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

        //  UnityWebRequest   手动设置Content-Type为application/json
        using (UnityWebRequest www = new UnityWebRequest(loginUrl, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(postData);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // 发送请求
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("网络错误: " + www.error);
                feedbackText.text = "WEB ERROR: " + www.error;
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                // 
                if (response.code == 200)
                {
                    Debug.Log("登录成功, 欢迎 " + response.data.user.username);

                    user_token = response.data.token;

                    Debug.Log("user_token ID: " + user_token);

                    SceneManager.LoadScene("selection");  // 
                }
                else
                {
                    Debug.LogError("登录失败: " + response.message);
                    feedbackText.text = "LOGIN ERROR: " + "Incorrect Username or Password";
                }
            }
        }
    }

    [System.Serializable]
    public class Subject
    {
        // 删除id字段，因为JSON中并没有提供id字段
        public string name;
        public string identifyId; // Subject 识别ID
        public string description;  //尝试获取 科目   description
    }

    [System.Serializable]
    public class SubjectResponse
    {
        public int code;         // 响应码
        public string message;   // 响应消息
        public List<Subject> data; // 直接使用List<Subject>作为data字段
    }

    //================================================================================

    // 响应类  用于解析服务器返回的JSON数据
    [System.Serializable]
    public class LoginResponse
    {
        public int code;  // 200或500
        public string message;  
        public LoginData data; 
    }

    [System.Serializable]
    public class LoginData
    {
        public string isTeacher;  
        public User user;  // 用户信息
        public string token;    //token
    }

    [System.Serializable]
    public class User
    {
        public string username;  // 用户名
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
        // 使用SceneManager加载场景
        SceneManager.LoadScene("3D");
    }









}

