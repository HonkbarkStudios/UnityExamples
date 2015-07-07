using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PersistenceSceneController : MonoBehaviour {
	public Text Text;
	public InputField InputField;

	public void SaveInputFieldValue() {
		var inputFieldValue = 0;
		if(int.TryParse(this.InputField.text, out inputFieldValue)) {
			this.SaveToDisc();
		}
	}

	private void SaveToDisc() {
		var highScore = this.GetHighScoreFromField();
		PersistenceManager.Instance.Save(highScore);
	}

	private HighScore GetHighScoreFromField() {
		var scoreFromInput = int.Parse(this.InputField.text);
		var highScore = new HighScore();
		highScore.score = scoreFromInput;
		return highScore;
	}

	void Start() {
		this.LoadHighScore();
	}

	private void LoadHighScore() {
		var highscore = PersistenceManager.Instance.Load(HighScore.NameOfFile) as HighScore;
		if(highscore != null) {
			this.Text.text = highscore.score.ToString();
		}
	}
}
