using ColossalFramework.UI;
using System.Collections.Generic;
using UnityEngine;
using static DecorationPropertiesPanel;

namespace AssetEditorTools
{
	public class ItemClassGroupContianerPanel : UIPanel
	{
        public static Dictionary<string, GroupInfo> m_GroupStates = [];

        public override void Start() 
		{ 
			base.Start();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
            height = 45;

            var itemClassPanel = AddUIComponent<ItemClassPanel>();
            AttachUIComponent(itemClassPanel.gameObject);

            var groupContianerPanel = AddUIComponent<GroupContianerPanel>();
			var groupContianerPanelComponent = AttachUIComponent(groupContianerPanel.gameObject);

            UIPanel uIPanel = groupContianerPanelComponent.Find<UIPanel>("GroupContianerPanel");

            var view = UIView.GetAView();
            var FullScreenContainer = view.FindUIComponent("FullScreenContainer");
            var DecorationProperties = FullScreenContainer.Find<UIPanel>("DecorationProperties");
            var Container = DecorationProperties.Find<UIScrollablePanel>("Container");

            var itemClassGroupContianerPanel = Container.Find<UIComponent>("ItemClassGroupContianerPanel");

            uIPanel.Hide();
            uIPanel.size = new Vector2(uIPanel.size.x, 0f);
            m_GroupStates.Add("ItemClassGroup", new GroupInfo
            {
                m_Folded = true,
                m_Container = itemClassGroupContianerPanel,
                m_PropertyContainer = uIPanel
            });

        }

    }
}
