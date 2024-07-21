using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameStates
{
    isPlaying,
    isPaused,
    GameOver,
    inMenu
}

public enum LevelList
{
    LevelOne,
    LevelTwo,
    LevelThree,
    BossLevel,
    None
}

public class GameManager : Singleton<GameManager>
{
    public GameStates currentGameStates;
    public int collectedGems;
    public List<string> unlockedLevels;

    void Start()
    {
       currentGameStates = GameStates.inMenu;
       unlockedLevels = new List<string>();
       AddUnlockLevel(LevelList.LevelOne);
    }

    public void AddUnlockLevel(LevelList levelList)
    {
        unlockedLevels.Add(levelList.ToString());
    }

    public void CollectedGemsCounter(int val)
    {
        collectedGems = val;
    }
}
