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

			m_itemClassDropDown = UIDropDowns.AddLabelledDropDown(this, 190.0f, 0.0f, "Item Class", 191.0f, 20.0f, 0.7f, 25, 8, true, "Item Class determines the Service SubService and building category.");
			m_itemClassDropDownButton = UIButtons.AddButton(m_itemClassDropDown, 0.0f, 0.0f, "Apply", 191.0f, 20.0f, 0.9f, 4);
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
