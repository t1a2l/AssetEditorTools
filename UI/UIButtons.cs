using ColossalFramework.UI;
using UnityEngine;

namespace AssetEditorTools.UI
{
    /// <summary>
    /// UI buttons.
    /// </summary>
    public static class UIButtons
    { 
        /// <summary>
        /// Adds a simple pushbutton.
        /// </summary>
        /// <param name="parent">Parent component.</param>
        /// <param name="posX">Relative X postion.</param>
        /// <param name="posY">Relative Y position.</param>
        /// <param name="text">Button text.</param>
        /// <param name="width">Button width (default 200).</param>
        /// <param name="height">Button height (default 30).</param>
        /// <param name="scale">Text scale (default 0.9).</param>
        /// <param name="vertPad">Vertical text padding within button (default 4).</param>
        /// <param name="tooltip">Tooltip, if any.</param>
        /// <returns>New pushbutton.</returns>
        public static UIButton AddButton(UIComponent parent, float posX, float posY, string text, float width = 200f, float height = 30f, float scale = 0.9f, int vertPad = 4, string tooltip = null)
        {
            UIButton button = parent.AddUIComponent<UIButton>();

            // Size and position.
            button.size = new Vector2(width, height);
            button.relativePosition = new Vector2(posX, posY);

            // Appearance.
            button.font = UIFonts.SemiBold;
            button.textScale = scale;
            button.atlas = UITextures.InGameAtlas;
            button.normalBgSprite = "ButtonWhite";
            button.hoveredBgSprite = "ButtonWhite";
            button.focusedBgSprite = "ButtonWhite";
            button.pressedBgSprite = "ButtonWhitePressed";
            button.disabledBgSprite = "ButtonWhiteDisabled";
            button.color = Color.white;
            button.focusedColor = Color.white;
            button.hoveredColor = Color.white;
            button.pressedColor = Color.white;
            button.textColor = Color.black;
            button.pressedTextColor = Color.black;
            button.focusedTextColor = Color.black;
            button.hoveredTextColor = Color.blue;
            button.disabledTextColor = Color.grey;
            button.canFocus = false;

            // Add tooltip.
            if (tooltip != null)
            {
                button.tooltip = tooltip;
            }

            // Text.
            button.textScale = scale;
            button.textPadding = new RectOffset(0, 0, vertPad, 0);
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
            button.text = text;

            return button;
        }
    } 
}
