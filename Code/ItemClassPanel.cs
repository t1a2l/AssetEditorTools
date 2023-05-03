using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using System.Linq;

namespace AssetEditorTools
{
	public class ItemClassPanel : UIPanel
	{
		public UIDropDown m_itemClassDropDown;
		public UIButton m_itemClassDropDownButton;

		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			m_itemClassDropDown = UIDropDowns.AddLabelledDropDown(this, 393, 25, "Item Class", 220, 25, 0.7f, 20, 8, true, "Item Class determines the Service SubService and building category.");
			m_itemClassDropDownButton = UIButtons.AddButton(m_itemClassDropDown, 191, 20, "Apply", 200, 30, 0.9f, 4);
			m_itemClassDropDown.triggerButton = m_itemClassDropDownButton;
			PopulateItemClassDropDown();

			gameObject.AddComponent<EditorController>();
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
			var sortedItemClasses = sortItemClasses.OrderBy(s => (int) s.m_service).ThenBy(s => (int) s.m_subService).ThenBy(s => (int) s.m_level).ThenBy(s => s.name);

			foreach(ItemClass itemClass in sortedItemClasses) {
				m_itemClassDropDown.AddItem(itemClass.name);
			}

			m_itemClassDropDown.selectedIndex = 0;
		}


	}
}
