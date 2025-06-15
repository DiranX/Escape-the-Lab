using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        //set time to 0 at the start of the game
        Time.timeScale = 0f;

        StopCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        //quit when pressing Esc button
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }

        //Reload scene if player died
        if(player.health <= 0)
        {
            StartCoroutine(Delay());
        }
    }

    public void StartGame()
    {
        //set time to 0 when Play button is pressed
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        //quit game
        Application.Quit();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(0);

    }
}
