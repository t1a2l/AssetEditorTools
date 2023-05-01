﻿using System.Collections.Generic;
using System.Linq;
using ColossalFramework.UI;
using UnityEngine;
using AssetEditorTools.UI;
using ColossalFramework;

namespace AssetEditorTools
{
	internal class AssetEditorToolsPanel : UIPanel
	{
		public UIDropDown m_itemClassDropDown;
		public UIDropDown m_UICategoryDropDown;

		public UIButton m_itemClassDropDownButton;
		public UIButton m_UICategoryDropDownButton;
		public UIButton m_showPropertiesButton;

		public UILabel m_spritelabel;
		public UIButton m_copy;
		public UIButton m_paste;

		public UITextureAtlas u_Atlas;
		public UITextureAtlas u_InfoTooltipAtlas;

		public UITextField m_UIPriorityTextField;

		public string u_Thumbnail;
		public string u_InfoTooltipThumbnail;

		public class CategoryInfo 
		{
			public string name;
			public bool BuildingInfo = false;
			public bool NetInfo = false;
			public bool PropInfo = false;
			public bool TreeInfo = false;
			public bool VehicleInfo = false;
			public bool CitizenInfo = false;
			public bool TransportInfo = false;
		}
		public static List<CategoryInfo> categoryList = null;

