using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class Card 
{
    public string cardName;
    public int health, defensePoints, attackPoints, ownerID, manaPoints;
    public Sprite illustration;

    public Card()
    {

    }

    public Card(Card card)
    {
        cardName= card.cardName;
        health= card.health;
        attackPoints= card.attackPoints;
        defensePoints= card.defensePoints;
        manaPoints= card.manaPoints;
        illustration= card.illustration;
    }
    
}
