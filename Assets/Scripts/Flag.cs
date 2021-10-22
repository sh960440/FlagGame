using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFlag", menuName = "Flag")]
public class Flag : ScriptableObject
{
    public Sprite flagSprite;
    public string flagName;
}