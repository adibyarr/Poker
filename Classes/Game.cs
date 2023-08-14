using System.Collections.Generic;
using System.Linq;
using PokerCoba.Enums;
using PokerCoba.Interface;

namespace PokerCoba.Class;

public class PokerGame
{
	
	public List<IPlayer> Players { get; }
	public List<ICard> CommunityCards { get; }
	public int CommunityPot { get; private set; }
	private readonly Deck _deck;
	private PlayerActionHandler _playerActionHandler;
	public PokerGame()
	{

		Players = new List<IPlayer>();
		CommunityCards = new List<ICard>();
		CommunityPot = 0;
		_deck = new Deck();
		_playerActionHandler = new PlayerActionHandler();
	}

	public void AddPlayer(IPlayer player)
	{
		Players.Add(player);
	}
	public void DealCards()
	{
		Deck deck = new Deck();
		deck.Shuffle();
		foreach(var player in Players)
		{
			player.Hand.Clear();
			for(int i = 0; i < 2; i++)
			{
				ICard card = deck.Draw();
				player.Hand.AddCard(card);
			}
		}
	}
	public void DealCommunityCards(int numCommunityCards)
	{
		Deck deck = new Deck();
		deck.Shuffle();
		
		for(int i = 0; i < numCommunityCards; i++)
		{
			ICard communityCards = deck.Draw();
			CommunityCards.Add(communityCards);
			foreach(var player in Players)
			{
				ICard cardToRemove = player.Hand.Cards.FirstOrDefault(card => card.cardRank == communityCards.cardRank && card.cardSuit == communityCards.cardSuit);
				
				if(cardToRemove != null)
				{
					player.Hand.Cards.Remove(cardToRemove);
				}
			}
		}
	}
	
	
	
	public(IPlayer winner, CardType WinningHand)  DetermineWinner()
	
	{
		IPlayer winner = Players[0];
		HandEvaluator evaluator = new HandEvaluator();
		CardType WinningHand = CardType.HighCard;
		
		
		foreach(var player in Players)
		{
			var allCards = new List<ICard>(player.Hand.Cards);
			allCards.AddRange(CommunityCards);

			CardType playerHandType = evaluator.EvaluateHand(allCards); 
			if(playerHandType > WinningHand)
			{
				winner = player;
				WinningHand = playerHandType;
			}
		}
		int totalChipsPot = CommunityPot;
		winner.Chips += totalChipsPot;
		return (winner, WinningHand); 
	}
   
}