using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroInfo : ScriptableObject
{
    public string HeroName;
    public string description;

    public int health;
    public int attackValue;
    public int level;

    public Sprite artwork;
    public RuntimeAnimatorController animationController;
    public float imgWidth;
    public float imgHeight;
    public float imgPositionY;
  
}
