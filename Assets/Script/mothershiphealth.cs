using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mothershiphealth : MonoBehaviour
{
    Attributes agentattributes;
    public Canvas healthdisplay;
    private int currenthealth;

    private int currentmagicpoint;


    public int damageblock { get; set; }

    public int attackdamage { get; set; }
    public int attackdamagebonus { get; set; }

    public int critdamage { get; set; }
    public int critchance { get; set; }

    public int magicdamage { get; set; }


    public int attackrange { get; set; }
    public int attackspeed { get; set; }

    public int movespeed { set; get; }
    private void Start()
    {
        agentattributes = transform.GetComponent<Attributes>();
        agentattributes.movespeed = Mathf.FloorToInt(GameSetting.agent_info[1]["Speed"]);
        agentattributes.attackdamage = Mathf.FloorToInt(GameSetting.agent_info[1]["Damage"]);
        agentattributes.healthpoint = Mathf.FloorToInt(GameSetting.agent_info[1]["Health"]);
        currenthealth = agentattributes.healthpoint;
    }
    private void FixedUpdate()
    {
        float value = (float)getcurrenthealth() / agentattributes.healthpoint;
        Slider[] y = healthdisplay.GetComponentsInChildren<Slider>();
        y[0].value = value;
    }
    public void takedamage(int dam)
    {
        print("agenttakedamage: " + dam);
        currenthealth -= dam;
        if (currenthealth < 0)
        {
            currenthealth = 0;
        }
    }
    public int getcurrenthealth()
    {
        return currenthealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        takedamage(10);
    }
}