using UnityEngine;
using System.Collections;

public delegate void DarkKnightControllerDel ();

public interface DarkKnightControllerInterface {

	CellSelector Selector { get; set; }
	
	void startGame() ;
	
	
	event DarkKnightControllerDel onBatmanWin;
	event DarkKnightControllerDel onBatmanDead;
	
	Board Board { get; set; }
}
