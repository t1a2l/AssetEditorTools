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

			ItemClassPanel.m_itemClassApplyButton.eventClicked += ApplyNewItemClass;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			string checkItemClass = null;
			BuildingInfo bi = info as BuildingInfo;
			if(bi != null) 
			{
				checkItemClass = bi.m_class.name;
			}
			NetInfo ni = info as NetInfo;
			if(ni != null) 
			{
				checkItemClass = ni.m_class.name;
			}
			PropInfo pi = info as PropInfo;
			if(pi != null) 
			{
				checkItemClass = pi.m_class.name;
			}
			TransportInfo ti = info as TransportInfo;
			if(ti != null) 
			{
				checkItemClass = ti.m_class.name;
			}
			TreeInfo tri = info as TreeInfo;
			if(tri != null) 
			{
				checkItemClass = tri.m_class.name;
			}
			VehicleInfo vi = info as VehicleInfo;
			if(vi != null) 
			{
				checkItemClass = vi.m_class.name;
			}
			ItemClassPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(ItemClassPanel.m_itemClassDropDown.items, checkItemClass);
		}

		// Attempts to matches the selected item class name with an actual ItemClass.
		private void ApplyNewItemClass(UIComponent component, UIMouseEventParameter eventParam) 
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			UIDropDown dropdown = (UIDropDown) component;
			foreach(ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>()) 
			{
				foreach(ItemClass itemClass in collection.m_classes)
				{
					if(dropdown.selectedValue == itemClass.name)
					{
						BuildingInfo bi = info as BuildingInfo;
						if(bi != null) 
						{
							bi.m_class = itemClass;
						}
						NetInfo ni = info as NetInfo;
						if(ni != null) 
						{
							ni.m_class = itemClass;
						}
						PropInfo pi = info as PropInfo;
						if(pi != null) 
						{
							pi.m_class = itemClass;
						}
						TransportInfo ti = info as TransportInfo;
						if(ti != null) 
						{
							ti.m_class = itemClass;
						}
						TreeInfo tri = info as TreeInfo;
						if(tri != null) 
						{
							tri.m_class = itemClass;
						}
						VehicleInfo vi = info as VehicleInfo;
						if(vi != null) 
						{
							vi.m_class = itemClass;
						}
						return;
					}
				}

			}
			
		}

	}
}