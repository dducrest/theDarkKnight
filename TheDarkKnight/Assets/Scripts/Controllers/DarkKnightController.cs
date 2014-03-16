using UnityEngine;
using System.Collections.Generic;

public class DarkKnightController : DarkKnightControllerInterface {

	protected CellSelector selector;
	public CellSelector Selector {
		get{ return selector; }
		set{
			if(selector != null)
				selector.onCellSelect-= this.onCellSelect;

			selector= value;

			if(selector != null)
				selector.onCellSelect+= this.onCellSelect; 
		}
	}

	public void startGame() {
		testAlert();
	}
	
	public event DarkKnightControllerDel onBatmanWin;
	public event DarkKnightControllerDel onBatmanDead;

	protected Board board;
	public Board Board {
		get { return board; }
		set { board= value;}
	}
	
	public void onCellSelect( int col, int row) {
	
		//if cell empty, then try to move batman
		//if cell full && not batman && can attack, then attack
		//else
		  // nothing

		Piece p = board.getPieceAt (col, row);
			//Debug.Log ("onCellSelect - selected unit: " + p.Type.ToString());
			if(board.getBatman().canMoveTo(board, col,row)) {
				clearHighlights();
				board.movePieceTo( board.getBatman(), col, row);
				if(p != null) {
					board.removePiece(p);
					testWin();
				}
						
				enemyAttack();
				testAlert();
			}
			else {			
				bool highlight= p.Highlight;
				clearHighlights();
				p.Highlight= !highlight;
			}

	}
	
	protected void testWin() {
		if(board.Pieces.Count==1) {
			if(onBatmanWin != null)
				onBatmanWin();
		}
	}
	
	protected void enemyAttack() {
		List<Piece> pieces= new List<Piece>(board.Pieces);
		Batman batman= board.getBatman();
		foreach(Piece p in pieces) {
			if(p != batman && p is ActivePiece) {
				ActivePiece e= (ActivePiece)p;
				if(e.State == ActivePiece.ActivePieceState.ALERT) {
					if(batman != null && e.canMoveTo(board, batman.Pos)) {
						board.movePieceTo(e, batman.Pos);
						board.removePiece(batman);
						if(onBatmanDead != null)
							onBatmanDead();
	
						Debug.Log("The Batman is Dead.");
					}
				}
			}
		}
	}
	
	protected void testAlert() {
		Batman batman= board.getBatman();
		foreach(Piece p in board.Pieces) {
			if(p != batman && p is ActivePiece) {
				ActivePiece e= (ActivePiece)p;
				if(batman != null && e.canMoveTo(board, batman.Pos))
					e.State= ActivePiece.ActivePieceState.ALERT;
				else
					e.State= ActivePiece.ActivePieceState.IDLE;
			}
		}
	}
	
	protected void clearHighlights() {
		foreach(Piece p in board.Pieces) {
			p.Highlight= false;
		}
	}


}
