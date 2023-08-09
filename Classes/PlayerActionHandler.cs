using PokerCoba.Enums;
using PokerCoba.Interface;

namespace PokerCoba.Class;
public class PlayerActionHandler
{
	public void PerformAction(IPlayer player, ref int currentBet, ref int pot)
	{
		Console.WriteLine($"Player {player.Name}, ini giliran muu");
		Console.WriteLine($"Chips yang kamu punya: {player.Chips}");
		Console.WriteLine($"Bet sekarang: {currentBet}");
		Console.WriteLine("Masukkan aksimu: (fold, raise, call, all-in): ");
		string? input = Console.ReadLine().ToLower();
		switch (input)
		{
			case "fold":
				player.Folded = true;
				Console.WriteLine($"Player: {player.Name} folds.");
				break;
				
			// 
			case "raise":
				Console.Write("Masukkan taruhanmu: ");
				string raiseInput = Console.ReadLine();
				if(int.TryParse(raiseInput, out int raiseAmount))
				{
					if(raiseAmount <= player.Chips)
					{
						player.Chips -= raiseAmount;
						pot += raiseAmount;
						currentBet = raiseAmount;
						player.ChipsBet += raiseAmount;
						Console.WriteLine($"Player {player.Name} raise dari {raiseAmount} chips.");
					}
					else
					{
						Console.WriteLine("Invalid input. ");
					}				
				}
				else
				{
					Console.WriteLine("INvalid input");
				}
				break;
			case "call":
				int callAmount = currentBet - player.ChipsBet;
				if(player.Chips >= callAmount)
				{
					player.Chips -= callAmount;
					pot += callAmount;
					player.ChipsBet += callAmount;
					Console.WriteLine($"Player {player.Name} call {callAmount} chips"); 
				}
				else
				{
					Console.WriteLine("Chips ga cukup");
				}
				break;
			case "all-in":
					if (player.Chips > 0)
					{
						pot += player.Chips;
						player.ChipsBet += player.Chips;
						player.Chips = 0;
						Console.WriteLine($"Player {player.Name} goes all-in!");
					}
					else
					{
						Console.WriteLine("You don't have any chips left to go all-in.");
					}
					break;
			default:
					Console.WriteLine("Invalid action. Please choose 'fold', 'raise', 'call', or 'all-in'.");
					break;
		}
	}
}
