using System;
using System.Reflection;
using ColossalFramework.UI;
using UnityEngine;
using AssetEditorTools.Utils;

namespace AssetEditorTools
{
	public class EditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;
		private bool m_onchange;

		private AssetEditorToolsPanel assetEditorToolsPanel;


		public void Start() 
		{
			m_view = UIView.GetAView();

			assetEditorToolsPanel = m_view.FindUIComponent<AssetEditorToolsPanel>("AssetEditorToolsPanel");

			assetEditorToolsPanel.m_itemClassDropDown.eventSelectedIndexChanged += ItemClassSelectedChanged;
			assetEditorToolsPanel.m_UICategoryDropDown.eventSelectedIndexChanged += UICategorySelectedChanged;

			assetEditorToolsPanel.m_copy.eventClick += CopySprite;
			assetEditorToolsPanel.m_paste.eventClick += PasteSprite;
			
			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;

			assetEditorToolsPanel.m_UIPriorityTextField.eventTextChanged += new PropertyChangedEventHandler<string>(OnConfirmChange);

			assetEditorToolsPanel.m_showPropertiesButton.eventClick += ToggleAsssetProperties;
		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			m_onchange = true; // Unnecessary in theory; distinguishes automatically changing the selected/indicated ItemClass from the user doing so.
			string checkItemClass = null;
			bool item_yes;
			var prefabType = (m_toolController.m_editPrefabInfo.GetType()).Name;
			if(prefabType == "BuildingInfo" | prefabType == "NetInfo" | prefabType == "PropInfo" | prefabType == "TransportInfo" | prefabType == "TreeInfo" | prefabType == "VehicleInfo") 
			{
				item_yes = true;
				BuildingInfo bi = m_toolController.m_editPrefabInfo as BuildingInfo;
				if(bi != null) 
				{
					checkItemClass = bi.m_class.name;
				}
				NetInfo ni = m_toolController.m_editPrefabInfo as NetInfo;
				if(ni != null) 
				{
					checkItemClass = ni.m_class.name;
				}
				PropInfo pi = m_toolController.m_editPrefabInfo as PropInfo;
				if(pi != null) 
				{
					checkItemClass = pi.m_class.name;
				}
				TransportInfo ti = m_toolController.m_editPrefabInfo as TransportInfo;
				if(ti != null) 
				{
					checkItemClass = ti.m_class.name;
				}
				TreeInfo tri = m_toolController.m_editPrefabInfo as TreeInfo;
				if(tri != null) 
				{
					checkItemClass = tri.m_class.name;
				}
				VehicleInfo vi = m_toolController.m_editPrefabInfo as VehicleInfo;
				if(vi != null) 
				{
					checkItemClass = vi.m_class.name;
				}
			} 
			else 
			{
				item_yes = false;
			}
			if(item_yes) 
			{
				assetEditorToolsPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(assetEditorToolsPanel.m_itemClassDropDown.items, checkItemClass);
				assetEditorToolsPanel.m_UICategoryDropDown.selectedIndex = Array.IndexOf(assetEditorToolsPanel.m_UICategoryDropDown.items, m_toolController.m_editPrefabInfo.category);
			}
			assetEditorToolsPanel.m_itemClassDropDown.listScrollbar.isEnabled = item_yes;
			m_onchange = false;
		}

		// Attempts to matches the selected item class name with an actual ItemClass.
		private void ItemClassSelectedChanged(UIComponent component, int index) 
		{
			if(!m_onchange) 
			{
				UIDropDown dropdown = (UIDropDown) component as UIDropDown;
				foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
				{
					foreach(ItemClass itemClass in collection.m_classes) 
					{
						if(dropdown.selectedValue == itemClass.name) 
						{
							//list was populated using the same technique, going to be lazy and presume that this is still valid.
							var prefabType = (m_toolController.m_editPrefabInfo.GetType()).Name;
							if(prefabType == "BuildingInfo" | prefabType == "NetInfo" | prefabType == "PropInfo" | prefabType == "TransportInfo" | prefabType == "TreeInfo" | prefabType == "VehicleInfo") 
							{
								BuildingInfo bi = m_toolController.m_editPrefabInfo as BuildingInfo;
								if(bi != null) 
								{
									MakeItemClassSafe(bi,itemClass);
								}
								NetInfo ni = m_toolController.m_editPrefabInfo as NetInfo;
								if(ni != null) {
									ni.m_class = itemClass;
								}
								PropInfo pi = m_toolController.m_editPrefabInfo as PropInfo;
								if(pi != null) 
								{
									pi.m_class = itemClass;
								}
								TransportInfo ti = m_toolController.m_editPrefabInfo as TransportInfo;
								if(ti != null) 
								{
									ti.m_class = itemClass;
								}
								TreeInfo tri = m_toolController.m_editPrefabInfo as TreeInfo;
								if(tri != null) 
								{
									tri.m_class = itemClass;
								}
								VehicleInfo vi = m_toolController.m_editPrefabInfo as VehicleInfo;
								if(vi != null) 
								{
									vi.m_class = itemClass;
								}
							}
							return;
						}
					}
				}
			}
		}

