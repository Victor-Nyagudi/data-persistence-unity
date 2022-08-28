using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    Button _startButton;

    [SerializeField]
    TextMeshProUGUI _playerName;

    void Start()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    void StartGame() 
    {
        GameManager.Instance.PlayerName = 
            GameObject.Find("Name Input Field").GetComponent<TMP_InputField>().text;

        SceneManager.LoadScene(1);
    }
}
