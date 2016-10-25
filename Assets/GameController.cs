using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public Rigidbody gamePiecePrefab;
	public Transform spawnPoint;
	public Material materialRef;
	private float rows = 6f;
	private float columns = 6f;
	private 
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)) {

			float pieceWidth = 1f / rows;
			float pieceHeight = 1f / columns;
			Debug.Log (pieceWidth);

			for (int r = 0; r < rows; r++) {
				for (int c = 0; c < columns; c++) {
					Rigidbody gamePiece = Instantiate (gamePiecePrefab, spawnPoint.position + new Vector3(c * 0.25f,0f, r * 0.5f), spawnPoint.rotation) as Rigidbody;
					Renderer rend = gamePiece.GetComponent<Renderer> ();

					if (rend != null) {
						rend.material.mainTextureScale = new Vector2 (pieceWidth, pieceHeight);
						rend.material.SetTextureOffset ("_MainTex", new Vector2 (r*pieceWidth, c*pieceHeight));
					}
			
				}
			}

		}
	}
}
