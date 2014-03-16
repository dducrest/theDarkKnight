using UnityEngine;
using System.Collections.Generic;

public class Rook : ActivePiece {
	
	public Rook(): base() {
		
	}
	
	public Rook(int col, int row): base(PieceType.ROOK, col, row) {
		
	}
	
	public override bool canMoveTo(Board board, int col, int row) {
		//count in four direction
		
		foreach(Vector2 pos in getValidMoves(board)) {
			if(pos.x == col && pos.y == row)
				return true;
		}
		
		return false;
	}
	

	
	public List<Vector2> getValidLeftMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
	
		for(int i= this.Col-1; i >=0; i--) {
			Vector2 move= Piece.makePos(i, this.Row);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
		}
		
		return validMoves;
	}
	
	public List<Vector2> getValidRightMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		for(int i= this.Col+1; i < board.Columns; i++) {
			Vector2 move= Piece.makePos(i, this.Row);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
		}
		
		return validMoves;
	}
	
	public List<Vector2> getValidDownMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		for(int i= this.Row-1; i >=0; i--) {
			Vector2 move= Piece.makePos(this.Col, i);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
		}
		
		return validMoves;
	}
	
	public List<Vector2> getValidUpMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		for(int i= this.Row+1; i < board.Rows; i++) {
			Vector2 move= Piece.makePos(this.Col, i);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
		}
		
		return validMoves;
	}
	

	public List<Vector2> getValidMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		validMoves.AddRange(getValidLeftMoves(board));
		validMoves.AddRange(getValidRightMoves(board));
		validMoves.AddRange(getValidUpMoves(board));
		validMoves.AddRange(getValidDownMoves(board));		
		
		return validMoves;
	}
}