using System;

[Serializable]
public class HighScore : IPersistence {
	public int score;
	public const string NameOfFile = "highscore.dat";
	public string FileName {
		get {
			return NameOfFile;
		}
	}
}