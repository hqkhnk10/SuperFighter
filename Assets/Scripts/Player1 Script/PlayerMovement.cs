using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myBody;
    private PlayerAnimation player_Anim;

    public float walk_Speed = 3f;
    public float z_Speed = 1.5f;

    private float rotation_Y = -90;
    private float rotation_Speed = 15f;
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWalk();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x = +1; 
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x = -1; 
        }
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0);
        transform.position += moveDir * walk_Speed * Time.deltaTime;

    } 
    void PlayerWalk() {
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, rotation_Y, 0f);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            player_Anim.Walk(true);
        }
        else
        {
            player_Anim.Walk(false);
        }
    }
}
