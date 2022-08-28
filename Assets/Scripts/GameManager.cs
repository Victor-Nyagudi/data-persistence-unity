using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static readonly GameManager _instance = new GameManager();

    public GameManager Instance { get => _instance; }

    [SerializeField]
    TextMeshProUGUI _playerName;

    void Awake() => DontDestroyOnLoad(gameObject);

    void Start()
    {
           
    }

    void Update()
    {
        
    }
}
