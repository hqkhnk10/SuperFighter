using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAnimation enemy_Anim;

    private bool activateTimerToReset;
    private float default_Combo_Timer = 0.4f;
    private float current_Combo_Timer;
    private ComboState current_Combo_State;

    private bool isBlock = false;
    void Awake()
    {
        enemy_Anim = GetComponentInChildren<EnemyAnimation>();
    }
    void Start()
    {
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
        enemy_Anim.Entry();
    }
    // Update is called once per frame
    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }
    void ComboAttacks()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if ((int)current_Combo_State >= 3)
            {
                return;
            }
            current_Combo_State++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (current_Combo_State == ComboState.PUNCH_1)
            {
                enemy_Anim.Punch_1();
            }
            if (current_Combo_State == ComboState.PUNCH_2)
            {
                enemy_Anim.Punch_2();
            }
            if (current_Combo_State == ComboState.PUNCH_3)
            {
                enemy_Anim.Punch_3();
            }
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            if ((int)current_Combo_State >= 5)
            {
                return;
            }
            if ((int)current_Combo_State == 0)
            {
                current_Combo_State = ComboState.KICK_1;
            }
            else
            {
                current_Combo_State++;
            }
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (current_Combo_State == ComboState.KICK_1)
            {
                enemy_Anim.Kick_1();
            }
            if (current_Combo_State == ComboState.KICK_2)
            {
                enemy_Anim.Kick_2();
            }
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            enemy_Anim.Block(true);
        }
        if (Input.GetKeyUp(KeyCode.PageDown))
        {
            enemy_Anim.Block(false);
        }
    }
    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0f)
            {
                current_Combo_State = ComboState.NONE;
                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }// reset comb0 state
        }
    }
}
