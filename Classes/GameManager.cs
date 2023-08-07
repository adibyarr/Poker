using System;
using System.Collections.Generic;
using PokerCoba.Enums;
using PokerCoba.Interface;
using System.Xml.Serialization;
namespace PokerCoba.Class;
public class PokerGameManager
{
	public PokerGame PokerGame { get; }

	public PokerGameManager()
	{
		PokerGame = new PokerGame();
	}

	public void AddPlayer(IPlayer player)
	{
		PokerGame.AddPlayer(player);
	}

	public void StartGame()
	{
		Console.WriteLine("Welcome to Texas Hold'em Poker!");

		IPlayer player1 = new Player("Jacob", 1000);
		IPlayer player2 = new Player("Sialan", 1500);
		AddPlayer(player1);
		AddPlayer(player2);

		Console.WriteLine("Game started!");
		
		PokerGame.DealCards();
		PokerGame.DisplayPlayerHand();
		

		int pot = 0;
		PokerGame.BettingRound(currentBet: 0, ref pot);
		
		PokerGame.DealCommunityCards(numCommunityCards:3);
		PokerGame.DisplayHandAndCommunityCards();
		
		PokerGame.BettingRound(currentBet:0, ref pot);
		PokerGame.DealCommunityCards(numCommunityCards:1);
		
		PokerGame.BettingRound(currentBet:0, ref pot);
		PokerGame.DealCommunityCards(numCommunityCards:1);
		
		PokerGame.BettingRound(currentBet: 0, ref pot);
		
		var(winner, WinningHand) = PokerGame.DetermineWinner();
		
;
		Console.WriteLine($"Player {winner.Name} wins {pot} with {WinningHand}");
		
		Console.WriteLine("Game Over");
	}

	
	
}
