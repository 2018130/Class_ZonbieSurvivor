using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Text ammoText;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text enemyWaveText;
    [SerializeField]
    private GameObject gameOverUI;

    public void SetAmmoText(string text)
    {
        ammoText.text = text;
    }
    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }
    public void SetEnemyWaveText(int wave, int remainEnemy)
    {
        enemyWaveText.text = $"Wave : {wave}\nEnemyLeft : {remainEnemy}";
    }

    public void ToggleGameOverUI()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
    }
}
