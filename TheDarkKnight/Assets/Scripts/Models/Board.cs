using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board {
	public Board() {

	}

	public int Columns = 0;
	public int Rows = 0;

	public bool validPos(Vector2 pos) {
		return (pos.x >= 0 && pos.x < Columns 
						&& pos.y >= 0 && pos.y < Rows);
	}

	protected List<Piece> pieces= new List<Piece>();
	public List<Piece> Pieces {
		get { return pieces; }
		set { pieces = value; }
	}


	public Batman getBatman() {
		foreach (Piece p in Pieces) {
			if(p.Type == Piece.PieceType.BATMAN)
				return p as Batman;
		}

		return null;
	}

	public Piece getPieceAt(Vector2 pos) {
		foreach (Piece p in Pieces) {
			if(p.Pos == pos)
				return p;
		}
		
		return null;
	}

	// Return NULL if no piece in space
	public Piece getPieceAt(int col, int row) {
		Vector2 pos = Piece.makePos(col,row);
		return getPieceAt(pos);
	}

	public void movePieceTo(Piece p, Vector2 pos) {
		movePieceTo(p, (int)pos.x, (int)pos.y);
	}

	public void movePieceTo(Piece p, int col, int row) {
		if(Pieces.Contains(p)) {
			Vector2 pos= Piece.makePos(col,row);
			if( validPos(pos) )
				p.Pos= pos;
			else
				Debug.Log (string.Format("Invalid Position Move Piece: {0}, from {1} to {2}",p.Type.ToString(), p.Pos.ToString(), pos.ToString()));
		}
	}

	public void removePiece(Piece p) {
		if(Pieces.Contains(p)) {
			Pieces.Remove(p);
		}
		else 
		{
			Debug.Log(string.Format("PieceRef {0} not in Board"));
		}
	}
}
