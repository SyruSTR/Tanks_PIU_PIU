using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    #region peremenya
    public int score = 50;
    public int HP = 1;
    public float speed = 100f;
    public Animator _animator;
    public Transform tank;
    public Transform gun;
    public Rigidbody2D bullet;
    public float bulletspeed = 3f;
    public float minFireTime = 1.5f;
    public float maxFireTime = 3.5f;
    public float minMoveTime = 0.5f;
    public float maxMoveTime = 3.5f;
    private Rigidbody2D body;
    private Vector2 moveDirection;
    private Vector3 rotation;
    private int move;
    private bool fire;
    #endregion
    // Use this for initialization
    void Start () {
        fire = false;
        move = 0;
        _animator.speed = 0;
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitMove(Random.Range(minMoveTime, maxMoveTime)));
        StartCoroutine(WaitFire(Random.Range(minFireTime, maxFireTime)));
	}

    IEnumerator WaitMove(float t)
    {
        if (!GameControl.gameover)
        {
            move = Random.Range(0, 4);
            yield return new WaitForSeconds(t);
            StartCoroutine(WaitMove(Random.Range(minMoveTime, maxMoveTime)));
        }
        else
            move = 0;
    }
    IEnumerator WaitFire(float t)
    {
        if (!GameControl.gameover)
        {
            fire = true;
            yield return new WaitForSeconds(t);
            StartCoroutine(WaitFire(Random.Range(minFireTime, maxFireTime)));
        }
    }

    void FixedUpdate()
    {
        body.AddForce(moveDirection * speed);

        if(Mathf.Abs(body.velocity.x) > speed / 100f)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x)* speed/100f, body.velocity.y);
        }
        if (Mathf.Abs(body.velocity.y) > speed / 100f)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Abs(body.velocity.y) * speed / 100f);
        }
    }

    void Update () {
		switch(move)
        {
            case 1:
                moveDirection = new Vector2(0, 1);
                rotation = new Vector3(0, 0, 90);
                _animator.speed = 1;
                break;

            case 2:
                moveDirection = new Vector2(0, -1);
                rotation = new Vector3(0, 0, -90);
                _animator.speed = 1;
                break;

            case 3:
                moveDirection = new Vector2(-1, 0);
                rotation = new Vector3(0, 0, 180);
                _animator.speed = 1;
                break;

            case 4:
                moveDirection = new Vector2(1, 0);
                rotation = new Vector3(0, 0, 0);
                _animator.speed = 1;
                break;

            default:
                moveDirection = new Vector2(0, 0);
                _animator.speed = 0;
                break;
        }
        if (fire)
        {           
            fire = false;
            Rigidbody2D bulletInstance = Instantiate(bullet, gun.position, Quaternion.identity) as Rigidbody2D;
            bulletInstance.velocity = gun.TransformDirection(Vector2.right * bulletspeed);
        }
        tank.localRotation = Quaternion.Euler(rotation);
        if(HP <= 0)
        {
            GameControl.score += score;            
            Destroy(gameObject);
        }
    }
}
