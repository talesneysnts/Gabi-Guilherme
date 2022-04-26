using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndexerManager : MonoBehaviour
{
    [SerializeField] SOPlayersIndexer playersIndexer;

    public GameObject[] players;

    //função variavel (delegate)
    public delegate void PlayerIndexer();
    public static PlayerIndexer playerIndexer;

    //singleton pattern
    static PlayerIndexerManager _instance;
    public static PlayerIndexerManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != this && _instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        players[playersIndexer.currentPlayerIndexer].SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //melhor controle depois... UI
        {
            playersIndexer.lastPlayerIndexer = playersIndexer.currentPlayerIndexer;
            playersIndexer.currentPlayerIndexer++;
            if (playersIndexer.currentPlayerIndexer >= players.Length)
            {
                playersIndexer.currentPlayerIndexer = 0;
            }
            playerIndexer?.Invoke(); //null ou !null

        }
    }

    void ChangePlayer()
    {
        players[playersIndexer.currentPlayerIndexer].SetActive(true);
        players[playersIndexer.lastPlayerIndexer].SetActive(false);
        players[playersIndexer.currentPlayerIndexer].transform.position = players[playersIndexer.lastPlayerIndexer].transform.position;
    }

    void OnEnable()
    {
        playerIndexer += ChangePlayer;
    }

    void OnDisable()
    {
        playerIndexer -= ChangePlayer;
    }
}
