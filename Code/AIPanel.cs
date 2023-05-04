using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using System.Linq;
using ColossalFramework;
using System;
using System.Reflection;

namespace AssetEditorTools
{
	public class AIPanel : UIPanel
	{
		public UIDropDown m_AIDropDown;
		public UIButton m_AIApplyButton;

		public class CategoryInfo 
		{
			public string name;
			public string type;
		}

		public static List<CategoryInfo> categoryList;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			categoryList = new();

			width = 393;
			height = 25;

			m_AIDropDown = UIDropDowns.AddLabelledDropDown(this, 20.0f, 0.0f, "AI", 180.0f, 25.0f, 0.7f, 25, 8, true, "Change the AI of the edited asset.");
			m_AIApplyButton = UIButtons.AddButton(this, 300.0f, 0.0f, "Apply", 90.0f, 30.0f, 0.9f, 4);
			PopulateAIDropDown();

			gameObject.AddComponent<AIEditorController>();
		}

		private void PopulateAIDropDown()
		{
			var sortedAI = new List<string>();
			foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				Type[] assemblyTypes = a.GetTypes();
				for (int j = 0; j < assemblyTypes.Length; j++) 
				{
					if (assemblyTypes[j].IsSubclassOf(typeof (PrefabAI))) 
					{
						sortedAI.Add(assemblyTypes[j].Name);
					}
				}
			}

			foreach(string ai in sortedAI) 
			{
				m_AIDropDown.AddItem(ai);
			}
		}

	

	}
}
