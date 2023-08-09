using PokerCoba.Enums;
using PokerCoba.Interface;

namespace PokerCoba.Class;

public class Display

{
	public static void DisplayHandAndCommunityCards(List<ICard> communityCards)
	{
		Console.WriteLine("Community cards: ");
		for(int i = 0; i < Math.Min(3, communityCards.Count); i++)
		{
			Console.WriteLine(communityCards[i].ToString());
		}
		Console.WriteLine();
	}
	public static void DisplayPlayerHand(IPlayer player)
	{
		Console.WriteLine($"{player.Name} hands: ");
		foreach(var card in player.Hand.Cards)
		{
			Console.WriteLine(card.ToString());
		}
		Console.WriteLine(player);
	}
}