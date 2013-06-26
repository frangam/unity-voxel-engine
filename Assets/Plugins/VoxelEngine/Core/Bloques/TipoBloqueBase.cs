using UnityEngine;
using System.Collections;

public class TipoBloqueBase : IBloque {
	public virtual bool esSolido(){
		return false;
	}
}
