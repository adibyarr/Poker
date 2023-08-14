using PokerCoba.Enums;
using PokerCoba.Interface;

namespace PokerCoba.Class;

public class Display

{
	public static void DisplayHandAndCommunityCards(List<ICard> communityCards)
	{
		Console.WriteLine("\nCommunity cards: ");
		foreach(var card in communityCards)
		{
			Console.WriteLine($"{card}");
		}
		Console.WriteLine();
	}
	public static void DisplayPlayerHand(IPlayer player)
	{
		Console.WriteLine($"\n{player.Name} hands: ");
		foreach(var card in player.Hand.Cards)
		{
			Console.WriteLine(card.ToString());
		}
		
	}
}