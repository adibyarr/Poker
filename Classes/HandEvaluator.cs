using PokerCoba.Interface;
using PokerCoba.Enums;

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
			throw new NotImplementedException();

		if (IsFullHouse(cards))
			throw new NotImplementedException();

		if (IsFlush(cards))
			throw new NotImplementedException();

		if (IsStraight(cards))
			throw new NotImplementedException();

		if (IsThreeOfAKind(cards))
			throw new NotImplementedException();

		if (IsTwoPair(cards))
			throw new NotImplementedException();

		if (IsOnePair(cards))
			throw new NotImplementedException();

		return CardType.HighCard;
	}

 

	private bool IsRoyalFlush(List<ICard> cards)
	{
		bool sameSuit = cards.All(c => c.cardSuit == cards[0].cardSuit);
		
		bool containsRoyalFlush = cards.Any(c => c.cardRank== CardRank.Ace) &&
								  cards.Any(c=> c.cardRank == CardRank.King)&&
								  cards.Any(c => c.cardRank == CardRank.Queen)&&
								  cards.Any(c=> c.cardRank == CardRank.Jack) &&
								  cards.Any(c => c.cardRank == CardRank.Ten);
		return sameSuit	& containsRoyalFlush;
	}

	private static bool IsStraightFlush(List<ICard> cards)
	{
		bool sameSuit = cards.All(c => c.cardSuit == cards[0].cardSuit);
		var sortedCards = cards.OrderBy(c => c.cardRank).ToList();
		
		bool isStraight = true;
		for (int i = 1; i< sortedCards.Count; i++)
		{
			if(sortedCards[i].cardRank != sortedCards[i-1].cardRank+1)
			{
				isStraight = false;
				break;
			}
		}
		return sameSuit && isStraight;
	}

	private static bool IsFourOfAKind(List<ICard> cards)
	{
		// Check if there are four cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 4);
	}

	private static bool IsFullHouse(List<ICard> cards)
	{
		// Check if there are three cards of one rank and two cards of another rank.
		var groups = cards.GroupBy(c => c.cardRank).ToList();
		return groups.Count == 2 && 
				(groups[0].Count() == 3 && groups[1].Count() == 2 ||
				 groups[0].Count() == 2 && groups[1].Count() == 3);
	}

	private static bool IsFlush(List<ICard> cards)
	{
		// Check if all five cards are of the same suit.
		return cards.All(c=> c.cardSuit == cards[0].cardSuit);
	}

	private static bool IsStraight(List<ICard> cards)
	{
		var sortedCards = cards.OrderBy(c => c.cardRank).ToList();
		bool isStraight = true;
		for (int i = 0; i < sortedCards.Count; i++)
		{
			if(sortedCards[i].cardRank != sortedCards[i-1].cardRank+1)
			{
				isStraight = false;
				break;
			}
		}
		return isStraight;
	}

	private static bool IsThreeOfAKind(List<ICard> cards)
	{
		// Check if there are three cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 3);
	}

	private static bool IsTwoPair(List<ICard> cards)
	{
		// Check if there are two cards of one rank and two cards of another rank.
		var groups = cards.GroupBy(c => c.cardRank);
		return groups.Count(g => g.Count() == 2) == 2;
	}

	private static bool IsOnePair(List<ICard> cards)
	{
		// Check if there are two cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 2);
	}
}
