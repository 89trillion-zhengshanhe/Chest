using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject startButton;
    
    public void ClickStartGame()
    {
        shopPanel.SetActive(true);
        startButton.SetActive(false);
    }
}
