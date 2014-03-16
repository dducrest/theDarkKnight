using UnityEngine;
using System.Collections.Generic;

/** 	\class DarkKnightController2
		\brief The enemies will chase you now.
*/
public class DarkKnightController2: DarkKnightControllerInterface  {

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
	
	
	protected ActionMenu actionMenu;
	public ActionMenu ActionMenu {
		get{ return actionMenu; }
		set { 
			if(actionMenu != null) {
				actionMenu.onAttackSelect-= onMove;
				actionMenu.onMoveSelect-= onMove;
			}
			
			actionMenu= value; 
			
			if(actionMenu != null) {
				actionMenu.onAttackSelect+= onMove;
				actionMenu.onMoveSelect+= onMove;
				actionMenu.onCancelAction+= onCancelAction;
			}
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
		Debug.Log("DarkKnightController2.onCellSelect");
	
		//if cell empty, then try to move batman
		//if cell full && not batman && can attack, then attack
		//else
		  // nothing
		  
		//toggle Menu
		Piece p = board.getPieceAt (col, row);	
		if(p!=null && p != board.getBatman()) {			//select a possible move for batman
			
			if(board.getBatman().canMoveTo(board, col, row)) {
				actionMenu.showActionMenu( Piece.makePos(col,row));
				actionMenu.ShowAttack= true;	
			}
			
			clearHighlights();
			p.Highlight= true;
		}
		else if(p!=null && p == board.getBatman()) {	//its batman		
			clearHighlights();
			p.Highlight= true;
		}
		else {
			if(board.getBatman().canMoveTo(board, col, row)) {
				actionMenu.showActionMenu( Piece.makePos(col,row) );
				actionMenu.ShowMove= true;	
			}
			else {
				actionMenu.hide();
			}
			clearHighlights();
		}
		
	}
	
	
	private void onMove(ActionMenu sender) {
		Debug.Log("DarkKnightController2.onMove");
		Piece p = board.getPieceAt (sender.SelectedCell);
		//Debug.Log ("onCellSelect - selected unit: " + p.Type.ToString());
		if(board.getBatman().canMoveTo(board, sender.SelectedCell)) {
			clearHighlights();
			board.movePieceTo( board.getBatman(), sender.SelectedCell);
			if(p != null) {
				
				//board.removePiece(p);
				if(p is ActivePiece)
					((ActivePiece)p).State= ActivePiece.ActivePieceState.DEAD;
				testWin();
			}
			
			actionMenu.hide();
					
			enemyAttack();
			testAlert();
		}
	}
	
	
	private void onCancelAction(ActionMenu sender) {
		actionMenu.hide();
		clearHighlights();
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
					if(batman != null && e.canMoveTo(board, batman.Pos)) { //If you can attack the batman, do so
						board.movePieceTo(e, batman.Pos);
						//board.removePiece(batman);
						//batman.State= Piece.ActivePieceState.Dead;
						if(onBatmanDead != null)
							onBatmanDead();
	
						Debug.Log("The Batman is Dead.");
					}
					else { //Attack where the Batman was
						if(e.canMoveTo(board, e.TargetPos))
							board.movePieceTo(e, e.TargetPos);
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
				if( e.State == ActivePiece.ActivePieceState.IDLE) {
					if(batman != null && e.canMoveTo(board, batman.Pos)) {   //Its the Bat, get 'im!!
						e.State= ActivePiece.ActivePieceState.ALERT;
						e.TargetPos= batman.Pos;
					}
				}
				else if(e.State == ActivePiece.ActivePieceState.ALERT) {   //Cannot see the Batman, go idle
						e.State= ActivePiece.ActivePieceState.IDLE;
				}
				//else if( e.State == ActivePiece.ActivePieceState.DEAD) { //stay dead
				//	;	
				//}
			}
		}
	}
	
	protected void clearHighlights() {
		foreach(Piece p in board.Pieces) {
			p.Highlight= false;
		}
	}


}
