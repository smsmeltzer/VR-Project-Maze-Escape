using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const int START_TIME = 10;

    [SerializeField] UIManager myUI;
    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject enemyPowerUps;

    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Enemy.SetActive(false);
        //Player.SetActive(true);
        StartCoroutine(myUI.game_start(START_TIME));
        enemyPowerUps.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > START_TIME)
        {
            Enemy.SetActive (true);
            enemyPowerUps.SetActive(true);
            StartCoroutine(myUI.start_chase());
            enabled = false;
        }
    }
}
