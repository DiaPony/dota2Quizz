using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell 
{

    public string Name;
    public Sprite Icon;
    [TextArea(15, 20)]
    public string Description;
}
