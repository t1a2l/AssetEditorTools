using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssetEditorTools.EditorControllers.GroupContainer;

namespace AssetEditorTools.Panels.GroupContainer
{
	public class SubServicePanel : UIPanel
	{
		public UIDropDown m_subServiceDropDown;
		public UIButton m_subServiceApplyButton;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 35;

            relativePosition = new Vector3(0f, 35f, 0f);

            m_subServiceDropDown = UIDropDowns.AddLabelledDropDown(this, 20.0f, 0.0f, "SubService", 180.0f, 25.0f, 0.7f, 20, 8, true, "Allow you to change the Asset Sub Service");
			m_subServiceApplyButton = UIButtons.AddButton(this, 300.0f, 0.0f, "Apply", 90.0f, 30.0f, 0.9f, 4);
			PopulateSubServiceDropDown();

			gameObject.AddComponent<SubServiceEditorController>();

		}

		private void PopulateSubServiceDropDown() 
		{
			var sortSubServices = new List<ItemClass.SubService>();
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes) 
				{
                    if (sortSubServices.IndexOf(itemClass.m_subService) == -1)
                    {
                        sortSubServices.Add(itemClass.m_subService);
                    }
                }
			}

			foreach(ItemClass.SubService subService in sortSubServices) 
			{
				m_subServiceDropDown.AddItem(subService.ToString());
			}

            Array.Sort(m_subServiceDropDown.items);

            m_subServiceDropDown.selectedIndex = 0;
		}

	}
}
