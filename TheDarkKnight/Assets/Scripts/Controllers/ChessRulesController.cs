using UnityEngine;
using System.Collections;

public class ChessRulesController {

	
	public static void canMoveTo(Board board, Piece.PieceType type, Vector2 from, Vector2 to) {
		switch (type)
		{
			case Piece.PieceType.BATMAN:
			case Piece.PieceType.KNIGHT:
			break;
			
			 case Piece.PieceType.BISHOP:
			 break;
			 
			 case Piece.PieceType.KING: 
			 break;
			 
			 case Piece.PieceType.PAWN:
			 break;
			 
			case Piece.PieceType.QUEEN:
			break; 
		}
	}	
	
}
