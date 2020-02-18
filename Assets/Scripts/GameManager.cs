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
    public static GameManager Instance{
        get{
            return _instance;
        }
    }
    [SerializeField]
    private GameObject panelReward;
    [SerializeField]
    private TextMeshProUGUI rewardText;
    [SerializeField]
    private Sprite goldReward;
    [SerializeField]
    private Sprite silverReward;
    public Image rewardImg;
    public int minimBenar;
    // Start is called before the first frame update
    void Start()
    {
        if(_instance == null || _instance != this)
            _instance = this;
        if(panelReward != null){
            if(panelReward.activeInHierarchy)
                panelReward.SetActive(false);
        }
    }

    public void showReward(){
        if(panelReward != null)
            panelReward.SetActive(true);

        if(GameUtility.totalBenar >= 26){
            rewardText.text = "Good Job";
            rewardImg.sprite = goldReward;
        }else if(GameUtility.totalBenar >= 21){
            rewardText.text = "Great";
            rewardImg.sprite = silverReward;
        }
    }
    public void Home(){
        SceneTransaction.Instance.SceneLoad("Main Menu");
    }
    public void ReloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
