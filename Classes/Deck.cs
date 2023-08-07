using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PokerCoba.Enums;
using PokerCoba.Interface;
namespace PokerCoba.Class;

public class Deck : IDeck
{
	public Stack<ICard> Cards{get;}
	
	public Deck()
	{
		Cards = new Stack<ICard>();
		InitializeDeck();
	}

	private void InitializeDeck()
	{
	   var cardSuit = Enum.GetValues(typeof(CardSuit));
	   var cardRank = Enum.GetValues(typeof(CardRank));
	
		foreach (CardSuit suit  in cardSuit)
		{
			foreach (CardRank rank in cardRank)
			{
				Cards.Push(new Card(suit, rank));
			}
		}
	}
	public void Shuffle()
	{
		List<ICard> cardList = new List<ICard>(Cards);
		Cards.Clear();
		
		var random = new Random();
		while(cardList.Count > 0)
		{
			int index = random.Next(cardList.Count);
			Cards.Push(cardList[index]);
			cardList.RemoveAt(index);
		}
	}
	public static void DealCard(PokerGame pokerGame)
	{
		Deck deck = new Deck();
		deck.Shuffle();
		
		int totalCardsToDeal = pokerGame.Players.Count * 2;
   		 if (deck.Cards.Count < totalCardsToDeal)
		{
			throw new InvalidOperationException("Not enough cards in the deck to deal to all players.");
   		}

		foreach(var player in pokerGame.Players)
		{
			player.Hand.AddCard(deck.Draw());
			player.Hand.AddCard(deck.Draw());
		}
	}
	public ICard Draw()
	{
		if(Cards.Count == 0)
		{
			throw new InvalidOperationException("Deck empty");
		}
		return Cards.Pop();
	}
}


