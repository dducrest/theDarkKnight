using UnityEngine;
using System.Collections;

public interface BoardToScreen  {

	int getCellSize();
	
	Vector2 convertCellToScreen(Vector2 cell);
}
