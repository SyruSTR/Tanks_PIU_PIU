﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 1;
    public Transform exp;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Explosion();
        }
        //if (coll.transform.CompareTag("Block"))
        //{
        //    Destroy(coll.transform.gameObject);
        //    Destroy(gameObject);
        //}


        if (coll.transform.CompareTag("Enemy"))
        {            
            EnemyControl enemy = coll.transform.GetComponent<EnemyControl>();
            enemy.HP -= damage;
            Destroy(gameObject);
            Explosion();
        }
        if (coll.transform.CompareTag("Player"))
        {
            PlayerControler player = coll.transform.GetComponent<PlayerControler>();
            player.HP -= damage;
            Destroy(gameObject);
            Explosion();
        }
        if (coll.transform.CompareTag("Base"))
        {
            Destroy(gameObject);
            Explosion();
            GameControl.Base_HP -= damage;
        }
    }
    void Explosion()
    {
        Instantiate(exp, this.transform.position, Quaternion.identity);
    }
}
