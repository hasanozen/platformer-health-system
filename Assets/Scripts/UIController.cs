using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite heartFull, heartHalf, heartEmpty;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= currentHealth)
                hearts[i].sprite = heartEmpty;
            else
                hearts[i].sprite = heartFull;
        }
    }
}
