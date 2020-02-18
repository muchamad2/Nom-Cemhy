using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mm : MonoBehaviour
{
    private int curIndex;
    public Sprite[] nextindikator;
    public Sprite[] indoImg;
    public Sprite[] engImg;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        if (indoImg.Length != 0 && engImg.Length != 0)
        {
            if (GameUtility.lang == GameUtility.Language.Indo)
            {
                nextindikator = indoImg;
            }
            else
            {
                nextindikator = engImg;
            }
        }

        curIndex = 0;
        panel.GetComponent<Image>().sprite = nextindikator[curIndex];
    }
    
    public void onBackPressed()
    {
        gameObject.SetActive(false);
    }
    public void nextIndi(int index)
    {

        if (nextindikator.Length > 2)
        {
            if (index == 1)
            {
                curIndex += 1;
                if (curIndex >= nextindikator.Length - 1)
                    curIndex = nextindikator.Length - 1;
            }
            else
            {
                curIndex -= 1;
                if (curIndex <= 0)
                    curIndex = 0;

            }

            Debug.Log(curIndex);

        }
        else
        {
            curIndex = index;
        }
        panel.GetComponent<Image>().sprite = nextindikator[curIndex];
    }
}
