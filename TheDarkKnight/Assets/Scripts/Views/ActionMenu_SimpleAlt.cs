using UnityEngine;
using System.Collections;


public class ActionMenu_SimpleAlt : MonoBehaviour, ActionMenu {

	protected BoardToScreen boardToScreen= null;
	public BoardToScreen BoardToScreen {
		get { return boardToScreen; }
		set { boardToScreen = value; }
	}
		
	protected Vector2 screenPos;
	public bool Show; 
	
	bool showAttack= false;
	public bool ShowAttack {
		get { return showAttack; }
		set { showAttack= value; }
	}
	
	bool showMove= false;
	public bool ShowMove {
		get { return showMove; }
		set { showMove= value; }
	}
	
	bool showHighlight= false;
	public bool ShowHighlight {
		get { return showHighlight; }
		set { showHighlight= value; }
	}
	
	Vector2 selectedCell;
	public Vector2 SelectedCell { 
		get {return selectedCell; }
	}
	
	
	public event ActionMenuDel onCancelAction;
	public event ActionMenuDel onAttackSelect;
	public event ActionMenuDel onMoveSelect;
	public event ActionMenuDel onHighlightSelect;
	
	public void showActionMenu(Vector2 cell) {
		Show= true;
		selectedCell= cell;
		
		screenPos= boardToScreen.convertCellToScreen(cell);
		//screenPos.y+= boardToScreen.getCellSize() * 0.66f;	 
	}
	
	public void hide() {
		Show= false;
		ShowAttack= false;
		ShowMove= false;
		ShowHighlight= false;
	}
	
	public int GUILevelOffset= -10;
	void OnGUI() {
		int guiLevel= GUI.depth;
		GUI.depth= GUILevelOffset;
		
		if(Show) { 
			int menuSize= (int)(boardToScreen.getCellSize()/3f);
			
			if(GUI.Button(new Rect(screenPos.x + 2*menuSize, screenPos.y + (menuSize*2), menuSize, menuSize), "X")) {
				if(onCancelAction != null) 
					onCancelAction(this);
				
			}	
			else if(ShowAttack && GUI.Button(new Rect(screenPos.x, screenPos.y, menuSize*3, menuSize*3), "ATTACK")) {
				if(onAttackSelect != null)
					onAttackSelect(this);
			}	
			else if(ShowMove && GUI.Button(new Rect(screenPos.x, screenPos.y, menuSize*3, menuSize*3), "MOVE")) {
				if(onMoveSelect != null) 
					onMoveSelect(this);
			}
			
		}
		
		GUI.depth= guiLevel;
	}
}
