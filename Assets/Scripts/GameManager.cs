using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Assets.Models;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Text _questionText;

	[SerializeField]
	private Text _scoreText;

	[SerializeField]
	private Text _buttonAText;

	[SerializeField]
	private Text _buttonBText;

	[SerializeField]
	private Text _buttonCText;

	[SerializeField]
	private Text _buttonDText;

	[SerializeField]
	private Button _buttonA;

	[SerializeField]
	private Button _buttonB;

	[SerializeField]
	private Button _buttonC;

	[SerializeField]
	private Button _buttonD;

	private static IList<Question> _unansweredQuestions;
	private static Question _currentQuestion;
	private static int _currentScore;

	private readonly Color _lightBlueColor = new Color(102f/255, 178f/255, 255f/255);
	private readonly Color _lightRedColor = new Color(255f/255, 102f/255, 102f/255);

	private void Start()
	{
		//todo: determine which lesson to load
		_unansweredQuestions = new Quiz420Db().GetLessons().Single().Questions;

		_currentQuestion = GetNextQuestion();
		UpdateUi();
	}

	private void Update()
	{
		//todo: handle if needed
	}

	public void OnAnswerASelected()
	{
		OnAnswerSelected(0);
	}

	public void OnAnswerBSelected()
	{
		OnAnswerSelected(1);
	}

	public void OnAnswerCSelected()
	{
		OnAnswerSelected(2);
	}

	public void OnAnswerDSelected()
	{
		OnAnswerSelected(3);
	}

	private void OnAnswerSelected(int answerIndex)
	{
		var answer = _currentQuestion.Answers[answerIndex];
		var button = GetCorrespondingButton(answerIndex);

		button.image.color = answer.Correct ? _lightBlueColor : _lightRedColor;
		if (answer.Correct) _currentScore++;

		var timer = new Timer(500);
		timer.Elapsed += (sender, args) =>
		{
			button.image.color = Color.white;

			_unansweredQuestions.Remove(_currentQuestion);
			if (AnyQuestionLeft())
			{
				_currentQuestion = GetNextQuestion();
				UpdateUi();
			}
			else
			{
				UpdateUi();
				//todo: display score and navigate to main menu
			}
		};
		timer.AutoReset = false;
		timer.Start();
	}

	private bool AnyQuestionLeft()
	{
		return _unansweredQuestions.Any();
	}

	private Question GetNextQuestion()
	{
		//todo: get next question with specified difficulty
		var q = _unansweredQuestions[Random.Range(0, _unansweredQuestions.Count)];
		q.Answers = ShuffleAnswers(q.Answers);
		return q;
	}

	private IList<Answer> ShuffleAnswers(IEnumerable<Answer> answers)
	{
		return answers.OrderBy(a => Guid.NewGuid()).ToList();
	}

	private void UpdateUi()
	{
		_questionText.text = _currentQuestion.Text;

		_scoreText.text = _currentScore.ToString();

		_buttonAText.text = _currentQuestion.Answers[0].Text;
		_buttonBText.text = _currentQuestion.Answers[1].Text;
		_buttonCText.text = _currentQuestion.Answers[2].Text;
		_buttonDText.text = _currentQuestion.Answers[3].Text;
	}

	private Button GetCorrespondingButton(int index)
	{
		switch (index)
		{
			case 0:
				return _buttonA;
			case 1:
				return _buttonB;
			case 2:
				return _buttonC;
			default:
				return _buttonD;
		}
	}
}
