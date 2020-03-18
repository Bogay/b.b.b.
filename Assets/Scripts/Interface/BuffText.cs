using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BuffText : MonoBehaviour
{
    private TextMeshProUGUI buffText;

    void Start()
    {
        buffText = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(ShowText());
    }

    /*這是一個讓字幕出現、上升並漸漸淡出的小動畫*/
    IEnumerator ShowText()
    {
        float y = transform.position.y;
        buffText.DOFade(0, 0.5f).SetEase(Ease.InQuint);
        transform.DOMoveY(y + 0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
