using System;
using System.Collections.Generic;
using System.Text;
using PokerCoba.Class;
using PokerCoba.Enums;
using PokerCoba.Interface;
namespace PokerCoba;

public class Program 
{
	
	static void Main(string[] args)
	{
		Console.WriteLine("Welcome to Texas Hold'em Poker!");
		
		IPlayer player1 = new Player("Anjasss", 3000);
		IPlayer player2 = new Player("Kobisss", 3000);
		IPlayer player3 = new Player("Sikatt", 3000);
		PokerGame pokerGame = new();
		pokerGame.AddPlayer(player1);
		pokerGame.AddPlayer(player2);
		pokerGame.AddPlayer(player3);
		Console.WriteLine("Game started!");

		pokerGame.DealCards();
		
		
		BettingRoundHandler bettingRoundHandler = new();
		int pot = 0;
		int currentBet = 0;

        // Bet Initial Pertama
        	bettingRoundHandler.HandleInitialBettingRound(pokerGame, ref pot);
            Display.DisplayPlayerHand(pokerGame.Players[0]);
            Display.DisplayPlayerHand(pokerGame.Players[1]);
            Display.DisplayPlayerHand(pokerGame.Players[2]);

			
			pokerGame.DealCommunityCards(numCommunityCards: 3);
			Display.DisplayHandAndCommunityCards(pokerGame.CommunityCards);
			
			// Betting kedua nampilin community card dan display player hand
			bettingRoundHandler.HandleBettingRound(pokerGame.Players, pokerGame.CommunityCards, ref currentBet);

			pokerGame.DealCommunityCards(numCommunityCards: 1);
			
			
			// Bet ke 3
			bettingRoundHandler.HandleBettingRound(pokerGame.Players, pokerGame.CommunityCards, ref currentBet);

			pokerGame.DealCommunityCards(numCommunityCards: 1);
			
			// Betting terakhir
			bettingRoundHandler.HandleBettingRound(pokerGame.Players, pokerGame.CommunityCards,  ref currentBet);

			(IPlayer winner, CardType winningHand) = pokerGame.DetermineWinner();
			Console.WriteLine($"Player {winner.Name} wins {pot} chips with a {winningHand}!");
			
			Console.WriteLine("Game Over");
	}

   
}