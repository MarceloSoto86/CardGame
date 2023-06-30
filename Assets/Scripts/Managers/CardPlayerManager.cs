using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlayerManager : MonoBehaviour
{
    public static CardPlayerManager instance;
    public List<CardPlayer> cardPlayers = new List<CardPlayer>();

    internal void AssignTurn(int currentCardPlayerTurn)
    {

        // FindPlayerByID(currentCardPlayerTurn).myTurn = true;

        /* CardPlayer player = cardPlayers.Find(x => x.ID == currentCardPlayerTurn);
        player.myTurn = true;*/

        //Another way to code the above would be:
        foreach (CardPlayer player in cardPlayers)
        {
            player.myTurn = player.ID == currentCardPlayerTurn;
            if (player.myTurn) player.mana = 5;
            //foundplayer = player;
            /*  if(player.ID == currentCardPlayerTurn)
              {
                  player.myTurn = true;
              }
              else
              {
                  player.myTurn = false;
              }*/

            //OR ALSO a Third way would be:
            // player.myTurn = player.ID == currentCardPlayerTurn;
        }


    }

    // public CardPlayer[] playersArray;

    public void DamagePlayer(int ID, int damage)
    {
        CardPlayer player = FindPlayerByID(ID);
        player.health -= damage;
        UIManager.instance.UpdateHealthValues(cardPlayers[0].health, cardPlayers[1].health);

        if (player.health <= 0)
        {
            PlayerLost(ID);
        }
    }


    private void Start()
    {
        UIManager.instance.UpdateValues(cardPlayers[0], cardPlayers[1]);
    }

    private void Awake()
    {
        instance = this;
    }

    private void PlayerLost(int ID)
    {
        UIManager.instance.GameFinished(ID == 0 ? FindPlayerByID(1) : FindPlayerByID(0));
    }

    public CardPlayer FindPlayerByID(int ID)
    {

        CardPlayer foundplayer = null;

        foreach (CardPlayer player in cardPlayers)
        {
            //player.myTurn = player.ID == currentCardPlayerTurn;
            if(player.ID == ID)
            foundplayer = player;
            /*  if(player.ID == currentCardPlayerTurn)
              {
                  player.myTurn = true;
              }
              else
              {
                  player.myTurn = false;
              }*/

            //OR ALSO a Third way would be:
            // player.myTurn = player.ID == currentCardPlayerTurn;
        }


        return foundplayer;
    }

    internal void SpendMana(int ownerID, int manaPoints)
    {
        FindPlayerByID(ownerID).mana -= manaPoints;
        UIManager.instance.UpdateManaValues(cardPlayers[0].mana, cardPlayers[1].mana);
        
    }
}
