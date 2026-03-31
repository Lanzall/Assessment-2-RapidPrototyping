using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text cardValue;
    [SerializeField] private TMP_Text cardDescription;
    [SerializeField] private TMP_Text cardSuite;
    [SerializeField] private SpriteRenderer cardIcon;
    [SerializeField] private Renderer cardRenderer;

    private CardData cardData;

    public void Initialize(CardData _cardData)
    {
        cardData = _cardData;
        cardName.text = cardData.cardID.ToString();
        cardValue.text = cardData.value.ToString();
        cardDescription.text = cardData.description.ToString();
        cardSuite.text = cardData.suite.ToString();
        cardIcon.sprite = cardData.icon;
        cardRenderer.material.color = cardData.colour;

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }
}