using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elenmeyer : MonoBehaviour
{
    public string elementType;
    public int soalID;
    public bool spesialElenmeyer = false;
    private SoalManager soal;
    public SpriteRenderer itemSprite;
    private readonly string plyr = "Player";
    private void Awake()
    {
        soal = FindObjectOfType<SoalManager>();
        itemSprite.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == plyr && soal.isInvokeSoal == false)
        {
            if (spesialElenmeyer == true && soal.isInDual == false)
            {
                soal.addCounterElen(1, itemSprite);
                if (soal.currentlevel == 1 || soal.currentlevel == 3)
                {
                    itemSprite.gameObject.SetActive(true);
                }
                elenCounter.Instance.addElenUI();
                StartCoroutine(poolBackItem());
            }
            else if (spesialElenmeyer == false)
            {
                soal.addCounterElen(1, itemSprite);
                if (soal.currentlevel == 1 || soal.currentlevel == 3)
                {
                    itemSprite.gameObject.SetActive(true);
                }
                elenCounter.Instance.addElenUI();
                StartCoroutine(poolBackItem());
            }
        }
    }
    IEnumerator poolBackItem()
    {
        yield return new WaitForSeconds(0.5f);
        if (soal.currentlevel == 1 || soal.currentlevel == 3)
            itemSprite.gameObject.SetActive(false);
        if (spesialElenmeyer)
            ElenPooler.Intance.SpesialPoolGetback(gameObject);
        else
            ElenPooler.Intance.PoolGetback(gameObject);
        StopCoroutine(poolBackItem());
    }
}
