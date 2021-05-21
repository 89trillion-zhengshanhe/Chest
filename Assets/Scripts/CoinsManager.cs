using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CoinsManager : MonoBehaviour
{
    public Text coinNumbertext;
    // 硬币Prefab
    public GameObject animatedCoinPrefab;
    // 硬币总数组件
    public Transform target;
    // 硬币总数组建位置
    private Vector3 targetPosition;
    [SerializeField] private GameObject Chest;
    private Vector3 chestPosition;
    private Vector3 spCoinPosition;
    public Ease easeType;
    public GameObject canvas;
    // 单次点击可获得最大硬币数
    public static int maxAddCoin = 15;
    // 按钮点击次数
    private int pressedTime = 1;
    // 硬币增幅数
    private int createCoinNum = 5;
    [SerializeField] private Animator chestAnimator;
    [SerializeField] private GameObject chestPartical;

    private void Awake ()
    {
        // 获取硬币总数组件的位置
        targetPosition = target.position;
        chestPosition = Chest.transform.position;
    }
    
    IEnumerator Animate (int amount)
    {
        // 根据硬币个数循环实例化硬币Prefab
        for (int i = 0; i < amount; i++)
        {
            spCoinPosition = new Vector3(chestPosition.x,chestPosition.y,0) + new Vector3(Random.Range(-10,10), 
                Random.Range(-10,10), chestPosition.z);
            GameObject animatedCoin = Instantiate(animatedCoinPrefab, new Vector3(0,0,0),
                animatedCoinPrefab.transform.rotation);
            animatedCoin.transform.SetParent(canvas.transform, false);
            animatedCoin.transform.DOScale(80, 0.4f);
            animatedCoin.transform.DOMove(spCoinPosition, 0.5f)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    animatedCoin.transform.DOMove (targetPosition, 0.5f)
                        .SetEase (easeType)
                        .OnComplete (() => {
                            // 到达硬币总数组件后销毁实例化的硬币Prefab
                            Destroy(animatedCoin);
                            chestAnimator.SetBool("box_close_1", true);
                            chestPartical.SetActive(false);
                        });
                });
            // 每生成一个特效后等待0.01s
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ClickAddCoinsWithAnimation()
    {
        // 计算获得硬币个数
        int addCoinNumber = pressedTime++ * createCoinNum;
        if (addCoinNumber >= maxAddCoin)
        {
            addCoinNumber = maxAddCoin;
        }
        // 计算玩家硬币总数并且显示
        string text = coinNumbertext.text;
        coinNumbertext.text = (addCoinNumber + Int32.Parse(text)).ToString();
        chestAnimator.SetBool("box_open_2", true);
        chestPartical.SetActive(true);
        StartCoroutine(Animate (addCoinNumber));
    }
}

