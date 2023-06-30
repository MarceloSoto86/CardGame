using System;

[System.Serializable]

public class CardPlayer 
{
    public int health, mana;
    public int ID;
    public bool myTurn;

    public CardPlayer(int health, int mana, int ID)
    {
        this.health = health;
        this.mana = mana;
        this.ID = ID;
    }

  
}
