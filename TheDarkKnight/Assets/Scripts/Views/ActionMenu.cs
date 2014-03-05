using UnityEngine;
using System.Collections;

public delegate void ActionMenuDel (ActionMenu sender);

public interface ActionMenu {
	
	BoardToScreen BoardToScreen { get; set; }
	
	event ActionMenuDel onCancelAction;
	event ActionMenuDel onAttackSelect;
	event ActionMenuDel onMoveSelect;
	event ActionMenuDel onHighlightSelect;
	
	bool ShowAttack {get ; set; }
	bool ShowMove {get ; set; }
	bool ShowHighlight {get ; set; }
	
	Vector2 SelectedCell { get; }
	
	void showActionMenu(Vector2 cell);
	
	void hide();
}
