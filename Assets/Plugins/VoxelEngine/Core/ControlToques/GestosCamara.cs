using UnityEngine;
using System.Collections;

public class GestosCamara : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var pan = new TKPanRecognizer();		
		// when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
//		if( Application.platform == RuntimePlatform.IPhonePlayer )
//			pan.minimumNumberOfTouches = 2;
		
		pan.gestureRecognizedEvent += ( r ) =>
		{
			float panX = pan.deltaTranslation.x / 100;
			float panZ = pan.deltaTranslation.y / 100;
			Vector3 camPos = Camera.mainCamera.transform.position;
			float newPosX = Mathf.Clamp(camPos.x + panX, 0, 40);
			float newPosZ = Mathf.Clamp(camPos.z + panZ, 0, 40);

			Camera.mainCamera.transform.position = new Vector3( newPosX, camPos.y, newPosZ);
			//Debug.Log( "pan recognizer fired: " + r );

		};
		
		// continuous gestures have a complete event so that we know when they are done recognizing
//		pan.gestureCompleteEvent += r =>
//		{
//			//Debug.Log( "pan gesture complete" );
//		};
		TouchKit.addGestureRecognizer( pan );
		
		
		
		
		var pinch = new TKPinchRecognizer();
		pinch.gestureRecognizedEvent += ( r ) =>
		{
			Camera.mainCamera.fieldOfView = Mathf.Clamp(Camera.mainCamera.fieldOfView + pinch.deltaScale * 2, 40, 75);
//			Debug.Log( "pinch recognizer fired: " + r );
		};
		TouchKit.addGestureRecognizer( pinch );

	}
}
