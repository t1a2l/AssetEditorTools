using System;
using System.Reflection;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class UICategoryEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;
		private bool m_onchange;

		private UICategoryPanel UICategoryPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			UICategoryPanel = m_view.FindUIComponent<UICategoryPanel>("UICategoryPanel");

			UICategoryPanel.m_UICategoryDropDown.eventSelectedIndexChanged += UICategorySelectedChanged;
		
			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;

		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			m_onchange = true; // Unnecessary in theory; distinguishes automatically changing the selected/indicated ItemClass from the user doing so.
			UICategoryPanel.m_UICategoryDropDown.selectedIndex = Array.IndexOf(UICategoryPanel.m_UICategoryDropDown.items, m_toolController.m_editPrefabInfo.category);

			var view = UIView.GetAView();
			var FullScreenContainer = view.FindUIComponent("FullScreenContainer");
			var DecorationProperties = FullScreenContainer.Find<UIPanel>("DecorationProperties"); 
			var Container = DecorationProperties.Find<UIScrollablePanel>("Container");
			var scrollBar = Container.Find<UIScrollbar>("Scrollbar");
			UICategoryPanel.m_UICategoryDropDown.listScrollbar = scrollBar;
			m_onchange = false;
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

	}
}