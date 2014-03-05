using UnityEngine;
using System.Collections.Generic;

public class King : Enemy {
	
	public King(): base() {
		
	}
	
	public King(int col, int row): base(PieceType.KING, col, row) {
		
	}
	
	
	public override bool canMoveTo(Board board, int col, int row) {
		//count in four direction
		
		foreach(Vector2 pos in getValidMoves(board)) {
			if(pos.x == col && pos.y == row)
				return true;
		}
		
		return false;
	}
	
	
	public List<Vector2> getValidMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		
		validMoves.Add( new Vector2( Pos.x -1, Pos.y-1)); //Left Up
		validMoves.Add( new Vector2( Pos.x -1, Pos.y+1)); //Left Down
		validMoves.Add( new Vector2( Pos.x +1, Pos.y-1)); //Right Up
		validMoves.Add( new Vector2( Pos.x +1, Pos.y+1)); //Right Down
		validMoves.Add( new Vector2( Pos.x +1, Pos.y)); //Right
		validMoves.Add( new Vector2( Pos.x, Pos.y-1)); //Down
		validMoves.Add( new Vector2( Pos.x -1, Pos.y)); //Left
		validMoves.Add( new Vector2( Pos.x , Pos.y+1)); //Up
		
		List<Vector2> rc= new List<Vector2>();
		foreach (Vector2 move in validMoves)
		{
			if(this.canBeInCell(board, move))
				rc.Add(move);
		}
		
		return validMoves;
	}
}