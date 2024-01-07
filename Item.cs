using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public int Cost;
    public bool isUpgraded;
    public Sprite[] ItemsNeededForUpgrade;
    public bool HasStats;
    [TextArea(15, 20)]
    public string Stats;
    public bool HasSpecialAbility;
    public SpecialAbility[] SpecialAbilities;
}
