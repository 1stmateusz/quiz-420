using System.Collections.Generic;

namespace Assets.Models
{
	public class Lesson
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IList<Question> Questions { get; set; }
	}
}
