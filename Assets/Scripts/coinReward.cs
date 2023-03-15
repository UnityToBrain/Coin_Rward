using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class coinReward : MonoBehaviour
{
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    void Start()
    {
        
        if (coinsAmount == 0) 
            coinsAmount = 10; // you need to change this value based on the number of coins in the inspector
        
        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];
        
        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            initialPos[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition;
            initialRotation[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
    }


   public void CountCoins()
    {
        pileOfCoins.SetActive(true);
        var delay = 0f;
        
        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(400f, 840f), 0.8f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);
             

            pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                .SetEase(Ease.Flash);
            
            
            pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

            delay += 0.1f;

            counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10,LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
        }

        StartCoroutine(CountDollars());
    }
    
    IEnumerator CountDollars()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerPrefs.SetInt("CountDollar",PlayerPrefs.GetInt("CountDollar") + 50 + PlayerPrefs.GetInt("BPrize"));
        counter.text = PlayerPrefs.GetInt("CountDollar").ToString();
        PlayerPrefs.SetInt("BPrize",0);
    }
}
