using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameMechanic : ScriptableObject
{
    public string Name;
    [TextArea(15, 20)]
    public string Description;
}
