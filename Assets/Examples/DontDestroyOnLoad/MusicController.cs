using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public static MusicController Instance;

	void Awake() 
	{
		this.InstantiateController();
	}

	private void InstantiateController() {
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else if(this != Instance) {
			Destroy(this.gameObject);
		}
	}
}
