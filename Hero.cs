using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu]
public class Hero : ScriptableObject
{
    
    public string Name;
    public Sprite Icon;
    public int Strength;
    public float StrengthGain;
    public int Agillity;
    public float AgillityGain;
    public int Intelligence;
    public float IntelligenceGain;

    public int BaseDamage;
    public float Attacktime;
    public int Attackrange;

    public float Rüstung;
    public int Movespeed;
    public int BaseHP;
    public float BaseHPGain;
    public int BaseMana;
    public float BaseManaGain;

    public Spell[] spells;

    [TextArea(15, 20)]
    public string[] Talents = new string[8];
}
