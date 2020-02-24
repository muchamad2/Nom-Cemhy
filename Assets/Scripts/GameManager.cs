using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    [Header("End Game References")]
    [SerializeField]
    private GameObject panelReward;
    [SerializeField]
    //public GameObject gameReward;

    private TextMeshProUGUI rewardText;
    [SerializeField]
    private Sprite goldReward;
    [SerializeField]
    private Sprite silverReward;
    public Image rewardImg;

    [Space]
    [Header("Current Stage References")]
    public int minimBenar;
    [Space]
    [Header("Progression Game Reference")]
    [SerializeField] GameObject gameDone;
    [SerializeField] GameObject gameOver;
    [SerializeField] Image gameOverImg;
    [SerializeField] Sprite[] gameOverSprite;

    [Space]
    [Header("Audio References")]
    [SerializeField] AudioSource bgmSources;
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null || _instance != this)
            _instance = this;
        if (panelReward != null)
        {
            if (panelReward.activeInHierarchy)
                panelReward.SetActive(false);
            if(gameDone.activeInHierarchy)
                gameDone.SetActive(false);
            if(gameOver.activeInHierarchy)
                gameOver.SetActive(false);
            
        }
        bgmSources.volume = GameUtility.currentVolume;
    }

    public void showReward()
    {
        if (panelReward != null)
            panelReward.SetActive(true);

        if (GameUtility.totalBenar >= 26)
        {
            rewardText.text = "Good Job";
            rewardImg.sprite = goldReward;
        }
        else if (GameUtility.totalBenar >= 21)
        {
            rewardText.text = "Great";
            rewardImg.sprite = silverReward;
        }
    }
    public void showProgressionReward()
    {
        panelReward.SetActive(true);
        gameDone.SetActive(true);
        gameOver.SetActive(false);
        switch (GameUtility.lang)
        {
            case GameUtility.Language.Indo:
                rewardImg.sprite = goldReward;
                break;
            case GameUtility.Language.Eng:
                rewardImg.sprite = silverReward;
                break;
        }
    }
    public void showGameOverReward(){
        panelReward.SetActive(true);
        //gameReward.SetActive(false);
        gameDone.SetActive(false);
        gameOver.SetActive(true);
        switch (GameUtility.lang)
        {
            case GameUtility.Language.Indo:
                gameOverImg.sprite = gameOverSprite[0];
                break;
            case GameUtility.Language.Eng:
                gameOverImg.sprite = gameOverSprite[1];
                break;
        }
    }
    public void Home()
    {
        SceneTransaction.Instance.SceneLoad("Main Menu");
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel(int lvl){
        SceneTransaction.Instance.SceneLoad("Lvl" + lvl);
    }
}
