using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace AssetEditorTools
{
	public class PrefabAIPanel : UIPanel
	{
		public UIDropDown m_PrefabAIDropDown;
		public UIButton m_PrefabAIApplyButton;

		public class PrefabAIInfo 
		{
			public string name;
			public Type type;
		}

		public static List<PrefabAIInfo> prefabAIList;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			prefabAIList = [];

			width = 393;
			height = 35;

			m_PrefabAIDropDown = UIDropDowns.AddLabelledDropDown(this, 20.0f, 0.0f, "PrefabAI", 180.0f, 25.0f, 0.7f, 25, 8, true, "Change the AI of the edited asset.");
			m_PrefabAIApplyButton = UIButtons.AddButton(this, 300.0f, 0.0f, "Apply", 90.0f, 30.0f, 0.9f, 4);
			PopulateAIDropDown();

			gameObject.AddComponent<PrefabAIEditorController>();
		}

		private void PopulateAIDropDown()
		{
			List<string> PrefabAI = [];
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
                    Type[] assemblyTypes = assembly.GetTypes();
                    for (int j = 0; j < assemblyTypes.Length; j++)
                    {
                        if (assemblyTypes[j].IsSubclassOf(typeof(PrefabAI)))
                        {
                            PrefabAIInfo prefabAIInfo = new()
                            {
                                name = assemblyTypes[j].Name,
                                type = assemblyTypes[j]
                            };
                            prefabAIList.Add(prefabAIInfo);

                            PrefabAI.Add(assemblyTypes[j].Name);
                        }
                    }
                }
				catch 
				{
                    // ignored
                }
            }

            PrefabAI.Sort();

			foreach(string prefabAI in PrefabAI) 
			{
				m_PrefabAIDropDown.AddItem(prefabAI);
			}
		}

	}
}
