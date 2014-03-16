using UnityEngine;
using System.Collections.Generic;

public class Batman: Knight {
	
	public Batman(): base() {
		
	}
	
	public Batman(int col, int row): base(col, row) {
		Type= PieceType.BATMAN;
	}
	
	
	public override bool canMoveTo(Board board, int col, int row) {
		List<Vector2> validMoves= this.getValidMoves(board);
			validMoves.AddRange(getValidSpecialMoves(board));
		
		Vector2 newMove= Piece.makePos(col,row);
		
		foreach( Vector2 valid in validMoves) {
			if(valid.x == newMove.x && valid.y == newMove.y)
			{
				return board.validPos(newMove);
			}
		}
		return false;
	}
	
	public List<Vector2> getValidSpecialMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		foreach(Piece p in board.Pieces) {
			if(p != this && p.canMoveTo(board, Col,Row))
			{
				validMoves.Add(p.Pos);			
			}
		}
		
		return validMoves;
	}
	
	protected override bool canBeInCell(Board board, Vector2 pos) {
		return true;
	}
}
