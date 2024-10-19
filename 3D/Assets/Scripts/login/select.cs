//by haoran wang
using my_assistant;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static login;
public class select : MonoBehaviour
{


    public Button myButton; // 按钮变量
    //=================================================================================


    public TMP_Dropdown subjectDropdown; // 第一个下拉框，显示科目
    public TMP_Dropdown assistantDropdown; // 第二个下拉框，显示助手
    public TextMeshProUGUI descriptionText; // 用于显示描述的Text组件
    //public int optionCount = 5; // 动态选项的数量

    string token = login.user_token;

    public List<Subject> subjectList = new List<Subject>(); // 存储服务器返回的Subject列表
    public List<Assistant> assistantList = new List<Assistant>(); // 存储服务器返回的Assistant列表

    private bool subjectDropdownOpened = false; // 用于检测Subject下拉框是否打开
    private bool assistantDropdownOpened = false; // 用于检测Assistant下拉框是否打开

    string old_text;  // to store old discription text    //已废弃

    public static string selected_assistant_id;
    public static List<string> fileList;



    void Start()
    {
        StartCoroutine(GetSubjectsFromServer());

        subjectDropdown.onValueChanged.AddListener(delegate { SubjectSelected(subjectDropdown); });

        assistantDropdown.onValueChanged.AddListener(delegate { AssistantSelected(assistantDropdown); });

        // 绑定按钮的点击事件
        myButton.onClick.AddListener(aaaa_OnMyButtonClick);
        myButton.interactable = false;


        //GenerateDropdownOptions(optionCount);           //已废弃
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        // 显示初始选项的描述
        //UpdateDescription(0);
    }

    void Update()
    {
        // ==================    检测Subject下拉框的打开和关闭状态  下拉框不会自动更新，需要手动写====================
        if (subjectDropdownOpened && !subjectDropdown.IsExpanded)
        {
            subjectDropdownOpened = false; // 标记下拉框已关闭
            SubjectSelected(subjectDropdown); // 手动触发Subject选中的逻辑
        }
        else if (!subjectDropdownOpened && subjectDropdown.IsExpanded)
        {
            subjectDropdownOpened = true; // 标记下拉框已打开
        }

        // 检测Assistant下拉框的打开和关闭状态
        if (assistantDropdownOpened && !assistantDropdown.IsExpanded)
        {
            assistantDropdownOpened = false; // 标记下拉框已关闭
            AssistantSelected(assistantDropdown); // 手动触发Assistant选中的逻辑
        }
        else if (!assistantDropdownOpened && assistantDropdown.IsExpanded)
        {
            assistantDropdownOpened = true; // 标记下拉框已打开
        }
    }

    public void aaaa_OnMyButtonClick()
    {
        // 在这里实现你的按钮功能
        Debug.Log("=v= Entering 3D interface...！");

        // 例如清空描述文本
        descriptionText.text = "=v= Entering 3D interface...";

        SceneManager.LoadScene("3D");

    }





