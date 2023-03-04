using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Image player_health_UI, enemy_health_UI;
    void Awake()
    {
       player_health_UI = GameObject.FindWithTag(Tags.PLAYER_HEALTH_UI).GetComponent<Image>();
       enemy_health_UI = GameObject.FindWithTag(Tags.ENEMY_HEALTH_UI).GetComponent<Image>();
        Debug.Log("run here 2");
    }
    public void DisplayHealth(bool player1, float value)
    {
            value /= 100f;
            if (value < 0f) 
                value = 0f;

        if (player1) { enemy_health_UI.fillAmount = value; }
        else { player_health_UI.fillAmount = value; }
    }
}