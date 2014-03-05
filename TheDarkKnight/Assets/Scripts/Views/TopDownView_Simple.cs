using UnityEngine;
using System.Collections;

public class TopDownView_Simple : MonoBehaviour, CellSelector, BoardToScreen {

	public Board board;

	// Use this for initialization
	void Start () {
	
	}


	void OnGUI() {
		for (int col=0; col< board.Columns; col++) {
			for(int row=0; row< board.Rows; row++) {
				Piece p= board.getPieceAt(col, row);
				if(p == null) {
					drawButton( col,  row, "");
				}
				else {
					if ( p is Enemy ) {
						Enemy e= (Enemy)p;
						if(e.State == Enemy.EnemyState.ALERT) 
							drawButton( col,  row, p.Type.ToString() + "\nALERTED");
						else if(e.State == Enemy.EnemyState.IDLE)
							drawButton( col,  row, p.Type.ToString());
					}
					else {
						drawButton( col,  row, p.Type.ToString());
					}
					
					if(p.Highlight) {
						drawHighlightFor(p);					
					}
				}
			}
		}
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
