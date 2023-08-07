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
		PokerGameManager pokerGameManager = new PokerGameManager();
		pokerGameManager.StartGame();
	}

   
}