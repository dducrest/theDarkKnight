using UnityEngine;
using System.Collections.Generic;

public class EnemiesChase_Tester : MonoBehaviour {

	public Vector2 BoardSize= new Vector2(8,8);
	public TopDownView_Simple view;
	public MonoBehaviour actionMenuMono;
	protected ActionMenu actionMenu;
	public DarkKnightController2 controller= new DarkKnightController2();

	// Use this for initialization
	void Start () {
		//Models
		board.Columns = (int)BoardSize [0];
		board.Rows = (int)BoardSize [1];
		
		board.Pieces.Add (new Batman (0, 0));
		board.Pieces.Add (new Rook (7, 7));
		board.Pieces.Add(new Queen(3,3));
		board.Pieces.Add(new Bishop(6,2));
		board.Pieces.Add(new King(1,1));
		board.Pieces.Add (new Pawn (7, 6, Pawn.PawnDirection.DOWN));
		board.Pieces.Add (new Rook (0, 7));
		board.Pieces.Add (new Knight (3, 0 ));
		
		board.getBatman().Highlight= true;
		
		
		// View Setup
		view.board = board;
		view.onCellSelect+= this.onMove;
		
		actionMenu= (ActionMenu)actionMenuMono;
		actionMenu.BoardToScreen= view;
		
		
		//Controller Setup
		controller.Selector= view;
		controller.Board= board;
		controller.ActionMenu= actionMenu;
		controller.onBatmanWin+= roundWin;
		controller.onBatmanDead+= roundLose;
		controller.startGame();
	}

	Board board= new Board();

	bool done= false;
	bool win= false;
	public void roundWin() {
		done= true;
		win= true;
	}

	public void roundLose() {
		done= true;
		win= false;
	}

	public void onMove(int col, int row) {

		Piece p = board.getPieceAt(col,row);
		if(p != null)
			Debug.Log (string.Format("{2} : Cell Selected {0},{1}",col, row, p.Type.ToString()));
		else
			Debug.Log (string.Format("Empty Cell Selected {0},{1}",col, row));
	}



	// Update is called once per frame
	void Update () {
	
	}
	
	
	public string rules= "\nRules:\n1)The Dark Knight must clear the board of other pieces without being attacked.\n\n2) The Dark Knight may move or attack like a standard Chess Knight.\n\n3) The Dark Knight can use his Detective Powers to use an opponent's attack against them.\nExample: If the Dark Knight is in range of a Rook's Attack, then the Dark Knight may attack that Rook, moving like a Rook. \n\n\nCaution: Enemies will attack if **ALERTED** to the Dark Knights presense. ";
	Vector2 scrollRules;
	void OnGUI() {
		if(done)
		{
			if(win)
			{
				Vector2 endGame= view.convertCellToScreen( new Vector2(board.Columns+1, 0));
				GUI.TextArea(new Rect(endGame.x, endGame.y, Screen.width- endGame.x - view.getCellSize(), view.getCellSize()), "The Dark Knight has cleared the city!");
			
			}
			else 
			{
				Vector2 endGame= view.convertCellToScreen( new Vector2(board.Columns+1, 0));
				GUI.TextArea(new Rect(endGame.x, endGame.y, Screen.width- endGame.x - view.getCellSize(), view.getCellSize()), "The Dark Knight has fallen!\nTry Again.");
			}
		}
		
		Vector2 resetButton= view.convertCellToScreen( new Vector2(board.Columns+1, 2));
		if(GUI.Button(new Rect(resetButton.x, resetButton.y, Screen.width- resetButton.x - view.getCellSize(), view.getCellSize()),"Reset"))
			Application.LoadLevel("EnemiesChase");
		
		
		Vector2 boardEdge= view.convertCellToScreen( new Vector2(board.Columns+1, 3));
		
		float requiredHeight= GUI.skin.label.CalcHeight( new GUIContent(rules), Screen.width- boardEdge.x - view.getCellSize());
		if(requiredHeight <= Screen.height-boardEdge.y) {
			GUI.Label(new Rect(boardEdge.x, boardEdge.y, Screen.width- boardEdge.x - view.getCellSize(), Screen.height-boardEdge.y), rules);
		}
		else {
			scrollRules= GUI.BeginScrollView(new Rect(boardEdge.x, boardEdge.y, Screen.width- boardEdge.x - view.getCellSize(), Screen.height-boardEdge.y),
			                             scrollRules, new Rect(boardEdge.x, boardEdge.y, Screen.width- boardEdge.x - view.getCellSize()-15, requiredHeight));
			
			GUI.Label(new Rect(boardEdge.x, boardEdge.y, (Screen.width- boardEdge.x - view.getCellSize()-15), requiredHeight), rules);
			
			GUI.EndScrollView();
		}
	}
}
