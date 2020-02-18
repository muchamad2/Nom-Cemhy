using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISetting : MonoBehaviour
{
    public TextMeshProUGUI langTxt;
    public TextMeshProUGUI btnNext;

    private void Update()
    {
        switch (GameUtility.lang)
        {
            case GameUtility.Language.Indo:
                langTxt.text = "Bahasa";
                btnNext.text = "Selanjutnya";
                break;
            case GameUtility.Language.Eng:
                langTxt.text = "Language";
                btnNext.text = "Next";
                break;
        }
    }
}
