using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Assets.Models;
using UnityEngine;

namespace Assets.Utils
{
	public sealed class Quiz420Db
	{
		public IList<Lesson> GetLessons()
		{
			return XDocument.Parse(((TextAsset)Resources.Load(@"lessons_db")).text).Descendants(@"lesson")
				.Select(lessonElement => new Lesson
				{
					Id = int.Parse(lessonElement.Attribute(@"id").Value),
					Name = lessonElement.Attribute(@"name").Value,
					Questions = lessonElement.Descendants(@"question")
						.Select(questionElement => new Question
						{
							Id = int.Parse(questionElement.Attribute(@"id").Value),
							Text = questionElement.Attribute(@"text").Value,
							Answers = questionElement.Descendants(@"answer")
								.Select(
									answerElement => new Answer
									{
										Id = int.Parse(answerElement.Attribute(@"id").Value),
										Text = answerElement.Attribute(@"text").Value,
										Correct = bool.Parse(answerElement.Attribute(@"correct").Value)
									}).ToList()
						}).ToList()
				}).ToList();
		}

		public IList<User> GetUsers()
		{
			throw new NotImplementedException();
		}

		public IList<Score> GetHighscores()
		{
			throw new NotImplementedException();
		}
	}
}
