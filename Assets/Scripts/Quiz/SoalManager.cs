using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SoalManager : MonoBehaviour
{
    [SerializeField] private bool tempJwbn1, tempJwbn2, spesialJwbn1;
    //nanti di jadikan satu
    [SerializeField] private int maxSoal;
    [SerializeField] private int currSoalindex = 0;

    public GameObject panelPenjelasan, panelBenar;
    private GameObject panelSoal, panelSoal1, panelSoal2;
    public GameObject btnView, btnSkip;
    private const int initCount = 0;
    private const int nilaiSoal = 10;
    private int totalNilaiSoal;
    private bool isSoalDone = false;
    private bool answerState = false;
    [HideInInspector]
    public bool isInvokeSoal = false;
    [HideInInspector]
    public bool isInDual = false;
    private int count;
    private int countItem;
    private ScoreManager skor;
    [SerializeField] int triggerCount = 2;
    public int currentlevel;


    public TextMeshProUGUI[] elemenText = new TextMeshProUGUI[2];
    public Button[] opsiButton = new Button[5];
    public Sprite[] itemSprite = new Sprite[10];
    public Sprite itemSpriteSpesial;

    [SerializeField]
    private List<soal1> kunciJwbnLvl = new List<soal1>();
    [SerializeField]
    private List<penjelasan> penjelasanLvl = new List<penjelasan>();
    public List<soal1> soalIndo = new List<soal1>();
    public List<penjelasan> penjelasanId = new List<penjelasan>();
    public List<soal1> soalEn = new List<soal1>();
    public List<penjelasan> penjelasanEn = new List<penjelasan>();
    public bool specialSoal = false;
    private string notifBenar, notifSalah;
    private string idNotifBenar = "Selamat Anda Benar <br><br> Skor yang diperoleh : +";
    private string idNotifSalah = "Maaf Anda Masih Belum Benar <br><br> Skor yang diperoleh : +0";
    private string enNotifBenar = "Congratulations <br><br> The additional score you get is : +";
    private string enNotifSalah = "Sorry, You're wrong <br><br> You don't get additional score";

    // Start is called before the first frame update
    void Awake()
    {
        switch (GameUtility.lang)
        {
            case GameUtility.Language.Indo:
                kunciJwbnLvl = soalIndo;
                penjelasanLvl = penjelasanId;
                notifBenar = idNotifBenar;
                notifSalah = idNotifSalah;
                break;
            case GameUtility.Language.Eng:
                kunciJwbnLvl = soalEn;
                penjelasanLvl = penjelasanEn;
                notifBenar = enNotifBenar;
                notifSalah = enNotifSalah;
                break;
        }
        maxSoal = kunciJwbnLvl.Count();
        countItem = 0;
        skor = FindObjectOfType<ScoreManager>();
        panelSoal = GameObject.FindGameObjectWithTag("Panel Soal");
        panelSoal1 = GameObject.FindGameObjectWithTag("Panel Soal 1");
        panelSoal2 = GameObject.FindGameObjectWithTag("Panel Soal 2");
        //panelPenjelasan = GameObject.FindGameObjectWithTag("Panel Penjelasan");
        //panelBenar = GameObject.FindGameObjectWithTag("Panel Benar");


        panelSoal.SetActive(false);

        panelPenjelasan.SetActive(false);

        panelBenar.SetActive(false);

        btnSkip.SetActive(false);
        GameUtility.currBenar = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //display panel soal
        if (count == triggerCount)
        {
            isSoalDone = false;
            StartCoroutine(invokeSoal());
            count = initCount;
            isInDual = false;
        }
        if (currSoalindex == maxSoal)
        {
            StartCoroutine(loadNextLevel(currentlevel + 1));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }


    public void addCounterElen(int add, SpriteRenderer sprite)
    {
        if (currentlevel == 3)
        {
            if (kunciJwbnLvl[currSoalindex].dualElenmeyer)
            {
                triggerCount = 2;
                isInDual = true;
            }
            else
            {
                triggerCount = 1;
                isInDual = false;
            }
            if (kunciJwbnLvl[currSoalindex].unsur1 == "?")
            {
                sprite.sprite = itemSpriteSpesial;
            }
            else
            {
                sprite.sprite = itemSprite[countItem];
                countItem += add;
            }

        }
        if (currentlevel == 1)
        {
            sprite.sprite = itemSprite[countItem];
            countItem += add;
        }
        count += add;
    }

    public bool soalDone()
    {
        return isSoalDone;
    }

    IEnumerator waitForCheck() { yield return new WaitForSeconds(10f); }

    public void checkJwbnSoal1(int pil)
    {
        Debug.Log("Chekc");
        if (pil == kunciJwbnLvl[currSoalindex].dapatBereaksi)
        {
            tempJwbn1 = true;
            if (kunciJwbnLvl[currSoalindex].specialAfter == true)
            {
                spesialJwbn1 = true;
            }
        }
        else
        {
            tempJwbn1 = false;
        }
        if (kunciJwbnLvl[currSoalindex].specialAfter == true)
        {
            Skip();
            StartCoroutine(invokeSoal());
        }
        else
        {
            if (currSoalindex <= kunciJwbnLvl.Count - 1)
            {
                Debug.Log("cek nek soal");
                StartCoroutine(invokeNextSoal());
            }
        }

    }

    public void checkJwbnSoal2(int pil)
    {

        if (pil == kunciJwbnLvl[currSoalindex].pilBenar)
        {
            tempJwbn2 = true;
        }
        else
        {
            tempJwbn2 = false;
        }
        if (currentlevel == 2)
        {
            tempJwbn1 = true;
        }
        else if (!kunciJwbnLvl[currSoalindex].doubleSoal)
        {
            tempJwbn1 = true;
        }
        invokeBenarSalah(tempJwbn1, tempJwbn2);
    }

    IEnumerator loadNextLevel(int lvl)
    {
        yield return new WaitForSeconds(1f);
        if (currentlevel == 3)
        {
            //SceneTransaction.Instance.SceneLoad("Main Menu");
            GameManager.Instance.showReward();
        }
        else
        {
            if (GameUtility.currBenar >= GameManager.Instance.minimBenar)
            {
                //SceneTransaction.Instance.SceneLoad("Lvl" + lvl);
                GameManager.Instance.showProgressionReward();
                GameUtility.totalBenar += GameUtility.currBenar;
                mainMenuController.unlockedLvl = lvl;
            }
            else
            {
                SceneTransaction.Instance.SceneLoad("Lvl" + mainMenuController.unlockedLvl);
            }
        }


    }

    public TextMeshProUGUI soalLvl2text;
    public TextMeshProUGUI soal1Text;
    void setTextSoal_2()
    {
        if (currentlevel == 2)
        {
            soalLvl2text.SetText(kunciJwbnLvl[currSoalindex].unsur3);
        }
        else if (currentlevel == 3)
        {
            if (kunciJwbnLvl[currSoalindex].doubleSoal)
                soalLvl2text.SetText(kunciJwbnLvl[currSoalindex].unsur4);
            else
                soalLvl2text.SetText(kunciJwbnLvl[currSoalindex].unsur3);
        }
        else if(currentlevel == 1){
            switch (GameUtility.lang)
            {
                case GameUtility.Language.Indo:
                    soalLvl2text.SetText("Apa nama senyawanya?");
                    break;
                case GameUtility.Language.Eng:
                    soal1Text.SetText("what is the name of the compound?");
                    break;
            }
            
        }
        opsiButton[0].GetComponentInChildren<TextMeshProUGUI>().SetText(kunciJwbnLvl[currSoalindex].pilA);
        opsiButton[1].GetComponentInChildren<TextMeshProUGUI>().SetText(kunciJwbnLvl[currSoalindex].pilB);
        opsiButton[2].GetComponentInChildren<TextMeshProUGUI>().SetText(kunciJwbnLvl[currSoalindex].pilC);
        opsiButton[3].GetComponentInChildren<TextMeshProUGUI>().SetText(kunciJwbnLvl[currSoalindex].pilD);
        opsiButton[4].GetComponentInChildren<TextMeshProUGUI>().SetText(kunciJwbnLvl[currSoalindex].pilE);
    }

    IEnumerator invokeSoal()
    {

        if (currentlevel == 1)
        {
            elemenText[0].SetText(kunciJwbnLvl[currSoalindex].unsur1);
            elemenText[1].SetText(kunciJwbnLvl[currSoalindex].unsur2);
            switch (GameUtility.lang)
            {
                case GameUtility.Language.Indo:
                    soal1Text.SetText("Apakah kedua unsur ini bisa berkaitan?");
                    break;
                case GameUtility.Language.Eng:
                    soal1Text.SetText("Are these two compounds possible to bind?");
                    break;
            }
            panelSoal1.SetActive(true);
            panelSoal2.SetActive(false);
        }
        else if (currentlevel == 2)
        {
            setTextSoal_2();
        }
        else if (currentlevel == 3)
        {
            if (kunciJwbnLvl[currSoalindex].doubleSoal)
            {
                soal1Text.SetText(kunciJwbnLvl[currSoalindex].unsur3);
                panelSoal1.SetActive(true);
                panelSoal2.SetActive(false);
            }
            else
            {
                setTextSoal_2();
                panelSoal2.SetActive(true);
            }
        }
        isInvokeSoal = true;
        yield return new WaitForSeconds(.1f);

        //Time.timeScale = 0f;
        panelSoal.SetActive(true);
    }

    IEnumerator invokeNextSoal()
    {
        Debug.Log("Masuk invoke next soal");
        setTextSoal_2();

        yield return new WaitForSecondsRealtime(0.75f);

        if (currentlevel == 1)
        {
            panelSoal1.SetActive(false);
            panelSoal2.SetActive(true);
        }
        else if (kunciJwbnLvl[currSoalindex].doubleSoal)
        {
            panelSoal1.SetActive(false);
            panelSoal2.SetActive(true);
        }
    }
    void invokeBenarSalah(bool jwbn1, bool jwbn2)
    {
        panelSoal.SetActive(false);
        panelBenar.SetActive(true);
        if (kunciJwbnLvl[(currSoalindex > 0) ? currSoalindex - 1 : currSoalindex].specialAfter == true)
        {
            if (jwbn1 && jwbn2 && spesialJwbn1)
            {
                skor.addSkor(nilaiSoal);
                panelBenar.GetComponentInChildren<TextMeshProUGUI>().SetText(notifBenar + nilaiSoal);
                btnSkip.SetActive(true);
                answerState = true;
                GameUtility.currBenar += 1;
            }
            else
            {
                panelBenar.GetComponentInChildren<TextMeshProUGUI>().SetText(notifSalah);
                btnSkip.SetActive(false);
                answerState = false;
            }
        }
        else
        {
            if (jwbn1 && jwbn2)
            {
                skor.addSkor(nilaiSoal);
                panelBenar.GetComponentInChildren<TextMeshProUGUI>().SetText(notifBenar + nilaiSoal);
                btnSkip.SetActive(true);
                answerState = true;
                GameUtility.currBenar += 1;
            }
            else
            {
                panelBenar.GetComponentInChildren<TextMeshProUGUI>().SetText(notifSalah);
                btnSkip.SetActive(false);
                answerState = false;
            }
        }

        isSoalDone = true;
        tempJwbn1 = false;
        tempJwbn2 = false;
        spesialJwbn1 = false;

    }
    public void Skip()
    {
        panelBenar.SetActive(false);
        currSoalindex += 1;
        isInvokeSoal = false;
    }
    public void Penjelasan()
    {
        StartCoroutine(invokePenjelasan(answerState));
    }
    IEnumerator invokePenjelasan(bool state)
    {
        panelBenar.SetActive(false);
        panelPenjelasan.SetActive(true);


        if (state)
        {
            panelPenjelasan.GetComponentInChildren<TextMeshProUGUI>().SetText(penjelasanLvl[currSoalindex].benar);
        }
        else
        {
            panelPenjelasan.GetComponentInChildren<TextMeshProUGUI>().SetText(penjelasanLvl[currSoalindex].salah);
        }

        yield return new WaitForSecondsRealtime(15f);

        Time.timeScale = 1f;

        panelPenjelasan.SetActive(false);
        currSoalindex += 1;
        isInvokeSoal = false;
        StopCoroutine(invokePenjelasan(state));
    }
}
