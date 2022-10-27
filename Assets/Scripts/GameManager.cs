using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum SpeedLimitState
    {
        OK,
        AboveSpeedLimit
    }

    public SpeedLimitState speedLimitState;
    public int playerCarInstanceID;
    public int pedestriansOnScene;
    public float gameSpeed = 1;

    private void Awake() { if (Instance == null) { Instance = this; } }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
