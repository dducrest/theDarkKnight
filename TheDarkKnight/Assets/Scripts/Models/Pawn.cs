using UnityEngine;
using System.Collections.Generic;

public class Pawn : Enemy {
	
	public Pawn(): base() {
		
	}
	
	public Pawn(int col, int row, PawnDirection direction= PawnDirection.UP): base(PieceType.PAWN, col, row) {
		Direction= direction;
	}
	
	public enum PawnDirection { UP, DOWN } ; //UP IS INCREASING IN THE ROW #
	public PawnDirection Direction {
		get { return direction;}
		set { direction= value;}
	}
	protected PawnDirection direction= PawnDirection.UP;
	
	
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
		
		if( direction == PawnDirection.UP)
		{
			Vector2 pos= new Vector2(this.Col-1, this.Row+1);	
			if(this.canBeInCell(board,pos))
				validMoves.Add(pos);
				
			Vector2 pos2= new Vector2(this.Col+1, this.Row+1);	
			if(this.canBeInCell(board,pos2))
				validMoves.Add(pos2);
		}	
		else if( direction == PawnDirection.DOWN)
		{
			Vector2 pos= new Vector2(this.Col-1, this.Row-1);	
			if(this.canBeInCell(board,pos))
				validMoves.Add(pos);
			
			Vector2 pos2= new Vector2(this.Col+1, this.Row-1);	
			if(this.canBeInCell(board,pos2))
				validMoves.Add(pos2);
		}
	
		return validMoves;
	}
}