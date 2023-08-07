namespace PokerCoba.Interface;

public interface IHand
{
	List<ICard> Cards{get;}
	void AddCard(ICard card);

}