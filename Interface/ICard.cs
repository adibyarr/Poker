using PokerCoba.Enums;
namespace PokerCoba.Interface;

public interface ICard
{
	CardSuit cardSuit{get; set;}
	CardRank cardRank {get; set;}
}