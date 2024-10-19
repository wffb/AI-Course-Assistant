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


    public Button myButton; // ��ť����
    //=================================================================================


    public TMP_Dropdown subjectDropdown; // ��һ����������ʾ��Ŀ
    public TMP_Dropdown assistantDropdown; // �ڶ�����������ʾ����
    public TextMeshProUGUI descriptionText; // ������ʾ������Text���
    //public int optionCount = 5; // ��̬ѡ�������

    string token = login.user_token;

    public List<Subject> subjectList = new List<Subject>(); // �洢���������ص�Subject�б�
    public List<Assistant> assistantList = new List<Assistant>(); // �洢���������ص�Assistant�б�

    private bool subjectDropdownOpened = false; // ���ڼ��Subject�������Ƿ��
    private bool assistantDropdownOpened = false; // ���ڼ��Assistant�������Ƿ��

    string old_text;  // to store old discription text    //�ѷ���

    public static string selected_assistant_id;
    public static List<string> fileList;



    void Start()
    {
        StartCoroutine(GetSubjectsFromServer());

        subjectDropdown.onValueChanged.AddListener(delegate { SubjectSelected(subjectDropdown); });

        assistantDropdown.onValueChanged.AddListener(delegate { AssistantSelected(assistantDropdown); });

        // �󶨰�ť�ĵ���¼�
        myButton.onClick.AddListener(aaaa_OnMyButtonClick);
        myButton.interactable = false;


        //GenerateDropdownOptions(optionCount);           //�ѷ���
        //dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
        // ��ʾ��ʼѡ�������
        //UpdateDescription(0);
    }

    void Update()
    {
        // ==================    ���Subject������Ĵ򿪺͹ر�״̬  �����򲻻��Զ����£���Ҫ�ֶ�д====================
        if (subjectDropdownOpened && !subjectDropdown.IsExpanded)
        {
            subjectDropdownOpened = false; // ����������ѹر�
            SubjectSelected(subjectDropdown); // �ֶ�����Subjectѡ�е��߼�
        }
        else if (!subjectDropdownOpened && subjectDropdown.IsExpanded)
        {
            subjectDropdownOpened = true; // ����������Ѵ�
        }

        // ���Assistant������Ĵ򿪺͹ر�״̬
        if (assistantDropdownOpened && !assistantDropdown.IsExpanded)
        {
            assistantDropdownOpened = false; // ����������ѹر�
            AssistantSelected(assistantDropdown); // �ֶ�����Assistantѡ�е��߼�
        }
        else if (!assistantDropdownOpened && assistantDropdown.IsExpanded)
        {
            assistantDropdownOpened = true; // ����������Ѵ�
        }
    }

    public void aaaa_OnMyButtonClick()
    {
        // ������ʵ����İ�ť����
        Debug.Log("=v= Entering 3D interface...��");

        // ������������ı�
        descriptionText.text = "=v= Entering 3D interface...";

        SceneManager.LoadScene("3D");

    }





    // �ӷ�������ȡSubject�б�
    IEnumerator GetSubjectsFromServer()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("localhost:8080/assistant/getSubjects"))
        {
            www.SetRequestHeader("Authorization", token);

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("web errors : " + www.error);
            }
            else
            {
                // �������������ص�JSON����
                string jsonResponse = www.downloadHandler.text;
                SubjectResponse response = JsonUtility.FromJson<SubjectResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("=v= Identity authentication successful......: " + response.message);

                    // ��ȡ����Subject�б�
                    subjectList = response.data;

                    // ��̬����Dropdownѡ��
                    GenerateDropdownOptions(subjectList);

                    // ����ѡ��ı仯
                    subjectDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(subjectDropdown); });

                    // ��ʾ��ʼѡ�������
                    if (subjectList.Count > 0)
                    {
                        UpdateDescription(0); // Ĭ����ʾ��һ��ѡ�������
                    }
                }
                else
                {
                    Debug.LogError("=v= failed......: " + response.message);
                }
            }
        }
    }

    // ����Subject�б�̬����Dropdownѡ��
    void GenerateDropdownOptions(List<Subject> subjects)
    {
        // �������ѡ��
        subjectDropdown.ClearOptions();

        // �����µ�ѡ���б���Subject��identifyId��name��ӵ�ѡ����
        List<string> options = new List<string>();
        foreach (var subject in subjects)
        {
            
            options.Add(subject.name + " - " + subject.identifyId);
        }

        // ���ѡ�Dropdown
        subjectDropdown.AddOptions(options);
    }

    // ��ѡ��ĳ��Dropdownѡ��ʱ����������
    void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        UpdateDescription(index);
    }

    // ���������ı���չʾ��ǰѡ�е�����
    void UpdateDescription(int index)
    {
        if (index < subjectList.Count)
        {
            descriptionText.text = "Subject Name: " + subjectList[index].name + "\nSubject description: " + subjectList[index].description;
        }
    }

    // ��ѡ��ĳ��Subjectѡ��ʱ�����������ȡ��Ӧ������
    void SubjectSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        string subjectId = subjectList[index].identifyId;

        // ��������
        UpdateDescription(index);

        // ����ѡ�еĿ�ĿID��ȡ����
        StartCoroutine(GetAssistantsFromServer(subjectId));
        
    }



    // ����ѡ�еĿ�Ŀ��ȡAssistant
    IEnumerator GetAssistantsFromServer(string subjectId)
    {
        Debug.Log("selected subject ID : " + subjectId);
        string url = "localhost:8080/admin/assistant/getAssistantBySubjectId?subjectIdentifyId=" + subjectId;
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", token);

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("�������: " + www.error);
            }
            else
            {
                // �������������ص�JSON����
                string jsonResponse = www.downloadHandler.text;
                AssistantResponse response = JsonUtility.FromJson<AssistantResponse>(jsonResponse);

                if (response.code == 200)
                {
                    Debug.Log("���ֻ�ȡ�ɹ�: " + response.message);

                    // ��ȡ����Assistant�б�
                    assistantList = response.data;

                    Debug.Log("assistantList  : " + assistantList);

                    // ��̬���ɵڶ���Dropdownѡ��
                    GenerateAssistantDropdownOptions(assistantList);
                }
                else
                {
                    Debug.LogError("��ȡʧ��: " + response.message);
                }
            }
        }
    }
    // ����Assistant�б�̬���ɵڶ���Dropdownѡ��
    void GenerateAssistantDropdownOptions(List<Assistant> assistants)
    {
        // �������ѡ��
        assistantDropdown.ClearOptions();

        // �����µ�ѡ���б���Assistant��id��description��ӵ�ѡ����
        List<string> options = new List<string>();
        foreach (var assistant in assistants)
        {
            // ��ʾ��������
            Debug.Log(" === assistant test list ==== ");
            Debug.Log("assistantList  : " + assistant.id + "  " + assistant.description);
            Debug.Log("assistantList  : " + assistant.name);
            options.Add(assistant.name);
        }

        // ���ѡ��ڶ���Dropdown
        assistantDropdown.AddOptions(options);
    }





    //=================
    void AssistantSelected(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        //selected_assistant_id = dropdown;//assistant.id    //discarded �ѷ���


        // ���ѡ�е������Ƿ��� assistantList ��
        if (index < assistantList.Count)
        {
            //string archive_text = descriptionText.text;
            // old_text = descriptionText.text;

            // ���� selected_assistant_id Ϊ��ѡ���ֵ� ID
            selected_assistant_id = assistantList[index].identifyId;

            Debug.Log(" === selected_assistant_id ==== " + selected_assistant_id);



            // ���� descriptionText����ʾ��ǰѡ�е����ֵ�����
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

            // �������󲢵ȴ���Ӧ
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Network error: " + www.error);
            }
            else
            {
                // �������������ص�JSON����
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
        public string description; // ����assistant�Ľ���
        public string identifyId;
    }

    [System.Serializable]
    public class AssistantResponse
    {
        public int code;
        public string message;
        public List<Assistant> data; // ����Assistant�б��data
    }

    [System.Serializable]
    public class FileResponse
    {
        public int code;
        public string message;
        public List<String> data; // ����File�б��data
    }







    //===========================



















}
