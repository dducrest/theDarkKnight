using UnityEngine;
using System.Collections.Generic;

public class Knight : Enemy {

	public Knight(): base() {
		
	}
	
	public Knight(int col, int row): base(PieceType.KNIGHT, col, row) {
		
	}
	
	public override bool canMoveTo(Board board, int col, int row) {
		List<Vector2> validMoves= this.getValidMoves(board);
		
		Vector2 newMove= Piece.makePos(col,row);
		
		foreach( Vector2 valid in validMoves) {
			if(valid.x == newMove.x && valid.y == newMove.y)
			{
				return board.validPos(newMove);
			}
		}
		return false;
	}

	public List<Vector2> getValidMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		validMoves.Add( new Vector2( Pos.x -2, Pos.y-1)); //Left Up
		validMoves.Add( new Vector2( Pos.x -2, Pos.y+1)); //Left Down
		validMoves.Add( new Vector2( Pos.x +2, Pos.y-1)); //Right Up
		validMoves.Add( new Vector2( Pos.x +2, Pos.y+1)); //Right Down
		
		validMoves.Add( new Vector2( Pos.x -1, Pos.y+2)); //Down Left
		validMoves.Add( new Vector2( Pos.x +1, Pos.y+2)); //Down Right
		validMoves.Add( new Vector2( Pos.x -1, Pos.y-2)); //Up Left
		validMoves.Add( new Vector2( Pos.x +1, Pos.y-2)); //Up Right
		
		List<Vector2> rc= new List<Vector2>();
		foreach (Vector2 move in validMoves)
		{
			if(this.canBeInCell(board, move))
				rc.Add(move);
		}
		
		return rc;
	}
}
