using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    private EnemyAnimation animationScript;
    private PlayerAnimation playerMovement;
    private bool characterDied;
    private bool player1;
    private HealthUI health_UI;
    void Awake()
    {
        if (gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            player1 = true;
            animationScript = GetComponentInChildren<EnemyAnimation>();
        }
        else
        {
            player1 = false;
            playerMovement = GetComponentInChildren<PlayerAnimation>();
        }
        health_UI = GetComponent<HealthUI>();
    }
    public void ApplyDamage(float damage, bool knockDown, bool block)
    {
        if (characterDied)
            return;

        health -= damage;
        health_UI.DisplayHealth(player1,health);
        // display health UI
        if (health <= 0f)
        {
            if (player1) { animationScript.Death(); } else { playerMovement.Death(); }
            characterDied = true;

            // if is player deactivate enemy script
        }
        if (knockDown)
        {
            if (player1) { animationScript.KnockDown(); } else { playerMovement.KnockDown(); }
        }
        if (block)
        {
            return;
        }
        else {
            if (player1) { animationScript.Hit(); } else {
                playerMovement.Hit(); }
        }
    }
}
