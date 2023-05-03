using System;
using ColossalFramework.UI;
using UnityEngine;
using AssetEditorTools.Utils;

namespace AssetEditorTools
{
	public class ItemClassEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;
		private bool m_onchange;

		private ItemClassPanel ItemClassPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			ItemClassPanel = m_view.FindUIComponent<ItemClassPanel>("ItemClassPanel");

			ItemClassPanel.m_itemClassDropDown.eventSelectedIndexChanged += ItemClassSelectedChanged;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;

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
				ItemClassPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(ItemClassPanel.m_itemClassDropDown.items, checkItemClass);
			}
			var view = UIView.GetAView();
			var FullScreenContainer = view.FindUIComponent("FullScreenContainer");
			var DecorationProperties = FullScreenContainer.Find<UIPanel>("DecorationProperties"); 
			var Container = DecorationProperties.Find<UIScrollablePanel>("Container");
			var scrollBar = Container.Find<UIScrollbar>("Scrollbar");
			ItemClassPanel.m_itemClassDropDown.listScrollbar = scrollBar;
			ItemClassPanel.m_itemClassDropDown.listScrollbar.isEnabled = item_yes;
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
								ItemClassPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(ItemClassPanel.m_itemClassDropDown.items, bi.m_class.name);
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
					ItemClassPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(ItemClassPanel.m_itemClassDropDown.items, bi.m_class.name);
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
				ItemClassPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(ItemClassPanel.m_itemClassDropDown.items, bi.m_class.name);
				m_onchange = false;
				return;
			}
			bi.m_class = itemClass;
		}

	}
}