		public override void Start() {
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			color = new Color32(150, 150, 150, 255);

			var view = UIView.GetAView();
			var uiContainer = view.FindUIComponent("FullScreenContainer");
			var propPanel = uiContainer.Find<UIPanel>("DecorationProperties"); 
			var propPanelPanel = propPanel.Find<UIScrollablePanel>("Container");

			m_itemClassDropDown = UIDropDowns.AddLabelledDropDown(propPanelPanel, 393, 25, "Item Class", 220, 25, 0.7f, 20, 8, true, "Item Class determines the Service SubService and building category.");
			m_itemClassDropDownButton = UIButtons.AddButton(m_itemClassDropDown, 191, 20, "Apply", 200, 30, 0.9f, 4);
			m_itemClassDropDown.triggerButton = m_itemClassDropDownButton;
			PopulateItemClassDropDown();
			m_itemClassDropDown.listScrollbar = propPanel.Find<UIScrollbar>("Scrollbar");

			m_UICategoryDropDown = UIDropDowns.AddLabelledDropDown(propPanelPanel, 393, 25, "UICategory", 220, 25, 0.7f, 20, 8, true, "Category determines the tab an asset will appear under.");
			m_UICategoryDropDownButton = UIButtons.AddButton(m_UICategoryDropDown, 0, 0, "Apply", 200, 30, 0.9f, 4);
			m_UICategoryDropDown.triggerButton = m_UICategoryDropDownButton;
			PopulateUICategoryDropDown();
			m_UICategoryDropDown.listScrollbar = propPanel.Find<UIScrollbar>("Scrollbar");

			m_UIPriorityTextField = UITextFields.AddLabelledTextField(propPanelPanel, 90, 20, "UIPriority", 200, 22, 1, 4, "Determines the horizontal position inside the group toolstrip.");
			PrefabInfo info = ToolsModifierControl.toolController.m_editPrefabInfo;
			m_UIPriorityTextField.objectUserData = ToolsModifierControl.toolController.m_editPrefabInfo;
			m_UIPriorityTextField.stringUserData = "m_UIPriority";
			m_UIPriorityTextField.text = info.m_UIPriority.ToString();

			m_showPropertiesButton = UIButtons.AddButton(propPanelPanel, 324, 25, "Properties", 200, 30, 0.9f, 4, "Properties");

			m_spritelabel = UILabels.AddLabel(propPanelPanel, 181, 18, "Copy/Paste Sprites", -1, 1, UIHorizontalAlignment.Left, "Duplicates the thumbnail and tooltip used ingame.");
			m_copy = UIButtons.AddButton(propPanelPanel, 90, 25, "Copy", 200, 30, 0.9f, 4, "Copies the currently loaded asset's tooltip and thumbnail sprites to memory.");			
			m_paste = UIButtons.AddButton(propPanelPanel, 90, 25, "Paste", 200, 30, 0.9f, 4, "Pastes the tooltip and thumbnail sprites in to memory over the currently loaded asset's.");
			m_paste.state = UIButton.ButtonState.Disabled;

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

		private void PopulateUICategoryDropDown()
		{
			foreach(BuildingCollection buildingCollection in FindObjectsOfType<BuildingCollection>()) 
			{
				foreach(BuildingInfo buildingInfo in buildingCollection.m_prefabs) 
				{
					AddCategory(buildingInfo);
				}
			}
			foreach(NetCollection netCollection in FindObjectsOfType<NetCollection>()) 
			{
				foreach(NetInfo netInfo in netCollection.m_prefabs) 
				{
					AddCategory(netInfo);
				}
			}
			foreach(PropCollection propCollection in FindObjectsOfType<PropCollection>()) 
			{
				foreach(PropInfo propInfo in propCollection.m_prefabs) 
				{
					AddCategory(propInfo);
				}
			}
			foreach(TreeCollection treeCollection in FindObjectsOfType<TreeCollection>()) 
			{
				foreach(TreeInfo treeInfo in treeCollection.m_prefabs) 
				{
					AddCategory(treeInfo);
				}
			}
			foreach(VehicleCollection vehicleCollection in FindObjectsOfType<VehicleCollection>()) 
			{
				foreach(VehicleInfo vehicleInfo in vehicleCollection.m_prefabs) 
				{
					AddCategory(vehicleInfo);
				}
			}
			foreach(CitizenCollection citizenCollection in FindObjectsOfType<CitizenCollection>()) 
			{
				foreach(CitizenInfo citizenInfo in citizenCollection.m_prefabs) 
				{
					AddCategory(citizenInfo);
				}
			}
			foreach(TransportCollection transportCollection in FindObjectsOfType<TransportCollection>()) 
			{
				foreach(TransportInfo transportInfo in transportCollection.m_prefabs) 
				{
					AddCategory(transportInfo);
				}
			}

			List<CategoryInfo> reducedCategoryList = new();

			while (categoryList.Count > 0) 
			{
				CategoryInfo categoryInfo = categoryList[0];
				foreach(CategoryInfo category in categoryList.FindAll(x => x.name == categoryInfo.name)) 
				{
					if(categoryInfo.BuildingInfo == true)  {category.BuildingInfo = true;}
					if(categoryInfo.NetInfo == true)       {category.NetInfo = true;}
					if(categoryInfo.PropInfo == true)      {category.PropInfo = true;}
					if(categoryInfo.TreeInfo == true)      {category.TreeInfo = true;}
					if(categoryInfo.VehicleInfo == true)   {category.VehicleInfo = true;}
					if(categoryInfo.CitizenInfo == true)   {category.CitizenInfo = true;}
					if(categoryInfo.TransportInfo == true) {category.TransportInfo = true;}
				}
				reducedCategoryList.Add(categoryInfo);
				categoryList.RemoveAll(x => x.name == categoryInfo.name);
			}

			var sortedcategorylist = reducedCategoryList.OrderBy(s => s.BuildingInfo).ThenBy(s => s.NetInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.CitizenInfo).ThenBy(s => s.TransportInfo).ThenBy(s => s.name);

			//we ant the appropriate categories for the currently loaded Prefab type first.
			PrefabInfo prefabInfo = Singleton<ToolManager>.instance.m_properties.m_editPrefabInfo;
			if(null != prefabInfo as BuildingInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.BuildingInfo).ThenBy(s => s.name).ThenBy(s => s.NetInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.CitizenInfo).ThenBy(s => s.TransportInfo);
			} 
			else if(null != prefabInfo as NetInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.NetInfo).ThenBy(s => s.name).ThenBy(s => s.BuildingInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.CitizenInfo).ThenBy(s => s.TransportInfo);
			} 
			else if(null != prefabInfo as PropInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.PropInfo).ThenBy(s => s.name).ThenBy(s => s.BuildingInfo).ThenBy(s => s.NetInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.CitizenInfo).ThenBy(s => s.TransportInfo);
			} 
			else if(null != prefabInfo as TreeInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.TreeInfo).ThenBy(s => s.name).ThenBy(s => s.BuildingInfo).ThenBy(s => s.NetInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.CitizenInfo).ThenBy(s => s.TransportInfo);
			} 
			else if(null != prefabInfo as VehicleInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.VehicleInfo).ThenBy(s => s.name).ThenBy(s => s.BuildingInfo).ThenBy(s => s.NetInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.CitizenInfo).ThenBy(s => s.TransportInfo);
			} 
			else if(null != prefabInfo as CitizenInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.CitizenInfo).ThenBy(s => s.name).ThenBy(s => s.BuildingInfo).ThenBy(s => s.NetInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.TransportInfo);
			} 
			else if(null != prefabInfo as TransportInfo) 
			{
				sortedcategorylist = reducedCategoryList.OrderBy(s => s.TransportInfo).ThenBy(s => s.name).ThenBy(s => s.BuildingInfo).ThenBy(s => s.NetInfo).ThenBy(s => s.PropInfo).ThenBy(s => s.TreeInfo).ThenBy(s => s.VehicleInfo).ThenBy(s => s.CitizenInfo);
			} 
			else 
			{
			}
			foreach(CategoryInfo mew in sortedcategorylist) 
			{
				m_UICategoryDropDown.AddItem(mew.name);
			}
		}

		private void AddCategory(PrefabInfo prefabInfo)
		{
			CategoryInfo categoryInfo = new()
			{
				name = prefabInfo.category,
				BuildingInfo = true
			};
			if (categoryList.IndexOf(categoryInfo) == -1) 
			{
				categoryList.Add(categoryInfo);
			}
		}

	}
}