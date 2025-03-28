﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    #region peremenya

    public GameObject[] panel;

    public static bool gameover;
    public static float Base_HP = 2;
    public static bool playerDead;
    public static int score;
    public Transform[] enemySpawn = new Transform[3];
    public Transform enemySpanw1;
    public Transform enemySpanw2;
    public Transform enemySpanw3;
    public float enemySpanwTime = 10;
    public int maxEnemy = 5;
    public Transform playerSpawn;
    public GameObject player;
    public GameObject enemy;
    public Text scoreText;
    public Text tankText;
    #endregion

    void Start () {
        gameover = false;
        panel[0].SetActive(false);
        Base_HP = 2;
        enemySpawn = new Transform[3] { enemySpanw1, enemySpanw2, enemySpanw3 };
        maxEnemy = maxEnemy * 3;
        playerDead = false;
        score = 0;
        Instantiate(player, playerSpawn.position, Quaternion.identity);
        StartCoroutine(WaitEnemySpawn(enemySpanwTime));
	}

    IEnumerator WaitEnemySpawn(float t)
    {
        if (!gameover) {
            foreach (Transform obj in enemySpawn)
            {
                maxEnemy--;
                Instantiate(enemy, obj.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(t);
            if (maxEnemy > 0) StartCoroutine(WaitEnemySpawn(enemySpanwTime));
        }
    }

    private void OnGUI()
    {
        scoreText.text = score.ToString();
        tankText.text = "Tank:\n" + maxEnemy;
    }

    void Update () {
		if (playerDead)
        {
            gameover = true;
        }
        if (Base_HP <= 0)
        {
            gameover = true;
            
        }
        if (gameover)
        {
            panel[0].SetActive(true);
        }
	}
}
