using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Slider slotSlider;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Sprite[] slotSprites;
    [SerializeField] private int[] randNum;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private AudioClip spinningSound;
    [SerializeField] private AudioClip stopSound;
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    private bool isSpinning = true;

    private enum ImageType
    {
        suibun,
        kaihatu,
        inakunaranai,
        secret
    }

    private void Start()
    {
        slotImages[0].sprite = slotSprites[3];
        slotImages[1].sprite = slotSprites[3];
        slotImages[2].sprite = slotSprites[3];

        StartCoroutine(StartSlot());
    }

    private void Update()
    {
        if (isSpinning)
        {
            return;
        }
        if (slotSlider.value == 0f)
        {
            StartCoroutine(StartSlot());
            winUI.SetActive(false);
            loseUI.SetActive(false);
            isSpinning = true;
        }
    }

    private IEnumerator StartSlot()
    {
        audioSource1.Play();
        while (slotSlider.value <= 0.9f)
        {
            randNum[0] = UnityEngine.Random.Range(0, slotSprites.Length - 1);
            randNum[1] = UnityEngine.Random.Range(0, slotSprites.Length - 1);
            randNum[2] = UnityEngine.Random.Range(0, slotSprites.Length - 1);
            slotImages[0].sprite = slotSprites[randNum[0]];
            slotImages[1].sprite = slotSprites[randNum[1]];
            slotImages[2].sprite = slotSprites[randNum[2]];
            yield return new WaitForSeconds(0.1f);
        }
        OnFinish();
    }

    private void OnFinish()
    {
        isSpinning = false;
        audioSource1.Pause();
        audioSource2.Play();
        if (CheckSprite(randNum[0]) != CheckSprite(randNum[1]) && CheckSprite(randNum[1]) != CheckSprite(randNum[2]) && CheckSprite(randNum[0]) != CheckSprite(randNum[2]))
        {
            OnWin();
        }
        else
        {
            OnLose();
        }
    }

    private void OnWin()
    {
        Debug.Log("You Win!");
        slotSlider.value = 0;
        winUI.SetActive(true);
    }
    
    private void OnLose()
    {
        Debug.Log("You Lose!");
        loseUI.SetActive(true);
    }

    private int CheckSprite(int num)
    {
        if (num == 0 || num == 1)
        {
            return 1;
        } else if (num == 2 || num == 3)
        {
            return 2;
        } else if (num == 4 || num == 5)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}
