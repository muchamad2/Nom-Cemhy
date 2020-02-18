using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skorText;
    [SerializeField] private int currentSkor = 0;
    private SoalManager skorInit;
    // Start is called before the first frame update
    void Awake()
    {
        skorText = GetComponent<TextMeshProUGUI>();
        skorInit = FindObjectOfType<SoalManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //update skor nya belakangan
        if(skorInit.soalDone()) {
            setSkor("Score " + currentSkor.ToString());
        }
        
    }

    public void addSkor(int skor) {
        currentSkor += skor;
    }

    void setSkor(string score) {
        skorText.SetText(score);
    }
}
