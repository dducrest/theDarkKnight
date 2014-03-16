using UnityEngine;
using System.Collections;

public delegate void CellSelect(int col, int row);

public interface CellSelector {
	event CellSelect onCellSelect;
}
