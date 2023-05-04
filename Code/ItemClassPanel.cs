using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using System.Linq;

namespace AssetEditorTools
{
	public class ItemClassPanel : UIPanel
	{
		public UIDropDown m_itemClassDropDown;
		public UIButton m_itemClassApplyButton;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			m_itemClassDropDown = UIDropDowns.AddLabelledDropDown(this, 20.0f, 0.0f, "ItemClass", 180.0f, 25.0f, 0.7f, 20, 8, true, "Item Class determines the Service SubService and building category.");
			m_itemClassApplyButton = UIButtons.AddButton(this, 300.0f, 0.0f, "Apply", 90.0f, 30.0f, 0.9f, 4);
			PopulateItemClassDropDown();

			gameObject.AddComponent<ItemClassEditorController>();

		}

		private void PopulateItemClassDropDown() 
		{
			var sortItemClasses = new List<ItemClass>();
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes) 
				{
					sortItemClasses.Add(itemClass);
				}
			}
			var sortedItemClasses = sortItemClasses.OrderBy(s => s.name).ThenBy(s => (int) s.m_service).ThenBy(s => (int) s.m_subService).ThenBy(s => (int) s.m_level);

			foreach(ItemClass itemClass in sortedItemClasses) 
			{
				m_itemClassDropDown.AddItem(itemClass.name);
			}

			m_itemClassDropDown.selectedIndex = 0;
		}

	}
}
