using UnityEngine;
using System.Collections.Generic;

public class Piece {

	public Piece() {

	}

	public Piece( PieceType type, int col, int row) {
		Type = type;
		Pos = makePos (col, row);
	}

	public static Vector2 makePos( int col, int row) {
		return new Vector2 (col, row);
	}
	
	Vector2 pos= new Vector2();
	public Vector2 Pos {
		get { return pos; }
		set { pos= value; }
	}
	
	bool highlight= false;
	public bool Highlight {
		get { return highlight; }
		set { highlight= value; }
	}
	
	public int Col {
		get { return (int)Pos.x; }
	}
	
	public int Row {
		get { return (int)Pos.y; }
	}

	public enum PieceType { BATMAN, KNIGHT, KING, QUEEN, ROOK, BISHOP, PAWN } ;
	protected PieceType type;
	public PieceType Type {
		get { return type; }
		set { type= value; }
	}
	
	public bool canMoveTo(Board board, Vector2 pos)  { return canMoveTo(board, (int)pos.x, (int)pos.y); }
	public virtual bool canMoveTo(Board board, int col, int row)  { return false; }
	protected virtual bool canBeInCell(Board board, Vector2 pos) { 
		Piece p= board.getPieceAt(pos);	
		return (p == this || p == null );
	}
}

public class ActivePiece: Piece {
	
	protected ActivePiece(): base() {
		
	}
	
	protected ActivePiece( PieceType type, int col, int row, ActivePieceState state= ActivePieceState.IDLE): base(type, col, row) {
		State = state;
	}

	public enum ActivePieceState { IDLE, ALERT, DEAD };
	public ActivePieceState State {
		get { return state; }
		set { state = value;
		//	Debug.Log (this.Type.ToString() + "Change State to: " + state.ToString());
		}
	}
	protected ActivePieceState state= ActivePieceState.IDLE;
	
	public Vector2 TargetPos {
		get { return targetPos; }
		set { targetPos = value; }
	}
	protected Vector2 targetPos;
	
	protected override bool canBeInCell(Board board, Vector2 pos) {
		Piece p= board.getPieceAt(pos);
		if(p is ActivePiece)
			return (p == this || ((ActivePiece)p).State == ActivePiece.ActivePieceState.DEAD || p == board.getBatman() || p == null );
		else 
			return true;
	}
	
}

