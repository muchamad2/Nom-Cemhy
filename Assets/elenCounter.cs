using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class elenCounter : MonoBehaviour
{
    public static elenCounter Instance {get; private set;}
    public TextMeshProUGUI elenCount;
    private int currentElen;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
        elenCount = GetComponentInChildren<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        elenCount.SetText(currentElen.ToString());
    }
    public void addElenUI() {
        currentElen += 1;
    }
}
