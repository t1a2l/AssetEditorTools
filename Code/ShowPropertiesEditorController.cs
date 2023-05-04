using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class ShowPropertiesEditorController : MonoBehaviour
	{
		private UIView m_view;

		private ShowPropertiesPanel ShowPropertiesPanel;

		public void Start() 
		{
			m_view = UIView.GetAView();

			ShowPropertiesPanel = m_view.FindUIComponent<ShowPropertiesPanel>("ShowPropertiesPanel");
			
			ShowPropertiesPanel.m_showPropertiesButton.eventClick += ToggleAsssetProperties;
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
				ShowPropertiesPanel.m_showPropertiesButton.text = "Hide Properties";
				ShowPropertiesPanel.m_showPropertiesButton.normalFgSprite = "InfoIconBaseNormal";
				ShowPropertiesPanel.m_showPropertiesButton.disabledFgSprite = "InfoIconBaseDisabled";
				ShowPropertiesPanel.m_showPropertiesButton.hoveredFgSprite = "InfoIconBaseHovered";
				ShowPropertiesPanel.m_showPropertiesButton.focusedFgSprite = "InfoIconBaseNormal";
				ShowPropertiesPanel.m_showPropertiesButton.pressedFgSprite = "InfoIconBasePressed";
			} 
			else 
			{
				ShowPropertiesPanel.m_showPropertiesButton.text = "Show Properties";
				ShowPropertiesPanel.m_showPropertiesButton.normalFgSprite = "InfoIconBaseFocused";
				ShowPropertiesPanel.m_showPropertiesButton.disabledFgSprite = "InfoIconBaseDisabled";
				ShowPropertiesPanel.m_showPropertiesButton.hoveredFgSprite = "InfoIconBaseHovered";
				ShowPropertiesPanel.m_showPropertiesButton.focusedFgSprite = "InfoIconBaseFocused";
				ShowPropertiesPanel.m_showPropertiesButton.pressedFgSprite = "InfoIconBasePressed";
			}
		}
	}
}