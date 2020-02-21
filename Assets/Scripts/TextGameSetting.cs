using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public struct TextSetting
{
    public TextMeshProUGUI textArea;
    public string indoText;
    public string engText;
}
public class TextGameSetting : MonoBehaviour
{
    [SerializeField] TextSetting[] settingTexts;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var itemText in settingTexts)
        {
            switch (GameUtility.lang)
            {
                case GameUtility.Language.Indo:
                    itemText.textArea.text = itemText.indoText;
                    break;
                case GameUtility.Language.Eng:
                    itemText.textArea.text = itemText.engText;
                    break;
            }

        }
    }
}
