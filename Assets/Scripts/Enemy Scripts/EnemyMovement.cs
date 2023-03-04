using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyAnimation enemyAnim;
    private Rigidbody myBody;

    public float speed =5f;
    private Transform playerTarget;
    public float attack_Distance =1f;
    private float chase_Player_After_Attack = 1f;
    private float current_Attack_Time;
    private float default_Attack_Time = 2f;
    private bool followPlayer, attackPlayer;
    private float rotation_Y = 90f;

    public bool is2Player = false;
    private void Awake()
    {
        enemyAnim = GetComponentInChildren<EnemyAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time; }
    // Update is called once per frame
    void Update() {
        if (is2Player)
        {
            PlayerWalk();
        }
        else
        {
            Attack();
        } 
    }
    void FixedUpdate() {
        if (is2Player)
        {
            Move();
        }
        else
        {
            FollowTarget();
        } }

    void PlayerWalk()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, rotation_Y, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }
    }
    private void Move()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = +1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = -1;
        }
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0);
        transform.position += moveDir * speed * Time.deltaTime;

    }
    void FollowTarget()
    {
        if (!followPlayer)
            return;
        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;
            if (myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
        {
            myBody.velocity = Vector3.zero;
            enemyAnim.Walk(false);
            followPlayer = false;
            attackPlayer = true;
        }// folLow target
    }
void Attack()
    {
        if (!attackPlayer)
            return;
        current_Attack_Time += Time.deltaTime;
        if (current_Attack_Time > default_Attack_Time)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3));
            current_Attack_Time = 0f;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) >
        attack_Distance + chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
        // attack
    }
}
