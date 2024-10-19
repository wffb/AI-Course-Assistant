using UnityEngine;

public class CharacterSwitchController : MonoBehaviour
{
    public GameObject idle;
    public GameObject[] speaking;

    private GameObject currentCharacter;

    void Start()
    {
        currentCharacter = Instantiate(idle, transform.position, transform.rotation);
    }

    public void StartSpeaking()
    {
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }
        int randomIndex = Random.Range(0, speaking.Length);
        currentCharacter = Instantiate(speaking[randomIndex], transform.position, transform.rotation);
    }

    public void StopSpeaking()
    {
        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }
        currentCharacter = Instantiate(idle, transform.position, transform.rotation);
    }
}

