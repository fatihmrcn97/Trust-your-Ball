using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private HealthSystem health;

    public List<GameObject> hearts;

    private Color tempColor;
    private void Awake()
    {
        health = new HealthSystem(100);

        health.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelController.instance.OpenUI();
        LevelController.instance.isGameContinou = false;
        LevelController.instance.WriteHighScore();
    }

    public void Damage(int damageAm)
    {
        health.Damage(damageAm);
        heartDecrease();
        if (damageAm > 110)
            allHearFalse();
    }
    private void Start()
    {
        tempColor = hearts[0].GetComponent<Image>().color;
        tempColor.a = 0.2f;
    }


    private void heartDecrease()
    { 
        if(health.GetHealth()>=65)
            hearts[2].GetComponent<Image>().color = tempColor;
        if (health.GetHealth()>=30 && health.GetHealth() < 65)
            hearts[1].GetComponent<Image>().color = tempColor;
        if (health.GetHealth() < 30)
            hearts[0].GetComponent<Image>().color = tempColor;
    }

    private void allHearFalse()
    {
    
        hearts[0].GetComponent<Image>().color = tempColor;
        hearts[1].GetComponent<Image>().color = tempColor;
        hearts[2].GetComponent<Image>().color = tempColor;

    }
   
}
