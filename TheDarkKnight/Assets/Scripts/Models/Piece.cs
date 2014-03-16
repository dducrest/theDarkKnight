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

public class Enemy: Piece {
	
	protected Enemy(): base() {
		
	}
	
	protected Enemy( PieceType type, int col, int row, EnemyState state= EnemyState.IDLE): base(type, col, row) {
		State = state;
	}

	public enum EnemyState { IDLE, ALERT };
	public EnemyState State {
		get { return state; }
		set { state = value; }
	}
	protected EnemyState state= EnemyState.IDLE;
	
	public Vector2 TargetPos {
		get { return targetPos; }
		set { targetPos = value; }
	}
	protected Vector2 targetPos;
	
	protected override bool canBeInCell(Board board, Vector2 pos) {
		Piece p= board.getPieceAt(pos);
		
		return (p == this || p == board.getBatman() || p == null );
	}
	
}

