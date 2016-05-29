using System.Collections.Generic;
using System.Linq;
using Assets.Models;
using Assets.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
	[SerializeField]
	private Dropdown _dropdownLevels;

	private IEnumerable<Lesson> _lessons;
	 
	private void Start()
	{
		_lessons = new Quiz420Db().GetLessons();
		_dropdownLevels.options = _lessons.Select(l => new Dropdown.OptionData(l.Name)).ToList();
	}

	public void OnDropdownItemSelected(int itemIndex)
	{
		CrossScenesData.LessonToLoad = _lessons.First(l => l.Name == _dropdownLevels.options[itemIndex].text);
	}

	public void OnPlayClicked()
	{
		SceneManager.LoadSceneAsync(@"QuestionScene");
	}
}
