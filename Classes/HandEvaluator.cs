using PokerCoba.Interface;
using PokerCoba.Enums;
using System.Linq;
namespace PokerCoba.Class;

public class HandEvaluator
{
	public  CardType EvaluateHand(List<ICard> cards)
	{
		if (IsRoyalFlush(cards))
			return CardType.RoyalFlush;

		if (IsStraightFlush(cards))
			return CardType.StraightFlush;

		if (IsFourOfAKind(cards))
			return CardType.FourOfAKind;

		if (IsFullHouse(cards))
			return CardType.FullHouse;

		if (IsFlush(cards))
			return CardType.Flush;

		if (IsStraight(cards))
			return CardType.Straight;

		if (IsThreeOfAKind(cards))
			return CardType.ThreeOfAKind;

		if (IsTwoPair(cards))
			return CardType.TwoPair;

		if (IsOnePair(cards))
			return CardType.OnePair;

		return CardType.HighCard;
	}

 

	private bool IsRoyalFlush(List<ICard> cards)
	{
		bool _sameSuit = cards.All(c => c.cardSuit == cards[0].cardSuit);
		
		bool _containsRoyalFlush = cards.Any(c => 
						c.cardRank== CardRank.Ace ||
						c.cardRank == CardRank.King ||
						c.cardRank == CardRank.Queen ||
						c.cardRank == CardRank.Jack ||
						c.cardRank == CardRank.Ten);
								
		return _sameSuit & _containsRoyalFlush;
	}

	private static bool IsStraightFlush(List<ICard> cards)
	{
		bool _sameSuit = cards.All(c => c.cardSuit == cards[0].cardSuit);
		var _sortedCards = cards.OrderBy(c => c.cardRank).ToList();
		
		bool isStraight = true;
		for (int i = 1; i< _sortedCards.Count; i++)
		{
			if(_sortedCards[i].cardRank != _sortedCards[i-1].cardRank+1)
			{
				isStraight = false;
				break;
			}
		}
		return _sameSuit && isStraight;
	}

	private static bool IsFourOfAKind(List<ICard> cards)
	{
		// Check if there are four cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 4);
	}

	private static bool IsFullHouse(List<ICard> cards)
	{
		// Check if there are three cards of one rank and two cards of another rank.
		var _groups = cards.GroupBy(c => c.cardRank).ToList();
		return _groups.Count == 2 && 
				(_groups[0].Count() == 3 && _groups[1].Count() == 2 ||
				 _groups[0].Count() == 2 && _groups[1].Count() == 3);
	}

	private static bool IsFlush(List<ICard> cards)
	{
		
		return cards.All(c=> c.cardSuit == cards[0].cardSuit);
	}

	private static bool IsStraight(List<ICard> cards)
	{
		var sortedCards = cards.OrderBy(c => c.cardRank).ToList();
		bool isStraight = true;
		
		if(sortedCards[0].cardRank == CardRank.Ace && 
			sortedCards[1].cardRank == CardRank.Two &&
			sortedCards[2].cardRank == CardRank.Three &&
			sortedCards[3].cardRank == CardRank.Four &&
			sortedCards[4].cardRank == CardRank.Five)
			{
				return true;
			}
		for(int i = 1; i < sortedCards.Count; i++)
		{
			if(sortedCards[i].cardRank != sortedCards[i-1].cardRank +1)
			{
				isStraight = false;
				break;
			}
		}
		return isStraight;
	}

	private static bool IsThreeOfAKind(List<ICard> cards)
	{
	
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 3);
	}

	private static bool IsTwoPair(List<ICard> cards)
	{
		
		var groups = cards.GroupBy(c => c.cardRank);
		return groups.Count(g => g.Count() == 2) == 2;
	}

	private static bool IsOnePair(List<ICard> cards)
	{
		
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 2);
	}
}
