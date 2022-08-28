using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    static readonly MainManager _instance = new MainManager();

    public static MainManager Instance { get => _instance; }


    public Brick _brickPrefab;
    public int _lineCount = 6;
    public Rigidbody _ball;

    public Text _scoreText;
    public GameObject _gameOverText, _hudText;
    
    bool m_Started = false;
    int m_Points;

    bool m_GameOver = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < _lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        _hudText.GetComponent<Text>().text =
            $"Name: {GameManager.Instance.PlayerName} - Best Score: 0";

        m_Points = MainManager.Instance.m_Points;
    }

    void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                _ball.transform.SetParent(null);
                _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MainManager.Instance.SaveScore();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        _scoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        _gameOverText.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

#else
        Application.Quit();
#endif
    }

    [Serializable]
    class SaveData
    {
        public int Score { get; set; }
    }

    public void SaveScore()
    {
        var data = new SaveData();

        data.Score = m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText($"{Application.persistentDataPath}/savefile.json", json);
    }

    public void LoadScore()
    {
        var path = $"{Application.persistentDataPath}/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_Points = data.Score;
        }
    }
}
