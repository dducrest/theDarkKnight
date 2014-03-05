﻿using UnityEngine;
using System.Collections.Generic;

public class Bishop : Enemy {
	
	public Bishop(): base() {
		
	}
	
	public Bishop(int col, int row): base(PieceType.BISHOP, col, row) {
		
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
		
		validMoves.AddRange(getValidUpLeftMoves(board));
		validMoves.AddRange(getValidUpRightMoves(board));
		validMoves.AddRange(getValidDownLeftMoves(board));
		validMoves.AddRange(getValidDownRightMoves(board));		
				
		return validMoves;
	}
	
	public List<Vector2> getValidUpLeftMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		int x= Col-1;
		int y= Row-1;
		while( x>=0 && y>=0)
		{
			Vector2 move= Piece.makePos(x, y);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
			
			x--;
			y--;
		}
		
		return validMoves;
	}
	
	public List<Vector2> getValidUpRightMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		int x= Col-1;
		int y= Row+1;
		while( x>=0 && y< board.Rows)
		{
			Vector2 move= Piece.makePos(x, y);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
			
			x--;
			y++;
		}
		
		return validMoves;
	}
	
	public List<Vector2> getValidDownLeftMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		int x= Col+1;
		int y= Row-1;
		while( x< board.Columns && y>=0)
		{
			Vector2 move= Piece.makePos(x, y);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
			
			x++;
			y--;
		}
		
		return validMoves;
	}
	
	public List<Vector2> getValidDownRightMoves(Board board) {
		List<Vector2> validMoves= new List<Vector2>();
		
		int x= Col+1;
		int y= Row+1;
		while(x< board.Columns && y < board.Rows)
		{
			Vector2 move= Piece.makePos(x, y);
			if( this.canBeInCell(board, move) )
				validMoves.Add(move);
			else 
				break;
			
			x++;
			y++;
		}
		
		return validMoves;
	}
	
	
}