    // 从服务器获取Subject列表
    IEnumerator GetSubjectsFromServer()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("localhost:8080/assistant/getSubjects"))
        {
            www.SetRequestHeader("Authorization", token);

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("web errors : " + www.error);
            }
            else
            {
                // 解析服务器返回的JSON数据
                string jsonResponse = www.downloadHandler.text;
                SubjectResponse response = JsonUtility.FromJson<SubjectResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("=v= Identity authentication successful......: " + response.message);

                    // 获取到的Subject列表
                    subjectList = response.data;

                    // 动态生成Dropdown选项
                    GenerateDropdownOptions(subjectList);

                    // 监听选项的变化
                    subjectDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(subjectDropdown); });

                    // 显示初始选项的描述
                    if (subjectList.Count > 0)
                    {
                        UpdateDescription(0); // 默认显示第一个选项的描述
                    }
                }
                else
                {
                    Debug.LogError("=v= failed......: " + response.message);
                }
            }
        }
    }

    // 根据Subject列表动态生成Dropdown选项
    void GenerateDropdownOptions(List<Subject> subjects)
    {
        // 清空现有选项
        subjectDropdown.ClearOptions();

        // 创建新的选项列表，将Subject的identifyId和name添加到选项中
        List<string> options = new List<string>();
        foreach (var subject in subjects)
        {
            
            options.Add(subject.name + " - " + subject.identifyId);
        }

        // 添加选项到Dropdown
        subjectDropdown.AddOptions(options);
    }

    // 当选中某个Dropdown选项时，更新描述
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        UpdateDescription(index);
    }

    // 更新描述文本，展示当前选中的描述
    void UpdateDescription(int index)
    {
        if (index < subjectList.Count)
        {
            descriptionText.text = "Subject Name: " + subjectList[index].name + "\nSubject description: " + subjectList[index].description;
        }
    }

    // 当选中某个Subject选项时，发送请求获取对应的助手
    void SubjectSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        string subjectId = subjectList[index].identifyId;

        // 更新描述
        UpdateDescription(index);

        // 根据选中的科目ID获取助手
        StartCoroutine(GetAssistantsFromServer(subjectId));
        
    }



    // 根据选中的科目获取Assistant
    IEnumerator GetAssistantsFromServer(string subjectId)
    {
        Debug.Log("selected subject ID : " + subjectId);
        string url = "localhost:8080/admin/assistant/getAssistantBySubjectId?subjectIdentifyId=" + subjectId;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", token);

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("网络错误: " + www.error);
            }
            else
            {
                // 解析服务器返回的JSON数据
                string jsonResponse = www.downloadHandler.text;
                AssistantResponse response = JsonUtility.FromJson<AssistantResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("助手获取成功: " + response.message);

                    // 获取到的Assistant列表
                    assistantList = response.data;

                    Debug.Log("assistantList  : " + assistantList);

                    // 动态生成第二个Dropdown选项
                    GenerateAssistantDropdownOptions(assistantList);
                }
                else
                {
                    Debug.LogError("获取失败: " + response.message);
                }
            }
        }
    }
    // 根据Assistant列表动态生成第二个Dropdown选项
    void GenerateAssistantDropdownOptions(List<Assistant> assistants)
    {
        // 清空现有选项
        assistantDropdown.ClearOptions();

        // 创建新的选项列表，将Assistant的id和description添加到选项中
        List<string> options = new List<string>();
        foreach (var assistant in assistants)
        {
            // 显示助手描述
            Debug.Log(" === assistant test list ==== ");
            Debug.Log("assistantList  : " + assistant.id + "  " + assistant.description);
            Debug.Log("assistantList  : " + assistant.name);
            options.Add(assistant.name);
        }

        // 添加选项到第二个Dropdown
        assistantDropdown.AddOptions(options);
    }





    //=================
    void AssistantSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        //selected_assistant_id = dropdown;//assistant.id    //discarded 已废弃


        // 检查选中的助手是否在 assistantList 中
        if (index < assistantList.Count)
        {
            //string archive_text = descriptionText.text;
            // old_text = descriptionText.text;

            // 更新 selected_assistant_id 为所选助手的 ID
            selected_assistant_id = assistantList[index].identifyId;

            Debug.Log(" === selected_assistant_id ==== " + selected_assistant_id);



            // 更新 descriptionText，显示当前选中的助手的描述
            descriptionText.text = "\n\nAssistant Description: " + assistantList[index].description;

            //old_text = archive_text;
            StartCoroutine(GetAssistantsFile(selected_assistant_id));
            myButton.interactable = true;
        }
    }

    IEnumerator GetAssistantsFile(string selected_assistant_id)
    {
        string url = "localhost:8080/getFileByAssitantId?assistantId=" + selected_assistant_id;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", token);

            // 发送请求并等待响应
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Network error: " + www.error);
            }
            else
            {
                // 解析服务器返回的JSON数据
                string jsonResponse = www.downloadHandler.text;
                FileResponse response = JsonUtility.FromJson<FileResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("File obtained successfully: " + response.message);
                    fileList = response.data;
                    Debug.Log("fileList  : " + fileList);
                }
                else
                {
                    Debug.LogError("Failed to get file: " + response.message);
                }
            }
        }
    }













    //====================================
    [System.Serializable]
    public class Assistant
    {
        public string id;
        public string name;
        public string description; // 对于assistant的介绍
        public string identifyId;
    }

    [System.Serializable]
    public class AssistantResponse
    {
        public int code;
        public string message;
        public List<Assistant> data; // 包含Assistant列表的data
    }

    [System.Serializable]
    public class FileResponse
    {
        public int code;
        public string message;
        public List<String> data; // 包含File列表的data
    }







    //===========================



















}