		// Renders the new item class to be stored inside a building info prefab safe, unless the asset is already broken.
		private void MakeItemClassSafe(BuildingInfo bi, ItemClass itemClass) 
		{
			if ((int) itemClass.m_service <= (int)ItemClass.Service.Office) 
			{
				SimulationManager.GetManagers(out ISimulationManager[] managers, out _);
				foreach (var manager in managers) 
				{
					NetManager net_manager = manager as NetManager;
					if(net_manager != null) 
					{
						if((net_manager.m_nodeCount + net_manager.m_segmentCount + net_manager.m_laneCount) > 0) 
						{
							if ((int)bi.m_class.m_service <= (int)ItemClass.Service.Office) 
							{
								LogHelper.Error("Private building cannot include roads or other net types.\n(paths, roads, tracks, pipes, powerlines, routes, lanes, etc.)\nAsset my already be broken.\nBackup and removal is strongly reccommended.");
							} 
							else 
							{
								m_onchange = true;
								assetEditorToolsPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(assetEditorToolsPanel.m_itemClassDropDown.items, bi.m_class.name);
								m_onchange = false;
								LogHelper.Error("ItemClass.Service", "Private building cannot include roads or other net types.\nItemClass change rejected.");
								return;
							}
						}
					}
				}
				//building info is not kept accurate, look to the NetManager.
				if (bi.m_paths != null && bi.m_paths.Length != 0) 
				{ 
					m_onchange = true; 
					assetEditorToolsPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(assetEditorToolsPanel.m_itemClassDropDown.items, bi.m_class.name);
					m_onchange = false;
					LogHelper.Error("ItemClass.Placement", "Private building cannot include roads or other net types.\nItemClass change rejected.");
					return;
				}
				if (bi.m_placementStyle == ItemClass.Placement.Manual) 
				{
					bi.m_placementStyle = ItemClass.Placement.Automatic;
					bi.m_class = itemClass;
					LogHelper.Error("ItemClass.Placement", "Private building cannot have manual placement style.\nPlacement style changed to automatic.");
					return;
				}
			} 
			else 
			{
				if (bi.m_placementStyle == ItemClass.Placement.Automatic) 
				{
					bi.m_placementStyle = ItemClass.Placement.Manual;
					bi.m_class = itemClass;
					LogHelper.Error("ItemClass.Placement", "Player building cannot have automatic placement style.\nPlacement style changed to manual.");
					return;
				}
			}
			if (bi.m_class == null) 
			{
				m_onchange = true;
				assetEditorToolsPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(assetEditorToolsPanel.m_itemClassDropDown.items, bi.m_class.name);
				m_onchange = false;
				return;
			}
			bi.m_class = itemClass;
		}

