using PokerCoba.Interface;
using PokerCoba.Enums;
namespace PokerCoba;

public class Card : ICard
{
    public CardSuit cardSuit { get; set; }
    public CardRank cardRank { get; set; }

    public Card(CardSuit suit, CardRank rank)
    {
        cardSuit = suit;
        cardRank = rank;
    }

    public override string ToString()
    {
        return $"{cardRank} of {cardSuit}";
    }
}

