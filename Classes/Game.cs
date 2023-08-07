using System.Collections.Generic;
using System.Linq;
using PokerCoba.Enums;
using PokerCoba.Interface;

namespace PokerCoba.Class;

public class PokerGame
{
	
	public List<IPlayer> Players { get; }
	public List<ICard> CommunityCards { get; }
	public int CommunityPot { get; private set; }
	private readonly Deck deck;
	public PokerGame()
	{

		Players = new List<IPlayer>();
		CommunityCards = new List<ICard>();
		CommunityPot = 0;
	}

	public void AddPlayer(IPlayer player)
	{
		Players.Add(player);
	}
	public void DealCards()
	{
		Deck deck = new Deck();
		deck.Shuffle();
		if(Players.Count * 2 > deck.Cards.Count)
		{
			Console.WriteLine("Kartu tidak cukup");
			return;
		}
		
		foreach(var player in Players)
		{
			player.Hand.AddCard(deck.Draw());
			player.Hand.AddCard(deck.Draw());
		}
	}
	public void DealCommunityCards(int numCommunityCards)
	{
		Deck deck = new Deck();
		deck.Shuffle();
		
		for(int i = 0; i < numCommunityCards; i++)
		{
			ICard communityCards = deck.Draw();
			CommunityCards.Add(communityCards);
		}
	}
	
	 public void PlayerAction(IPlayer player, ref int currentBet, ref int pot)
	{
		Console.WriteLine($"Player {player.Name}, ini giliran muu");
		Console.WriteLine($"Chips yang kamu punya: {player.Chips}");
		Console.WriteLine($"Bet sekarang: {currentBet}");
		Console.WriteLine("Masukkan aksimu: (fold, raise, call, all-in): ");
		string input = Console.ReadLine().ToLower();

		switch (input)
		{
			case "fold":
				player.Folded = true;
				Console.WriteLine($"Player: {player.Name} folds.");
				break;
				
			// 
			case "raise":
				Console.Write("Masukkan taruhanmu: ");
				if(int.TryParse(input, out int raiseAmount))
				{
					if(raiseAmount <= player.Chips)
					{
						player.Chips -= raiseAmount;
						pot += raiseAmount;
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
	public void BettingRound(int currentBet, ref int pot)
	{
		foreach(IPlayer player in Players)
		{
			if(player.Folded) continue;
			
			PlayerAction(player, ref currentBet, ref pot);
			
			Console.WriteLine($"Current bet: {currentBet}");
			Console.WriteLine($"Player chips: {player.Chips}");
			Console.WriteLine("Masukkan taruhanmu: ");
			if(int.TryParse(Console.ReadLine(), out int betAmount))
			{
				if(betAmount > player.Chips)
				{
					Console.WriteLine("Chips mu gacukup");
					continue;
				}
				if(betAmount > currentBet)
				{
					currentBet = betAmount;
					pot += currentBet;
					player.Chips -= currentBet;
					player.ChipsBet += currentBet;
				}
				else if (betAmount == currentBet)
				{
					pot += currentBet;
					player.Chips -= currentBet;
					player.ChipsBet += currentBet;
				
				}
				else
				{
					Console.WriteLine("Taruhan mu harus lebih atau sama ");
					continue;
				}
				Console.WriteLine($"Player {player.Name} bet {betAmount} chips");
			}
			else
			{
					Console.WriteLine("Invalid input");
					continue;
			}
			
		}
		
	}
	public(IPlayer winner, CardType WinningHand)  DetermineWinner()
	
	{
		IPlayer winner = Players[0];
		HandEvaluator evaluator = new HandEvaluator();
		CardType WinningHand = CardType.HighCard;
		
		
		foreach(var player in Players)
		{
			var allCards = new List<ICard>(player.Hand.Cards);
			allCards.AddRange(CommunityCards);

			CardType playerHandType = evaluator.EvaluateHand(allCards); 
			if(playerHandType > WinningHand)
			{
				winner = player;
				WinningHand = playerHandType;
			}
		}
		int totalChipsPot = CommunityPot;
		winner.Chips += totalChipsPot;
		return (winner, WinningHand); 
	}
	public void DisplayHandAndCommunityCards()
	{
		Console.WriteLine("Community Card: ");
		foreach(var card in CommunityCards)
		{
			Console.WriteLine(card.ToString());
		}
		Console.WriteLine("\nPlayers hands: ");
		foreach(var player in Players)
		{
			Console.WriteLine($"Player {player.Name} hands:");
			foreach(var card in player.Hand.Cards)
			{
				Console.Write(card.ToString());
			}
			Console.WriteLine();
		}
	}
	public void DisplayPlayerHand()
	{
		
		foreach (var player in Players)
		{
			Console.WriteLine($"{player.Name} hands: ");
			foreach(var card in player.Hand.Cards)
			{
				Console.WriteLine(card.ToString());
			}
			Console.WriteLine(player);
		}
	}
}