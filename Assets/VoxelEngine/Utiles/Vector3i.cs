using UnityEngine;
using System.Collections;

[System.Serializable]
public class Vector3i : System.Object{
	#region atributos
	public int x;
	public int y;
	public int z;
	#endregion
	
	#region constructores
	public Vector3i(){
		x = y = z = 0;
	}
	
	public Vector3i(int x, int y, int z){
		this.x = x;
		this.y = y;
		this.z = z;
	}
	#endregion
	
	#region metodos sobreescritos de System.Object
	public override string ToString (){
		 return string.Format("{0}, {1}, {2}", x, y, z);
	}
	
	/// <summary>
	/// Determina si <see cref="Vector3i"/> es igual al  <see cref="Vector3i"/>.
	/// Criterio de igualdad: que las coordenadas de <see cref="Vector3i"/> sean las mismas que <see cref="Vector3i"/>
	/// </summary>
	/// <param name='obj'>
	/// El <see cref="Vector3i"/> para compararlo con <see cref="Vector3i"/>.
	/// </param>
	/// <returns>
	/// <c>true</c> si el especificado <see cref="Vector3i"/> es igual al <see cref="Vector3i"/>; en otro caso, <c>false</c>.
	/// </returns>
	public override bool Equals (object v){
		bool res = false;
		Vector3i vector = (Vector3i) v;
		
		res = (this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z));
		
		return res;
	}
	#endregion
}

