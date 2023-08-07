namespace PokerCoba.Interface;

public interface IPlayer
{
	string Name{get;set;}
	int Chips{get;set;}
	int ChipsBet{get; set;}
	IHand Hand{get;}
	bool Folded{get;set;}


}