		// Attempts to shove the selected UICategory into the Prefab.
		private void UICategorySelectedChanged(UIComponent component, int index) 
		{
			if(!m_onchange) 
			{
				UIDropDown dropdown = (UIDropDown) component as UIDropDown;
				typeof(PrefabInfo).GetField("m_UICategory", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(m_toolController.m_editPrefabInfo, dropdown.selectedValue);
			}
		}

		private void CopySprite(UIComponent component, UIMouseEventParameter eventParam) 
		{
			var prefabInfo = (PrefabInfo) m_toolController.m_editPrefabInfo;
			if(prefabInfo.m_Atlas != null) 
			{ 
				assetEditorToolsPanel.u_Atlas = prefabInfo.m_Atlas; 
				assetEditorToolsPanel.m_paste.state = UIButton.ButtonState.Normal;
				if(prefabInfo.m_Thumbnail != null) 
				{ 
					assetEditorToolsPanel.u_Thumbnail = prefabInfo.m_Thumbnail; 
					assetEditorToolsPanel.m_paste.atlas = assetEditorToolsPanel.u_Atlas;
					assetEditorToolsPanel.m_paste.hoveredFgSprite = assetEditorToolsPanel.u_Thumbnail;
				}
			}
			if(prefabInfo.m_InfoTooltipAtlas != null) 
			{ 
				assetEditorToolsPanel.u_InfoTooltipAtlas = prefabInfo.m_InfoTooltipAtlas; 
				assetEditorToolsPanel.m_paste.state = UIButton.ButtonState.Normal;
				if(prefabInfo.m_InfoTooltipThumbnail != null) 
				{
					assetEditorToolsPanel.u_InfoTooltipThumbnail = prefabInfo.m_InfoTooltipThumbnail; 
					assetEditorToolsPanel.m_paste.state = UIButton.ButtonState.Normal;
				}
				assetEditorToolsPanel.m_paste.atlas = assetEditorToolsPanel.u_InfoTooltipAtlas;
				assetEditorToolsPanel.m_paste.normalBgSprite  = assetEditorToolsPanel.u_InfoTooltipThumbnail;
			}
		}

		private void PasteSprite(UIComponent component, UIMouseEventParameter eventParam) 
		{
			var prefabInfo = m_toolController.m_editPrefabInfo;
			if(assetEditorToolsPanel.u_Atlas != null) prefabInfo.m_Atlas = assetEditorToolsPanel.u_Atlas;
			if(assetEditorToolsPanel.u_Thumbnail != null) prefabInfo.m_Thumbnail = assetEditorToolsPanel.u_Thumbnail;
			if(assetEditorToolsPanel.u_InfoTooltipAtlas !=null ) prefabInfo.m_InfoTooltipAtlas = assetEditorToolsPanel.u_InfoTooltipAtlas;
			if(assetEditorToolsPanel.u_InfoTooltipThumbnail !=null ) prefabInfo.m_InfoTooltipThumbnail = assetEditorToolsPanel.u_InfoTooltipThumbnail;
		}

		//public class DecorationPropertiesPanel : ToolsModifierControl
		private void OnConfirmChange(UIComponent comp, string text) 
		{
			if (comp == null || comp.objectUserData == null) {
				return;
			}
			FieldInfo field = comp.objectUserData.GetType().GetField(comp.stringUserData, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (field.FieldType == typeof(int)) 
			{
				if (int.TryParse(text, out int num)) 
				{
					comp.color = Color.white;
					field.SetValue(comp.objectUserData, num);
				} 
				else 
				{
					comp.color = Color.red;
				}
			} 
			else if (field.FieldType == typeof(float)) 
			{
				if (float.TryParse(text, out float num2)) 
				{
					comp.color = Color.white;
					field.SetValue(comp.objectUserData, num2);
				} 
				else 
				{
					comp.color = Color.red;
				}
			}
		}

		private void ToggleAsssetProperties(UIComponent component, UIMouseEventParameter eventParam) 
		{
			var decorationProperties = UIView.GetAView().FindUIComponent("FullScreenContainer").Find<UIPanel>("DecorationProperties");
			if(decorationProperties != null) 
			{
				decorationProperties.isVisible = !decorationProperties.isVisible;
				SpriteShowForeground();
			}
		}

		private void SpriteShowForeground() 
		{
			var decorationProperties = UIView.GetAView().FindUIComponent("FullScreenContainer").Find<UIPanel>("DecorationProperties");
			if(decorationProperties.isVisible) 
			{
				assetEditorToolsPanel.m_showPropertiesButton.normalFgSprite = "InfoIconBaseNormal";
				assetEditorToolsPanel.m_showPropertiesButton.disabledFgSprite = "InfoIconBaseDisabled";
				assetEditorToolsPanel.m_showPropertiesButton.hoveredFgSprite = "InfoIconBaseHovered";
				assetEditorToolsPanel.m_showPropertiesButton.focusedFgSprite = "InfoIconBaseNormal";
				assetEditorToolsPanel.m_showPropertiesButton.pressedFgSprite = "InfoIconBasePressed";
			} 
			else 
			{
				assetEditorToolsPanel.m_showPropertiesButton.normalFgSprite = "InfoIconBaseFocused";
				assetEditorToolsPanel.m_showPropertiesButton.disabledFgSprite = "InfoIconBaseDisabled";
				assetEditorToolsPanel.m_showPropertiesButton.hoveredFgSprite = "InfoIconBaseHovered";
				assetEditorToolsPanel.m_showPropertiesButton.focusedFgSprite = "InfoIconBaseFocused";
				assetEditorToolsPanel.m_showPropertiesButton.pressedFgSprite = "InfoIconBasePressed";
			}
		}
	}
}