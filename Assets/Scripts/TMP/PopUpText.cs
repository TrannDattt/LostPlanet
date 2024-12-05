using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform rectTransform;

    public ETextType textType;

    public float flySpeed;

    public float visibleTime;
    private float timeCount = 0;
    private bool isVisible = false;

    public void SetInstacne(Vector2 spawnPos, string content)
    {
        rectTransform.position = spawnPos;
        text.SetText(content);

        isVisible = true;
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        isVisible = false;
        gameObject.SetActive(false);
        timeCount = 0;

        TMPPooling.Instance.ReturnObjectToPool(this);
    }

    private void FixedUpdate()
    {
        if (isVisible)
        {
            timeCount += Time.fixedDeltaTime;
            transform.position += Vector3.up * flySpeed;
        }

        if (timeCount >= visibleTime)
        {
            ReturnToPool();
        }
    }
}
