using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using ExitGames.Client.Photon;
using System;
using DG.Tweening;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IDragHandler
{

    public Card card;
    public Image illustration, image;
    public TextMeshProUGUI cardName, health, defensePoints, attackPoints;
    private Transform originalParent;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }



    void Start()
    {
    }

    public void Initialize(Card card, int ownerID, Transform intendedParent)
    {
        this.card = new Card(card);
        this.card.ownerID = ownerID;
        illustration.sprite = card.illustration;
        cardName.text = card.cardName;
        defensePoints.text = card.defensePoints.ToString();
        attackPoints.text = card.attackPoints.ToString();
        health.text = card.health.ToString();
        originalParent = intendedParent;
        Tweener tween = transform.DOMove(intendedParent.transform.position, 1, true);
        transform.DOScale(1, 0.7f);
        tween.onComplete += () =>
        {
            transform.SetParent(intendedParent);
        };
        if (card.health == 0) health.text = "";
        if (card.defensePoints == 0) defensePoints.text = "";
    }


    public void DamageReceived(int amount)
    {
        card.health -= amount;
        health.text = card.health.ToString();
    }

   public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(originalParent.name == $"Player{card.ownerID + 1}PlayArea" || CardTurnManager.instance.currentCardPlayerTurn != card.ownerID)
        {
            transform.DOShakeScale(0.25f,0.5f,5);
        }
        else
        {
            transform.SetParent(transform.root);
            image.raycastTarget = false;
        }
      
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerEnter);

        if (originalParent.name == $"Player{card.ownerID + 1}PlayArea" || CardTurnManager.instance.currentCardPlayerTurn != card.ownerID)
        {

        }
        else
        {
            image.raycastTarget = true;
            AnalyzePointerUp(eventData);
        }
       
        
    }

    private void AnalyzePointerUp(PointerEventData eventData)
    {

        if (eventData.pointerEnter !=null && eventData.pointerEnter.name == $"Player{card.ownerID + 1}PlayArea")
        {
            if(CardPlayerManager.instance.FindPlayerByID(card.ownerID).mana >= card.manaPoints)
            {
                PlayCard(eventData.pointerEnter.transform);
                CardPlayerManager.instance.SpendMana(card.ownerID,card.manaPoints);
            }
            else
            {
                transform.DOShakeScale(0.25f, 0.25f, 3);
                ReturnToHand();
            }
        }
        else
        {
            ReturnToHand();
        }
        
    }

  

    private void PlayCard(Transform playArea)
    {
        transform.SetParent(playArea);
        transform.localPosition = Vector3.zero;
        originalParent = playArea;
        CardManager.instance.PlayCard(this, card.ownerID);
        
        //NOT Yet
    }

    public void ReturnToHand()
    {
        Tweener tween = transform.DOMove(originalParent.transform.position, 0.3f, true);
        //transform.DOScale(1, 0.7f);

        tween.onComplete += () =>
        {
            transform.SetParent(originalParent);
        };
        //transform.SetParent(originalParent);
        //transform.localPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent == originalParent) return;
        transform.position = eventData.position;
    }

}
