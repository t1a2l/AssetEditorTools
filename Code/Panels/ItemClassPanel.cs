using ColossalFramework.UI;
using AssetEditorTools.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AssetEditorTools
{
	public class ItemClassPanel : UIPanel
	{
		public UIPanel GroupContainer;

		public UIButton uibutton;
		public UIDropDown m_itemClassDropDown;
		public UIButton m_itemClassApplyButton;
	
		public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			GroupContainer = AddGroupConatinerPanel();

			var servicePanel = GroupContainer.AddUIComponent<ServicePanel>();
			GroupContainer.AttachUIComponent(servicePanel.gameObject);

			var subServicePanel = AddUIComponent<SubServicePanel>();
			GroupContainer.AttachUIComponent(subServicePanel.gameObject);

			var levelPanel = AddUIComponent<LevelPanel>();
			GroupContainer.AttachUIComponent(levelPanel.gameObject);

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

		
		private UIPanel AddGroupConatinerPanel()
		{
			var view = UIView.GetAView();
			var FullScreenContainer = view.FindUIComponent("FullScreenContainer");
			var DecorationProperties = FullScreenContainer.Find<UIPanel>("DecorationProperties");
			var Container = DecorationProperties.Find<UIScrollablePanel>("Container");
			UIComponent uicomponent = Container.AttachUIComponent(UITemplateManager.GetAsGameObject("GroupPropertySet"));
			uibutton = uicomponent.Find<UIButton>("Toggle");
			uibutton.text = "";
			uibutton.stringUserData = "ItemClassData";
			uibutton.normalFgSprite = "PropertyGroupClosed";
			uibutton.transform.SetParent(transform);
			uibutton.relativePosition = new Vector2(3.0f, 0.0f);
			UIPanel uipanel = uicomponent.Find<UIPanel>("GroupContainer");
			uipanel.Hide();
			uipanel.size = new Vector2(uipanel.size.x, 0f);
			ItemClassEditorController.m_GroupStates.Add("ItemClassData", new ItemClassEditorController.GroupInfo
			{
				m_Folded = true,
				m_Container = uicomponent,
				m_PropertyContainer = uipanel
			});
			width = uipanel.size.x - uipanel.autoLayoutPadding.horizontal;
			return uipanel;
		}
	}
}
