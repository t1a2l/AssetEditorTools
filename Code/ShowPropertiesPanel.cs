using ColossalFramework.UI;
using AssetEditorTools.UI;
using UnityEngine;

namespace AssetEditorTools
{
	public class ShowPropertiesPanel : UIPanel
	{
		public UIButton m_showPropertiesButton;

		public override void Start() 
		{ 
			base.Start();
			var view = UIView.GetAView();

			backgroundSprite = "SubcategoriesPanel";
			clipChildren = true;

			width = 393;
			height = 25;

			m_showPropertiesButton = UIButtons.AddButton(this, 150.0f, 0.0f, "Hide Properties", 150.0f, 25.0f, 0.9f, 4, "Properties");

			UIComponent bulldozerButton = view.FindUIComponent<UIComponent>("MarqueeBulldozer");
            UIComponent moveitButton = view.FindUIComponent<UIComponent>("MoveIt");
            if(moveitButton == null)
			{
                if (bulldozerButton == null)
                {
                    bulldozerButton = view.FindUIComponent<UIComponent>("BulldozerButton");
                }
				this.absolutePosition = new Vector2(bulldozerButton.absolutePosition.x - width - 5, bulldozerButton.parent.absolutePosition.y);
			}
            else
			{
                this.absolutePosition = new Vector2(moveitButton.absolutePosition.x - width - 5, moveitButton.absolutePosition.y);
			}

			gameObject.AddComponent<ShowPropertiesEditorController>();
		}

	}
}
