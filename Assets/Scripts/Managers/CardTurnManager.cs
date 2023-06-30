using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTurnManager : MonoBehaviour
{
    public static CardTurnManager instance;
    public int currentCardPlayerTurn;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCardGameplayTurn(0);
    }

    public void StartCardGameplayTurn(int playerID)
    {
        currentCardPlayerTurn= playerID;
        
        StartCardTurn();
    }

    public void StartCardTurn()
    {
        GameplayUIController.instance.UpdateCurrentPlayerTurn(currentCardPlayerTurn);
        CardPlayerManager.instance.AssignTurn(currentCardPlayerTurn);
        CardManager.instance.ProcessStartTurn(currentCardPlayerTurn);
    }

    public void EndCardTurn()
    {

        CardManager.instance.ProcessEndTurn(currentCardPlayerTurn);
        StartCoroutine(WaitForAttacks(currentCardPlayerTurn == 0 ?CardManager.instance.player1Cards.Count: CardManager.instance.player2Cards.Count));
        currentCardPlayerTurn = currentCardPlayerTurn == 0 ? 1 : 0;
        
    }

    private IEnumerator WaitForAttacks(float cards)
    {
        yield return new WaitForSeconds(cards * 0.35f);
        StartCardTurn();

    }
}
