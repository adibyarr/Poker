using PokerCoba.Interface;
using PokerCoba.Enums;

namespace PokerCoba.Class;

public class HandEvaluator
{
	public CardType EvaluateHand(string[] cards)
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

	private bool IsRoyalFlush(string[] cards)
	{
		bool hasAce = false;
		bool hasKing =false;
		bool hasQueen = false;
		bool hasJack = false;
		bool hasTen = false;
		
		foreach (var card in cards)
		{
			if(card.Contains("A"))
				hasAce = true;
			if(card.Contains("K"))
				hasKing = true;
			if()
		}
	}

	private bool IsStraightFlush(ICard[] cards)
	{
		// Check if the hand has five consecutive cards of the same suit.
		return IsFlush(cards) && IsStraight(cards);
	}

	private bool IsFourOfAKind(ICard[] cards)
	{
		// Check if there are four cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 4);
	}

	private bool IsFullHouse(ICard[] cards)
	{
		// Check if there are three cards of one rank and two cards of another rank.
		var groups = cards.GroupBy(c => c.cardRank);
		return groups.Any(g => g.Count() == 3) && groups.Any(g => g.Count() == 2);
	}

	private bool IsFlush(ICard[] cards)
	{
		// Check if all five cards are of the same suit.
		return cards.GroupBy(c => c.cardSuit).Any(g => g.Count() == 5);
	}

	private bool IsStraight(ICard[] cards)
	{
		// Check if the hand has five consecutive cards of any suit.
		var ranks = cards.Select(c => (int)c.cardRank).OrderBy(r => r).ToList();
		return Enumerable.Range(ranks[0], 5).SequenceEqual(ranks);
	}

	private bool IsThreeOfAKind(ICard[] cards)
	{
		// Check if there are three cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 3);
	}

	private bool IsTwoPair(ICard[] cards)
	{
		// Check if there are two cards of one rank and two cards of another rank.
		var groups = cards.GroupBy(c => c.cardRank);
		return groups.Count(g => g.Count() == 2) == 2;
	}

	private bool IsOnePair(ICard[] cards)
	{
		// Check if there are two cards of the same rank.
		return cards.GroupBy(c => c.cardRank).Any(g => g.Count() == 2);
	}

	internal Hand EvaluateHand(List<ICard> allCards)
	{
		throw new NotImplementedException();
	}
}
