using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static readonly GameManager _instance = new GameManager();

    public static GameManager Instance { get => _instance; }

    public string PlayerName { get; set; }

    void Awake() => DontDestroyOnLoad(gameObject);

    void Start()
    {
           
    }
}
