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
        SceneManager.LoadScene(1);


    }
}
