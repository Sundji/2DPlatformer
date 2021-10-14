using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager selfGM;

    public static GameManager GM
    {
        get
        {

            if (selfGM == null)
                selfGM = FindObjectOfType<GameManager>();

            return selfGM;

        }
    }

    public Transform PlayerPrefab;

    public Transform ObstacleParent;

    [Header("Gameplay Canvas")]

    public Canvas GameplayCanvas;
    public Text TimerText;

    [Header("Menu Canvas")]

    public Canvas MenuCanvas;
    public Text BestTimeText;

    [Header("Camera Options")]

    //public Vector2 AspectRatio;

    private float timer;

    private bool inGame = true;

    public bool InGame
    {
        get
        {
            return inGame;
        }
    }

    private void Awake()
    {

        if (selfGM == null)
            selfGM = this;

        if (selfGM != this)
            Destroy(gameObject);

        //if (Camera.main)
            //Camera.main.aspect = AspectRatio.x / AspectRatio.y;

        OpenMenu();

    }

    private void Update()
    {
        if (inGame)
        {
            timer = timer + Time.deltaTime;
            if (TimerText)
                TimerText.text = timer.ToString("00000");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {

        inGame = false;

        if (GameplayCanvas)
            GameplayCanvas.enabled = false;

        if (MenuCanvas)
            MenuCanvas.enabled = true;

        if (SLSManager.SLSM)
        {

            if (Mathf.FloorToInt(SLSManager.SLSM.BestTime) < Mathf.FloorToInt(timer))
            {
                SLSManager.SLSM.SaveBestTime(timer);
            }

            if (BestTimeText)
                BestTimeText.text = SLSManager.SLSM.BestTime.ToString("00000");

        }

    }

    public void StartGame()
    {

        inGame = true;

        if (GameplayCanvas)
            GameplayCanvas.enabled = true;

        if (MenuCanvas)
            MenuCanvas.enabled = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
            Destroy(player);

        if (PlayerPrefab)
            Instantiate(PlayerPrefab);

        ClearLevel();

        timer = 0.0f;

    }

    private void ClearLevel()
    {
        if (ObstacleParent)
            foreach (Transform obstacle in ObstacleParent)
                Destroy(obstacle.gameObject);
    }

}
