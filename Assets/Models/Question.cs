using System.Collections.Generic;

namespace Assets.Models
{
	public class Question
	{
		public int Id { get; set; }
		public string Text { get; set; }
		//public QuestionDifficulty Difficullty { get; set; }
		public IList<Answer> Answers { get; set; }
	}

	public enum QuestionDifficulty
	{
		VeryEasy,
		Easy,
		Medium,
		Hard,
		VeryHard
	}
}
