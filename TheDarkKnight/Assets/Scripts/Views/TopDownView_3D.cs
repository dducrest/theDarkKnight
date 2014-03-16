using UnityEngine;
using System.Collections.Generic;

public class TopDownView_3D : MonoBehaviour, iBoardView {
	
	protected Board board;
	public Board Board { 
		get { return board; }
		set { 
			if(board != value ) {
				board= value; 
				init();
			}
		}
	}
	
	//Prefabs
	public GameObject boardTile= null;
	public GameObject pawn= null;
	public GameObject rook= null;
	public GameObject knight= null;
	public GameObject bishop= null;
	public GameObject queen= null;
	public GameObject king= null;
	public GameObject batman= null;
	protected Dictionary< Piece.PieceType, GameObject> piecePrefabs= null;
	
	//Game Space
	private Dictionary<Vector2, GameObject> tileObjects= new Dictionary<Vector2, GameObject>();
	private Dictionary<Piece, GameObject> pieceObjects= new Dictionary<Piece, GameObject>();
	
	//Events
	public event CellSelect onCellSelect;
	
	// Use this for initialization
	void Start () {
		populatePiecePrefabs();
	}
	
	//Part of Initialization - fill the piecePrefab dictionary to more easily find prefabs by type
	void populatePiecePrefabs() {
		piecePrefabs= new Dictionary<Piece.PieceType, GameObject>();
		piecePrefabs[Piece.PieceType.BATMAN]= batman;
		piecePrefabs[Piece.PieceType.KING]= king;
		piecePrefabs[Piece.PieceType.KNIGHT]= knight;
		piecePrefabs[Piece.PieceType.QUEEN]= knight;
		piecePrefabs[Piece.PieceType.BISHOP]= bishop;
		piecePrefabs[Piece.PieceType.PAWN]= pawn;
		piecePrefabs[Piece.PieceType.ROOK]= rook;
		piecePrefabs[Piece.PieceType.QUEEN]= queen;	
	}
	
	void Update() {
		updateView();
	}
	
	public void updateView() {
		updatePieces();
	}
	
	private void updatePieces() {
		foreach(Piece piece in Board.Pieces) {
			GameObject pieceObj= pieceObjects[piece];
			if(pieceObj != null) {
				pieceObj.transform.position= convertCellToWorld(piece.Pos);
				
				if(piece is ActivePiece)
				{
					ActivePiece ap= (ActivePiece)piece;
					if(ap.State == ActivePiece.ActivePieceState.DEAD) {
						Debug.Log("update piece to dead: " + piece.Type.ToString());
						pieceObj.SetActive(false);
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
	
	static public Vector3 convertCellToWorld(Vector2 cell) {
		Vector3 rc= new Vector3();
	
		rc.x= cell.x;
		rc.z= cell.y;
		
		return rc;
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
	
	
	private void init() {
		clearBoard();
		createBoardTiles(board);
		createBoardPieces(board);
	}
	
	private void createBoardPieces(Board board) {
		if(piecePrefabs == null) 
			populatePiecePrefabs();
		
		GameObject newPiece= null;
		foreach( Piece piece in board.Pieces) {
			try{ 
				newPiece= GameObject.Instantiate(piecePrefabs[piece.Type]) as GameObject;
				newPiece.transform.parent= this.transform;
				newPiece.transform.position= convertCellToWorld(piece.Pos);
				
				pieceObjects[piece]= newPiece;
			} catch {
				Debug.LogError("Error: View could not instantiate piece of type: " + piece.Type.ToString());
			}
		}
	}
	
	private void createBoardTiles(Board board) {
		for(int row=0; row<board.Rows; row++) {
			for(int col=0; col<board.Columns; col++) {
				GameObject tile= GameObject.Instantiate(boardTile) as GameObject;
				Vector2 pos= Piece.makePos(col, row);
				
				tile.transform.parent= this.transform;
				tile.transform.position= convertCellToWorld(pos );
				
				tileObjects[pos]= tile;
			}
		}
	}
	
	private void clearBoard() {
		foreach( Transform child in this.transform) {
			Destroy(child);
		}
	}
	
	
	
}
