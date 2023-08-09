using System;
using System.Collections.Generic;
using PokerCoba.Enums;
using PokerCoba.Interface;
using System.Xml.Serialization;
namespace PokerCoba.Class;
public class PokerGameManager
{
	public PokerGame pokerGame { get; }
	private readonly PlayerActionHandler actionHandler;
	private readonly GameMechanic gameMechanic;
	private readonly BettingRoundHandler bettingRoundHandler;

	public PokerGameManager()
	{
		pokerGame = new PokerGame();
		actionHandler = new PlayerActionHandler();
		gameMechanic = new GameMechanic();
		bettingRoundHandler = new BettingRoundHandler();
	}

	public void StartGame()
	{
		Console.WriteLine("Welcome to Texas Hold'em Poker!");
		
		IPlayer player1 = new Player("Anjasss", 3000);
		IPlayer player2 = new Player("Kobisss", 3000);
		IPlayer player3 = new Player("Sikatt", 3000);
		pokerGame.AddPlayer(player1);
		pokerGame.AddPlayer(player2);
		pokerGame.AddPlayer(player3);
		Console.WriteLine("Game started!");

		pokerGame.DealCards();
		
		

		int pot = 0;
		int currentBet = 0;
		
			// Initial betting round
			bettingRoundHandler.HandleInitialBettingRound(pokerGame, ref pot);
			Display.DisplayPlayerHand(pokerGame.Players[0]);
			Display.DisplayPlayerHand(pokerGame.Players[1]);
			Display.DisplayPlayerHand(pokerGame.Players[2]);
			
			pokerGame.DealCommunityCards(numCommunityCards: 3);
			Display.DisplayHandAndCommunityCards(pokerGame.CommunityCards);
			
			// Second betting round
			bettingRoundHandler.HandleBettingRound(pokerGame.Players, pokerGame.CommunityCards, ref currentBet);

			pokerGame.DealCommunityCards(numCommunityCards: 1);
			
			
			// Third betting round
			bettingRoundHandler.HandleBettingRound(pokerGame.Players, pokerGame.CommunityCards, ref currentBet);

			pokerGame.DealCommunityCards(numCommunityCards: 1);
			
			// Final betting round
			bettingRoundHandler.HandleBettingRound(pokerGame.Players, pokerGame.CommunityCards,  ref currentBet);

			(IPlayer winner, CardType winningHand) = pokerGame.DetermineWinner();
			Console.WriteLine($"Player {winner.Name} wins {pot} chips with a {winningHand}!");
			Console.WriteLine("Game Over");
	 }

		
	
}

