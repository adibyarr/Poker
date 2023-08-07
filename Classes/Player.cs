using System.Collections.Generic;
using System.Text;
using System.Linq;
using PokerCoba.Interface;
using PokerCoba.Enums;

namespace PokerCoba.Class;

public class Player : IPlayer
{

    public string Name{get; set;}
	public int Chips{get; set;}
	public int ChipsBet{get; set;}
	public IHand Hand{get;}
	public bool Folded{get;set;}
    
    public Player(string name, int initialChips)
	{
		Name = name;
		Chips = initialChips;
		Hand = new Hand();
		Folded = false;
		ChipsBet = 0;
	}
}
	
