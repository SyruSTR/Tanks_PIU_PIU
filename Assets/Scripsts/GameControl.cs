using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    #region peremenya
    public static bool playerDead;
    public static int score;
    public Transform[] enemySpawn;
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
        enemySpawn = new Transform[3] { enemySpanw1, enemySpanw2, enemySpanw3 };
        maxEnemy = maxEnemy * 3;
        playerDead = false;
        score = 0;
        Instantiate(player, playerSpawn.position, Quaternion.identity);
        StartCoroutine(WaitEnemySpawn(enemySpanwTime));
	}

    IEnumerator WaitEnemySpawn(float t)
    {
        foreach (Transform obj in enemySpawn)
        {
            maxEnemy--;
            Instantiate(enemy, obj.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(t);
        if (maxEnemy > 0) StartCoroutine(WaitEnemySpawn(enemySpanwTime));
    }

    private void OnGUI()
    {
        scoreText.text = score.ToString();
        tankText.text = "Tank:\n" + maxEnemy;
    }

    void Update () {
		if (playerDead)
        {
            playerDead = false;
            Instantiate(player, playerSpawn.position, Quaternion.identity);
        }
	}
}
