using PokerCoba.Interface;
using PokerCoba.Enums;

namespace PokerCoba.Class;
public class BettingRoundHandler
{
	public void HandleInitialBettingRound(PokerGame pokerGame, ref int pot)
	{
		int currentBet = 0;
		foreach(IPlayer player in pokerGame.Players)
		{
			if(player.Folded) continue;
			 Console.WriteLine($"{player.Name}, it's your turn.");
				Console.WriteLine($"Current bet: {currentBet}");
				Console.WriteLine($"Your chips: {player.Chips}");
				Console.WriteLine("Do you want to bet (b) or fold (f)?");
				string input = Console.ReadLine().ToLower();

				if (input == "b")
				{
					Console.Write("Enter your bet: ");
					if (int.TryParse(Console.ReadLine(), out int betAmount))
					{
						if (betAmount > player.Chips)
						{
							Console.WriteLine("Not enough chips.");
							player.Folded = true;
						}
						else
						{
							player.Chips -= betAmount;
							pot += betAmount;
							currentBet = betAmount;
						}
					}
					else
					{
						Console.WriteLine("Invalid input.");
						player.Folded = true;
					}
				}
				else if (input == "f")
				{
					player.Folded = true;
					Console.WriteLine($"{player.Name} folds.");
				}
				else
				{
					Console.WriteLine("Invalid choice. You are considered folded.");
					player.Folded = true;
				}
		}
	}
	 public void HandleBettingRound(List<IPlayer> players, List<ICard> communityCards, ref int pot)
		{
			foreach (IPlayer player in players)
			{
				if (player.Folded) continue;

				Console.WriteLine($"\n{player.Name}, it's your turn.");
				Console.WriteLine($"Current pot: {pot}");
				Console.WriteLine($"Your chips: {player.Chips}\n");
				Console.WriteLine("\nDo you want to bet (b), check (c), or fold (f) or all-in(a)?");
				string input = Console.ReadLine().ToLower();

				if (input == "b")
				{
					Console.Write("Enter your bet: ");
					if (int.TryParse(Console.ReadLine(), out int betAmount))
					{
						if (betAmount > player.Chips)
						{
							Console.WriteLine("Not enough chips. You are considered folded.");
							player.Folded = true;
						}
						else
						{
							player.Chips -= betAmount;
							pot += betAmount;
							player.ChipsBet += betAmount;
						}
					}
					else
					{
						Console.WriteLine("Invalid input. You are considered folded.");
						player.Folded = true;
					}
				}
				else if (input == "c")
				{
					Console.WriteLine($"{player.Name} checks.");
				}
				else if (input == "f")
				{
					player.Folded = true;
					Console.WriteLine($"{player.Name} folds.");
				}
				else if(input =="a")
				{
					if(player.Chips > 0)
					{
						pot += player.Chips;
						player.ChipsBet += player.Chips;
						player.Chips = 0;
						Console.WriteLine($"{player.Name} mau all-in");
					}
					else
					{
						Console.WriteLine("Chips gaada buat all-in");
					}
				}
				else
				{
					Console.WriteLine("Invalid choice. You are considered folded.");
					player.Folded = true;
				}
			}
	
		}
}