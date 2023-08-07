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
		
		foreach(var player in Players)
		{
			player.Hand.AddCard(deck.Draw());
			player.Hand.AddCard(deck.Draw());
		}
	}
	public void DealCommunityCards(int numCommunityCards)
	{
		deck.Shuffle();
		
		for(int i = 0; i < numCommunityCards; i++)
		{
			ICard communityCards = deck.Draw();
			CommunityCards.Add(communityCards);
		}
	}
	public void BettingRound(int currentBet, ref int pot)
	{
		foreach(IPlayer player in Players)
		{
			if(player.Folded) continue;
			PlayerAction(player, currentBet, ref pot);
		}
	}
	 public void PlayerAction(IPlayer player,int currentBet, ref int pot)
	{
		Console.WriteLine($"Player {player.Name}, ini giliran muu");
		Console.WriteLine($"Chips yang kamu punya: {player.Chips}");
		Console.WriteLine($"Bet sekarang: {currentBet}");
		Console.WriteLine("Masukkan aksimu: (fold, raise, call): ");
		string input = Console.ReadLine().ToLower();

		switch (input)
		
		{
			case "fold":
				player.Folded = true;
				Console.WriteLine($"Player: {player.Name} folds.");
				break;
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
			default:
				Console.WriteLine("Invalid action. Please choose 'fold', 'raise', or 'call'.");
				break;
				
		}
	}
	public IPlayer DetermineWinner()
	
	{
		List<IPlayer> winner = Players.FindAll(p => !p.Folded);
		HandEvaluator evaluator = new HandEvaluator();
		
		
		foreach(var player in Players)
		{
			var allCards = new List<ICard>(player.Hand.Cards);
            allCards.AddRange(CommunityCards);

                CardType playerHandType = HandEvaluator.EvaluateHand(allCards.Select(card => card.ToString()).ToArray());
                CardType winnerHandType = handEvaluator.EvaluateHand(winner.Hand.Cards.Select(card => card.ToString()).ToArray());

                if (playerHandType > winnerHandType)
                {
                    winner = player;
                }
                else if (playerHandType == winnerHandType)
                {
                    // If two players have the same hand type, compare the highest-ranking card of the hands
                    if (CompareHighestCard(player.Hand.Cards, winner.Hand.Cards) > 0)
                    {
                        winner = player;
                    }
                }
		}
		int totalChipsPot = CommunityPot;
		winner.Chips += totalChipsPot;
		return winner; 
	}
	public void DisplayHandAndCommunityCards()
	{
		Console.WriteLine("Community Card: ");
		foreach(var card in CommunityCards)
		{
			Console.WriteLine(card + " ");
		}
		Console.WriteLine();
		foreach(var player in Players)
		{
			Console.WriteLine($"Player {player.Name} hands:");
			foreach(var card in player.Hand.Cards)
			{
				Console.Write(card + " ");
			}
			Console.WriteLine();
		}
	}
}