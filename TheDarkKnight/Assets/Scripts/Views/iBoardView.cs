
public interface iBoardView: CellSelector, BoardToScreen  {

	Board Board { 
		get ;
		set ;
	}
	void updateView();	
}
