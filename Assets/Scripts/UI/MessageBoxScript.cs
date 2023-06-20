using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageBoxScript : MonoBehaviour
{
    static MessageBoxScript instance;
    public static MessageBoxScript Instance
    {
        get { 
            return instance; 
        }
    }
    [SerializeField] Text text;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float time=5;
    private void Awake()
    {
        instance = this;
    }
    public void ChangeMessage(string message)
    {
        StopCoroutine(FadeMessage());
        canvasGroup.DOFade(100, 0);
        text.text = message;     
        StartCoroutine(FadeMessage());
    }
    IEnumerator FadeMessage()
    {
        yield return new WaitForSeconds(time);
        canvasGroup.DOFade(0, 5);
    }
}
