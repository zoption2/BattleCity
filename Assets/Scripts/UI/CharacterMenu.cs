using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class CharacterMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject panel3;
    [SerializeField] private GameObject panel4;

    [SerializeField] private Text countdown;
    private AsyncOperation loadingOperation;


    private void Start()
    {
        PrepareForGame.DoNewGame();
        PrepareForGame.SetCharacterMenu(this);

        LevelConfig.Instance.CreateLevelStats(); //создаёт лист противников для всех уровней

        var amountOfPlayers = MasterController.totalPlayersInGame;
        panel1.SetActive(amountOfPlayers >= 1);
        panel2.SetActive(amountOfPlayers >= 2);
        panel3.SetActive(amountOfPlayers >= 3);
        panel4.SetActive(amountOfPlayers >= 4);
    }

    

    public void DoCountdown()
    {
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        switch (MasterController.difficult)
        {
            case Difficult.normal:
                loadingOperation = SceneManager.LoadSceneAsync("Constructor");
                break;
            case Difficult.hard:
                loadingOperation = SceneManager.LoadSceneAsync("Hardcore");
                break;
        }

        loadingOperation.allowSceneActivation = false;
        SoundManager.play.StopPlaySound("MainSong");
        countdown.text = "3";
        yield return new WaitForSeconds(1f);
        countdown.text = "2";
        yield return new WaitForSeconds(1f);
        countdown.text = "1";
        yield return new WaitForSeconds(1f);
        countdown.text = "0";
        loadingOperation.allowSceneActivation = true;
    }

}
