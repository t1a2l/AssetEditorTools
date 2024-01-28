using System;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class ItemClassEditorController : MonoBehaviour
	{
		private ItemClassPanel itemClassPanel;

		private ToolController m_toolController;

        private UIView m_view;

        public struct GroupInfo
		{
			public bool m_Folded;

			public UIComponent m_Container;

			public UIPanel m_PropertyContainer;
		}

		public void Start()
		{
            m_view = UIView.GetAView();

            itemClassPanel = m_view.FindUIComponent<ItemClassPanel>("ItemClassPanel");

            itemClassPanel.m_toggle.eventClicked += OnGroupClicked;

            itemClassPanel.m_itemClassApplyButton.eventClicked += ApplyNewItemClass;

            m_toolController = ToolsModifierControl.toolController;

			m_toolController.eventEditPrefabChanged += OnEditPrefabChanged;
        }

		// Sets the ItemClass dropdown box and the UICategory dropdown box to the currently loaded prefab and category.
		private void OnEditPrefabChanged(PrefabInfo info)
		{
			string checkItemClass = null;
			BuildingInfo bi = info as BuildingInfo;
			if (bi != null)
			{
				checkItemClass = bi.m_class.name;
			}
			NetInfo ni = info as NetInfo;
			if (ni != null)
			{
				checkItemClass = ni.m_class.name;
			}
			PropInfo pi = info as PropInfo;
			if (pi != null)
			{
				checkItemClass = pi.m_class.name;
			}
			TransportInfo ti = info as TransportInfo;
			if (ti != null)
			{
				checkItemClass = ti.m_class.name;
			}
			TreeInfo tri = info as TreeInfo;
			if (tri != null)
			{
				checkItemClass = tri.m_class.name;
			}
			VehicleInfo vi = info as VehicleInfo;
			if (vi != null)
			{
				checkItemClass = vi.m_class.name;
			}
            itemClassPanel.m_itemClassDropDown.selectedIndex = Array.IndexOf(itemClassPanel.m_itemClassDropDown.items, checkItemClass);
		}

		// Attempts to matches the selected item class name with an actual ItemClass.
		private void ApplyNewItemClass(UIComponent component, UIMouseEventParameter eventParam)
		{
			ref PrefabInfo info = ref m_toolController.m_editPrefabInfo;
			foreach (ItemClassCollection collection in FindObjectsOfType<ItemClassCollection>())
			{
				foreach (ItemClass itemClass in collection.m_classes)
				{
					if (itemClassPanel.m_itemClassDropDown.selectedValue == itemClass.name)
					{
						BuildingInfo bi = info as BuildingInfo;
						if (bi != null)
						{
							bi.m_class = itemClass;
						}
						NetInfo ni = info as NetInfo;
						if (ni != null)
						{
							ni.m_class = itemClass;
						}
						PropInfo pi = info as PropInfo;
						if (pi != null)
						{
							pi.m_class = itemClass;
						}
						TransportInfo ti = info as TransportInfo;
						if (ti != null)
						{
							ti.m_class = itemClass;
						}
						TreeInfo tri = info as TreeInfo;
						if (tri != null)
						{
							tri.m_class = itemClass;
						}
						VehicleInfo vi = info as VehicleInfo;
						if (vi != null)
						{
							vi.m_class = itemClass;
						}
						return;
					}
				}

			}

		}
        private void OnGroupClicked(UIComponent comp, UIMouseEventParameter p)
        {
            UIButton uIButton = p.source as UIButton;
            if (!(uIButton != null) || string.IsNullOrEmpty(uIButton.stringUserData))
            {
                return;
            }
            if (!ItemClassGroupContianerPanel.m_GroupStates.TryGetValue("ItemClassGroup", out var groupInfo))
            {
                return;
            }
            uIButton.normalBgSprite = ((!groupInfo.m_Folded) ? "PropertyGroupClosed" : "PropertyGroupOpen");
            if (groupInfo.m_Folded)
            {
                UIPanel propertyContainer = groupInfo.m_PropertyContainer;
                propertyContainer.Show();
                float endValue = CalculateHeight(propertyContainer);
                ValueAnimator.Animate("PropGroup" + uIButton.stringUserData, delegate (float val)
                {
                    Vector2 size2 = groupInfo.m_Container.size;
                    size2.y = val;
                    groupInfo.m_Container.size = size2;
                }, new AnimatedFloat(UITemplateManager.Peek("GroupPropertySet").size.y, endValue, 0.2f));
            }
            else
            {
                UIPanel container = groupInfo.m_PropertyContainer;
                float startValue = CalculateHeight(container);
                ValueAnimator.Animate("PropGroup" + uIButton.stringUserData, delegate (float val)
                {
                    Vector2 size = groupInfo.m_Container.size;
                    size.y = val;
                    groupInfo.m_Container.size = size;
                }, new AnimatedFloat(startValue, UITemplateManager.Peek("GroupPropertySet").size.y, 0.2f), delegate
                {
                    container.Hide();
                });
            }
            groupInfo.m_Folded = !groupInfo.m_Folded;
            ItemClassGroupContianerPanel.m_GroupStates[uIButton.stringUserData] = groupInfo;
        }

        private float CalculateHeight(UIPanel container)
        {
            float num = 0f;
            num += UITemplateManager.Peek("GroupPropertySet").size.y;
            return num + CalculatePropertiesHeight(container);
        }

        private float CalculatePropertiesHeight(UIPanel comp)
        {
            float num = 0f;
            for (int i = 0; i < comp.childCount; i++)
            {
                num += comp.components[i].size.y + (float)comp.autoLayoutPadding.vertical;
            }
            return num;
        }


    }
}