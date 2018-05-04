using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    #region peremenya
    public int HP = 2;
    public float speed = 100f;
    public Animator _animator;
    public Transform tank;
    public Transform gun;
    public Rigidbody2D bullet;
    public float bulletspeed = 3f;
    private Rigidbody2D body;
    private Vector2 moveDirection;
    private Vector3 rotation;
    private bool hit;
    #endregion

    // Use this for initialization
    void Start () {
        _animator.speed = 0;
        body = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        body.AddForce(moveDirection * speed);

        if (Mathf.Abs(body.velocity.x) > speed/100f)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x)* speed/100f, body.velocity.y);
        }
        if (Mathf.Abs(body.velocity.y) > speed / 100f)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed/100f);
        }
    }

    void Update () {
        #region Move
        
        
            if (Input.GetKey(KeyCode.W) && !GameControl.gameover)
            {
                moveDirection = new Vector2(0, 1);
                rotation = new Vector3(0, 0, 0);
                _animator.speed = 1;
            }
            else if (Input.GetKey(KeyCode.S) && !GameControl.gameover)
            {
                moveDirection = new Vector2(0, -1);
                rotation = new Vector3(0, 0, 180);
                _animator.speed = 1;
            }
            else if (Input.GetKey(KeyCode.A) && !GameControl.gameover)
            {
                moveDirection = new Vector2(-1, 0);
                rotation = new Vector3(0, 0, 90);
                _animator.speed = 1;
            }
            else if (Input.GetKey(KeyCode.D) && !GameControl.gameover)
            {
                moveDirection = new Vector2(1, 0);
                rotation = new Vector3(0, 0, -90);
                _animator.speed = 1;
            }
        
        else
        {
            moveDirection = new Vector2(0, 0);
            _animator.speed = 0;
        }
#endregion

        if (Input.GetKeyDown(KeyCode.Space) && !GameControl.gameover)
        {
            Rigidbody2D bulletInstance = Instantiate(bullet, gun.position, Quaternion.identity) as Rigidbody2D;
            bulletInstance.velocity = gun.TransformDirection(Vector2.up * bulletspeed);
        }
        tank.localRotation = Quaternion.Euler(rotation);

        if (HP <= 0)
        {
            GameControl.playerDead = true;
            Destroy(gameObject);
        }

	}


}
