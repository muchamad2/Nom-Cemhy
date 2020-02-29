using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] Slider slider;
    [Header("Panel References")]
    [SerializeField] public List<GameObject> panel = new List<GameObject>();
    [SerializeField] public List<GameObject> materiPanel = new List<GameObject>();
    public static int unlockedLvl = 1;
    [Header("Level Panel References")]
    public Image[] imgLvl;
    public GameObject[] lvlUnlock;
    public Sprite sprLockLvl;
    public Sprite sprUnlockLvl;

    private void Awake()
    {
        if (GameUtility.isPlaying)
            turnOffOtherPanel(4);
        else
            turnOffOtherPanel(7);
        turnOffAllMaterialPanel(); //only turn on main menu
        GameUtility.totalBenar = 0;
        if (GameUtility.currentVolume > 0)
            bgmSource.volume = GameUtility.currentVolume;
        else
            GameUtility.currentVolume = bgmSource.volume;
        slider.value = bgmSource.volume;
        for (int i = 0; i < imgLvl.Length; i++)
        {
            if (i > unlockedLvl-1){
                imgLvl[i].sprite = sprLockLvl;
                lvlUnlock[i].SetActive(false);
            }
            else{
                imgLvl[i].sprite = sprUnlockLvl;
                lvlUnlock[i].SetActive(true);
            }
                
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameUtility.isPlaying = false;
            Application.Quit();
        }
    }
    void turnOffOtherPanel(int indexOn)
    {
        foreach (var item in panel)
        {
            item.SetActive(false);
        }
        panel[indexOn].SetActive(true);
    }
    void turnOffOtherMateriPanel(int indexOn)
    {
        foreach (var item in materiPanel)
        {
            item.SetActive(false);
        }
        materiPanel[indexOn].SetActive(true);
    }
    void turnOffAllMaterialPanel()
    {
        foreach (var item in materiPanel)
        {
            item.SetActive(false);
        }
    }
    public void onMateriButtonPressed(int index)
    {
        turnOffOtherMateriPanel(index);
    }
    public void onButtonPressed(int index)
    {
        turnOffOtherPanel(index);
    }

    public void onBackButton()
    {
        turnOffOtherPanel(4);
        turnOffAllMaterialPanel();
    }
    public void LanguageSelect(string choose)
    {
        if (choose == "id")
            GameUtility.lang = GameUtility.Language.Indo;
        else if (choose == "en")
        {
            GameUtility.lang = GameUtility.Language.Eng;
        }
    }

    public void onPlayLevel(int lvl)
    {
        if (lvl <= unlockedLvl)
        {
            GameUtility.isPlaying = true;
            SceneTransaction.Instance.SceneLoad("Lvl" + lvl);
        }
        else
        {
            Debug.Log("Level Terkunci");
        }
    }
    public void onVolumeChanged()
    {
        bgmSource.volume = slider.value;
        GameUtility.currentVolume = slider.value;
    }
    public void volumeChanger()
    {
        slider.gameObject.SetActive(!slider.gameObject.activeSelf);
    }
    public void onBackToMenu() { SceneManager.LoadScene("Main Menu"); }
    public void onPlayVideo(int id)
    {
        turnOffOtherMateriPanel(id);
    }

}
