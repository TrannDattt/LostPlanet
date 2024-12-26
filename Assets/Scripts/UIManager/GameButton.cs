using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite btnReleased;
    [SerializeField] private Sprite btnClicked;
    [SerializeField] private Sprite btnHovering;

    [SerializeField] private Image displayedImage;

    [SerializeField] private UnityEvent callbackEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        displayedImage.sprite = btnHovering;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        displayedImage.sprite = btnReleased;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        displayedImage.sprite = btnReleased;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        displayedImage.sprite = btnClicked;
        GameAudioManager.Instance.PlaySFX(GameAudioManager.EAudio.BtnClickedSFX);
        callbackEvent?.Invoke();
    }
}
