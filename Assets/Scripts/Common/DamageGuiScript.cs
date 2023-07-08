using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageGuiScript : MonoBehaviour
{
    public Vector2 initialOffset;
    public Vector2 finalOffset;
    public float fadeDuration;
    float fadeStartTime;
    TextMeshProUGUI text;
    private void Start()
    {
        fadeStartTime = Time.time;
        text=GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        float progress=(Time.time-fadeStartTime)/fadeDuration;
        if (progress <= 1)
        {
            transform.localPosition = Vector2.Lerp(initialOffset, finalOffset, progress);
            text.color =new Color(text.color.r, text.color.g, text.color.b, progress);
        }
        else Destroy(gameObject);
    }
}
