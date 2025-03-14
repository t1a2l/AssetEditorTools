using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssetEditorTools.EditorControllers.GroupContainer;

namespace AssetEditorTools.Panels.GroupContainer
{
	public class LevelPanel : UIPanel
	{
		public UIDropDown m_levelDropDown;
		public UIButton m_levelApplyButton;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 35;

            relativePosition = new Vector3(0f, 70f, 0f);

            m_levelDropDown = UIDropDowns.AddLabelledDropDown(this, 20.0f, 0.0f, "Level", 180.0f, 25.0f, 0.7f, 20, 8, true, "Allow you to change the Asset Level");
			m_levelApplyButton = UIButtons.AddButton(this, 300.0f, 0.0f, "Apply", 90.0f, 30.0f, 0.9f, 4);
			PopulateLevelDropDown();

			gameObject.AddComponent<LevelEditorController>();

		}

		private void PopulateLevelDropDown() 
		{
			var sortLevels = new List<ItemClass.Level>();
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes) 
				{
					if(sortLevels.IndexOf(itemClass.m_level) == -1)
					{
						sortLevels.Add(itemClass.m_level);
					}
				}
			}

			foreach(ItemClass.Level level in sortLevels) 
			{
				m_levelDropDown.AddItem(level.ToString());
			}

            Array.Sort(m_levelDropDown.items);

            m_levelDropDown.selectedIndex = 0;
		}

	}
}
