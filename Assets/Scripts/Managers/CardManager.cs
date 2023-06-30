using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public List<Card> cards = new List<Card>(); //, player1Deck = new List<Card>(), player2Deck = new List<Card>();
    public List<int> player1Deck = new List<int>();
    public Transform player1Hand, player2Hand;
    public CardController cardControllerPrefab;
    public List<CardController> player1Cards = new List<CardController>(), player2Cards = new List<CardController>(), player1HandCards = new List<CardController>(), player2HandCards = new List<CardController>();


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GenerateCards();
       // FillDecks();
    }

 /*   private void FillDecks()
    {
        foreach (Card card in cards)
        {
            player1Deck.Add(new Card(card));
            player1Deck.Add(new Card(card));
        }

        foreach (Card card in cards)
        {
            player2Deck.Add(new Card(card));
            player2Deck.Add(new Card(card));
        }
    }*/

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateCards()
    {
        /*     THIS IS HOW WE CREATE A DECK:
         *     foreach(int cardIndex in player1Deck)
              {
                  CardController newCard = Instantiate(cardControllerPrefab, player1Hand);
                  newCard.transform.localPosition = Vector3.zero;
                  newCard.Initialize(cards[cardIndex], 0);
              }*/

        //THIS GENERATES CARDS FOR PLAYER 1 HAND AND ADDS IT TO THE PLAYER 1 CARDS LIST
        foreach (Card card in cards)
        {
            CardController newCard = Instantiate(cardControllerPrefab, player1Hand.root);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 0,player1Hand);
            player1HandCards.Add(newCard);

        }

        //THIS GENERATES CARDS FOR PLAYER 2 HAND AND ADDS IT TO THE PLAYER 2 CARDS LIST
        foreach (Card card in cards)
        {
            CardController newCard = Instantiate(cardControllerPrefab, player2Hand.root);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(card, 1,player2Hand);
            player2HandCards.Add(newCard);
        }
    }

    public void PlayCard(CardController card, int ID)
    {
        if (ID == 0)
        {
            player1Cards.Add(card);
            player1HandCards.Remove(card);
        }
        else
        {
            player2Cards.Add(card);
            player2HandCards.Remove(card);
        }
    }

    public void ProcessStartTurn(int ID)
    {
        List<CardController> cards = new List<CardController>();
        List<CardController> enemyCards = new List<CardController>();

        //THIS CHECKS WHAT PLAYER DECK IS ACTIVE ON THIS STARTING TURN
        if (ID == 0)
        {
            cards.AddRange(player1Cards);
            enemyCards.AddRange(player2Cards);
        }
        else
        {
            cards.AddRange(player2Cards);
            enemyCards.AddRange(player1Cards);
        }

        //THIS BLOCK OF CODE DESTROYS THE CARDS THAT REACHED 0 HEALTH

        foreach (CardController card in cards)
        {
            if (card == null) continue;
            if (card.card.health <= 0)
            {
                Destroy(card.gameObject);
            }
        }

        foreach (CardController card in enemyCards)
        {
            if (card.card.health <= 0)
            {
                Destroy(card.gameObject);
            }
        }

        //THIS CLEANS THE CARD LISTS


        player1Cards.Clear();
        player2Cards.Clear();


        //THIS BLOCK OF CODE CHECKS IF CARDS ARE NOT NULL AND ADD THE INTENDED CARD TO THE DECK

        foreach (CardController card in cards)
        {
            if (card != null)
            {
                if (ID == 0)
                {
                    player1Cards.Add(card);
                }
                else
                {
                    player2Cards.Add(card);
                }
            }
        }

        foreach (CardController card in enemyCards)
        {
            if (card != null)
            {
                if (ID == 1)
                {
                    player1Cards.Add(card);
                }
                else
                {
                    player2Cards.Add(card);
                }
            }
        }

        bool drawCard = false;
        if(ID == 0)
        {
            drawCard = player1HandCards.Count < 7;
        }
        else
        {
            drawCard = player2HandCards.Count < 7;

        }
        if (drawCard)
        {
            int randomCard = UnityEngine.Random.Range(0, this.cards.Count);
            CardController newCard = Instantiate(cardControllerPrefab, player1Hand.root);
            newCard.transform.localPosition = Vector3.zero;
            newCard.Initialize(this.cards[randomCard], ID, ID == 0 ? player1Hand : player2Hand);
            cards.Add(newCard);
            if (ID == 0)
                player1HandCards.Add(newCard);
            else 
                player2HandCards.Add(newCard);
        }


    }

    public void ProcessEndTurn(int ID)
    {
        List<CardController> cards = new List<CardController>();
        List<CardController> enemyCards = new List<CardController>();
        if (ID == 0)
        {
            cards.AddRange(player1Cards);
            enemyCards.AddRange(player2Cards);
        }
        else
        {
            cards.AddRange(player2Cards);
            enemyCards.AddRange(player1Cards);
        }
        foreach (CardController cardController in cards)
        {
            if(cardController == null) continue;
            if (AreThereCardsWithHealth(enemyCards))
            {
                int randomEnemyCard = UnityEngine.Random.Range(0, enemyCards.Count);
                while (enemyCards[randomEnemyCard].card.health <= 0) //REVISAR SI ESTO NO DEBERÍA SER card.health > 0 en vez de lo que está puesto
                {
                    randomEnemyCard = UnityEngine.Random.Range(0, enemyCards.Count);
                }
                enemyCards[randomEnemyCard].DamageReceived(cardController.card.attackPoints);
                cardController.transform.SetParent(cardController.transform.root);
                cardController.transform.DOMove(enemyCards[randomEnemyCard].transform.position,0.3f,true).onComplete += () =>
                {
                    cardController.ReturnToHand();
                };

                cardController.DamageReceived(enemyCards[randomEnemyCard].card.attackPoints);
            }
            else
            {
                int enemyID = ID == 0 ? 1 : 0;
                cardController.transform.SetParent(cardController.transform.root);
                cardController.transform.DOMove(ID == 0? player2Hand.transform.position: player1Hand.transform.position, 0.3f, true).onComplete += () =>
                {
                    cardController.ReturnToHand();
                };
                CardPlayerManager.instance.DamagePlayer(enemyID, cardController.card.attackPoints);
            }
        }
    }

   

    private bool AreThereCardsWithHealth(List<CardController> cards)
    {
        bool cardHasHealth = false;
        foreach (CardController card in cards)
        {
            if (card.card.health > 0)
            {
                cardHasHealth = true;
            }
        }

        return cardHasHealth;
    }
}
