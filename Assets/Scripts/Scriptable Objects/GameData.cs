using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public int playerMoney = 0;

    // Upgrades System
    // Constants
    public int walkingSpeed = -1;
    public int carSpeed = -1;
    public int pickQuality = -1;
    public int chiselQuality = -1;
    public int walkSpeedUpgradeCost = -1;
    public int carSpeedUpgradeCost = -1;
    public int pickUpgradeCost = -1;
    public int chiselUpgradeCost = -1;
    public int maxWalkingSpeed = 4;
    public int maxCarSpeed = 10;
    public int maxPickLevel = 15;

    // Levels
    public int walkSpeedLevel = 1;
    public int carSpeedLevel = 1;
    public int pickLevel = 1;
    public int chiselLevel = 1;
}
