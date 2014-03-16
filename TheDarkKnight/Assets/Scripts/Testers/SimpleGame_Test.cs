using UnityEngine;
using System.Collections.Generic;

public class SimpleGame_Tester: MonoBehaviour {

	public Vector2 BoardSize= new Vector2(8,8);
	public TopDownView_Simple view;
	public DarkKnightControllerInterface controller= new DarkKnightController();

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
		board.Pieces.Add (new Knight (0, 3));


		// View Setup
		view.Board = board;
		view.onCellSelect+= this.onMove;
		
				

		//Controller Setup
		controller.Selector= view;
		controller.Board= board;
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
	
	void OnGUI() {
		if(done)
		{
			if(win)
			{
				GUI.TextArea(new Rect( (Screen.width - 200), 0, 200,50), "The Dark Knight has cleared the city!");
			
			}
			else 
			{
				GUI.TextArea(new Rect( (Screen.width - 200), 0, 200,50), "The Dark Knight has fallen!\nTry Again.");
			
			}
		}
	
	}
}
