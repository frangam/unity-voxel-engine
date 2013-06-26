using UnityEngine;
using System.Collections;

public interface IBloque{
	
 	bool esSolido();
	Vector2 posicionUVTexturaCaraSuperior();
	Vector2 posicionUVTexturaCaraInferior();
	Vector2 posicionUVTexturaCaraLaterales();
	Vector2 posicionUVTexturaCara();
	bool destruirse(Bloque bloque);
	bool crearse(Bloque bloque);
}
