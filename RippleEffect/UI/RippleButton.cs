using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RippleEffect.UI
{
    public class RippleButton : Button
    {
        private Border _border;
        private Shaders.RippleEffect _ripple;
        private static DoubleAnimation _anim;

        static RippleButton()
        {
            _anim = new DoubleAnimation();
            _anim.EasingFunction = new CubicEase();
            _anim.From = 0.0;
            _anim.To = 1.0;
            _anim.Duration = TimeSpan.FromSeconds(2.0);
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleButton),
                new FrameworkPropertyMetadata(typeof(RippleButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_Border") is Border border) {
                _border = border;
            }

            if (GetTemplateChild("PART_RippleEffect") is Shaders.RippleEffect ripple) {
                _ripple = ripple;
                _ripple.Radius = 0.0;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            Point mousePos = Mouse.GetPosition(_border);
            double width = mousePos.X / _border.ActualWidth;
            double height = mousePos.Y / _border.ActualHeight;
            _ripple.Center = new Point(width, height);
            _ripple.AspectRatio = _border.ActualWidth / _border.ActualHeight;
            _ripple.BeginAnimation(Shaders.RippleEffect.RadiusProperty, _anim);
        }
    }
}
