using PokerCoba.Interface;
namespace PokerCoba.Class;
public class Hand : IHand
{
	public List<ICard> Cards {get;set;}
	public Hand()
	{
		Cards = new List<ICard>();
	}
	public void AddCard(ICard card)
	{
		Cards.Add(card);
	}

    internal int CompareTo(Hand bestHand)
    {
        throw new NotImplementedException();
    }
}