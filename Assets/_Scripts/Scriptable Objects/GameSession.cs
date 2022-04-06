using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Session", menuName = "Game Session")]
public class GameSession : ScriptableObject
{
    public int coinsInCurrentLevel;
    public int expInCurrentLevel;
    public int selectedHero;
}
