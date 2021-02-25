using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { 
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;
    public static GameManager sharedInstance;

    private PlayerController controller;

    const string SUBMIT = "Submit";
    const string CANCEL = "Cancel";

    private void Awake()
    {
        if (sharedInstance == null) {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(SUBMIT) && currentGameState != GameState.inGame)
        {
            StartGame();
        }

        if (Input.GetButtonDown(CANCEL))
        {
            BackToMenu();
        }

    }

    public void StartGame() {
        SetGameState(GameState.inGame);
    }

    public void GameOver() {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu() {
        SetGameState(GameState.menu);
    }

    //Este será el único privado, porque solo el game manager puede cambiar el juego
    private void SetGameState(GameState newGameState) {
        if (newGameState == GameState.menu) {
            //TODO: colocar la lógica del menu
        } else if (newGameState == GameState.inGame) {
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            Invoke("ReloadLevel", 0.1f);

        }
        else if (newGameState == GameState.gameOver) {
            //TODO: colocar la lógica del GameOver
        }

        this.currentGameState = newGameState;
    }


    void ReloadLevel() {
        LevelManager.sharedInstance.GenerateInitialBlocks();
        controller.StartGame();
    }
}
