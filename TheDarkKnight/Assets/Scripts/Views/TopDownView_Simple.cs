using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopDownView_Simple : MonoBehaviour, iBoardView{

	protected Board board;
	public Board Board { 
		get { return board; }
		set { 
			board= value;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	public void updateView() {
	
	}

	public int GUILevelOffset=5;
	void OnGUI() {
		int depth= GUI.depth;
		GUI.depth= GUILevelOffset;
		for (int col=0; col< board.Columns; col++) {
			for(int row=0; row< board.Rows; row++) {
				bool drawAPiece= false;
				foreach(Piece p in board.getPiecesAt(col, row) ) {
					drawAPiece=true;
					if ( p is ActivePiece ) {
						ActivePiece e= (ActivePiece)p;
						if(e.State == ActivePiece.ActivePieceState.ALERT) 
							drawButton( col,  row, p.Type.ToString() + "\nALERTED");
						else if(e.State == ActivePiece.ActivePieceState.IDLE)
							drawButton( col,  row, p.Type.ToString());
						else if(e.State == ActivePiece.ActivePieceState.DEAD)
							drawButton( col,  row, "");
					}
					else {
						drawButton( col,  row, p.Type.ToString());
					}
					
					if(p.Highlight) {
						drawHighlightFor(p);					
					}
				}
				
				if(!drawAPiece) {
					drawButton( col,  row, "");
				}
			}
		}
		GUI.depth= depth;
	}

	public int getCellSize() {
		int cellSize = Screen.height / board.Rows;
		if (cellSize > Screen.width / board.Columns)
			cellSize = Screen.width / board.Columns;
		
		return cellSize;
	}
	
	public Vector2 convertCellToScreen(Vector2 cell) {
		Vector2 rc;
		float cellSize= getCellSize();
		rc.x= cell.x  * cellSize;
		rc.y= cell.y  * cellSize;
		
		return rc;
	}
	

	void drawButton(int col, int row, string val) {
		int cellSize= getCellSize();
	
		if (GUI.Button (new Rect (col * cellSize, row * cellSize, cellSize, cellSize), val)) {
			if(onCellSelect != null)
				onCellSelect(col, row);
		}
	}
	
	void drawHighlight(int col, int row, string color) {
		int cellSize= getCellSize();
		//Debug.Log(string.Format("drawHighlight {0},{1}",col, row));
		GUI.Box (new Rect (col * cellSize, row * cellSize, cellSize, cellSize), "" );
	}
	
	void drawHighlightFor(Piece p) {		
		for(int col =0; col < board.Columns; col++) {
			for(int row=0; row < board.Rows; row++) {
				if(p.canMoveTo(board, col,row))
					drawHighlight(col,row,"x");
			}					
		}
	}
	
	public event CellSelect onCellSelect;


	void Update() {

	}

}
