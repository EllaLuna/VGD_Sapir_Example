using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverText.gameObject.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Game is paused");
        }
    }
}
