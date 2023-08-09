using PokerCoba.Enums;
using PokerCoba.Interface;
using System; 
using System.Collections.Generic;

namespace PokerCoba.Class;

public class GameMechanic
{
	public static void PlaceFirstBet(List<IPlayer> players, int initialBet, ref int communityPot)
	{
		foreach(IPlayer player in players)
		{
			if(!player.Folded)
			{
				player.Chips -= initialBet;
				communityPot += initialBet;
				player.ChipsBet += initialBet; 
			}
		}
	}
	public static void Raise(IPlayer player, int raiseAmount, ref int currentBet, ref int pot)
	{
		if(raiseAmount <= player.Chips)
		{
			player.ChipsBet -= raiseAmount;
			pot += raiseAmount;
			currentBet = raiseAmount;
			player.ChipsBet += raiseAmount; 
		}
		else
		{
			Console.WriteLine("Chip Ga cukup buat raise");
		}
	}
	public static void Call(IPlayer player, int currentBet, ref int pot)
	{
		int callAmount = currentBet - player.ChipsBet;
		if(player.Chips >= callAmount)
		{
			player.Chips -= callAmount;
			pot += callAmount;
			player.ChipsBet += callAmount; 
		}
		else
		{
			Console.WriteLine("Chips Ga cukup buat call");
		}
	}
	public static void Fold(IPlayer player)
	{
		player.Folded = true;
		Console.WriteLine($"Player {player.Name} folds");
	}
	public static void AllIn(IPlayer player, ref int pot)
	{
		if(player.Chips > 0)
		{
			pot += player.Chips;
			player.ChipsBet += player.Chips;
			player.Chips = 0;
			Console.WriteLine($"Player {player.Name} goes all in");
		}
		else
		{
			Console.WriteLine("Chips ga ada buat all-in");
		}
	}
}