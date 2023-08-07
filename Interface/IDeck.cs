using PokerCoba.Class;
namespace PokerCoba.Interface;

public interface IDeck
{
	Stack<ICard> Cards{get;}
	void Shuffle();
}