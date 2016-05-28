using System.Collections.Generic;
using System.Linq;
using Assets.Models;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private Text _questionText;

	[SerializeField]
	private Text _answerAText;

	[SerializeField]
	private Text _answerBText;

	[SerializeField]
	private Text _answerCText;

	[SerializeField]
	private Text _answerDText;

	private static IList<Question> _questions;
	private static IList<Question> _unansweredQuestions;
	private static Random _random;

	private void Start()
	{
		//todo: determine which lesson to load
		_questions = new Quiz420Db().GetLessons().Single().Questions.ToList();
		_unansweredQuestions = _questions;

		//_questionText.text = _questions.Single().Text;

		SetCurrentQuestion(GetNextQuestion());
		//QuestionText = "tralala";

//		var a = GameObject.Find(@"QuestionText");
//		var b = a.GetComponent<GUIText>();
//		b.text = "dupa fsdd";
	}


	private void Update()
	{

	}

	//todo: get next question with specified difficulty
	private Question GetNextQuestion()
	{
		return _questions.Single();
	}

	private void SetCurrentQuestion(Question question)
	{
		_questionText.text = question.Text;
		PopulateAnswers(question);
	}

	private void PopulateAnswers(Question question)
	{
		_answerAText.text = question.Answers[0].Text;
		_answerBText.text = question.Answers[1].Text;
		_answerCText.text = question.Answers[2].Text;
		_answerDText.text = question.Answers[3].Text;
	}
}
