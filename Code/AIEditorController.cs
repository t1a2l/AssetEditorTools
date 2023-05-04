using System;
using ColossalFramework.UI;
using UnityEngine;
using AssetEditorTools.Utils;
using Object = UnityEngine.Object;

namespace AssetEditorTools
{
	public class AIEditorController : MonoBehaviour
	{
		private ToolController m_toolController;
		private UIView m_view;

		private AIPanel AIPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			AIPanel = m_view.FindUIComponent<AIPanel>("AIPanel");

			AIPanel.m_AIApplyButton.eventClicked += ApplyNewAI;

			m_toolController = ToolsModifierControl.toolController;
			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
		}

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			AIPanel.m_AIDropDown.selectedIndex = Array.IndexOf(AIPanel.m_AIDropDown.items, info.GetAI().name);
		}

		// Attempts to matches the selected item class name with an actual ItemClass.
		private void ApplyNewAI(UIComponent component, UIMouseEventParameter eventParam) 
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			UIDropDown dropdown = (UIDropDown) component;

			if(info.GetAI().name != dropdown.selectedValue)
			{
				var oldAI = info.GetComponent<PrefabAI>();
                Object.DestroyImmediate(oldAI);
                var newAI = (PrefabAI)info.gameObject.AddComponent(info.GetAI().GetType());
                PrefabUtil.TryCopyAttributes(oldAI, newAI, false);
			}

		}

	}
}