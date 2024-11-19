using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace RippleEffect.Shaders
{
    public class RippleEffect : ShaderEffect
    {
        private static readonly PixelShader _pixelShader;

        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty(nameof(Input), typeof(RippleEffect), 0);
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register(nameof(Center), typeof(Point), typeof(RippleEffect), new PropertyMetadata(new Point(0.5, 0.5), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register(nameof(Radius), typeof(double), typeof(RippleEffect), new PropertyMetadata(0.3, PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register(nameof(Brightness), typeof(double), typeof(RippleEffect), new PropertyMetadata(0.4, PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty AspectRatioProperty =
            DependencyProperty.Register(nameof(AspectRatio), typeof(double), typeof(RippleEffect), new PropertyMetadata(1.0, PixelShaderConstantCallback(3)));

        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }
        public Point Center
        {
            get => (Point)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }
        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }
        public double Brightness
        {
            get => (double)GetValue(BrightnessProperty);
            set => SetValue(BrightnessProperty, value);
        }
        public double AspectRatio
        {
            get => (double)GetValue(AspectRatioProperty);
            set => SetValue(AspectRatioProperty, value);
        }

        static RippleEffect()
        {
            _pixelShader = new PixelShader();
            var asm = Assembly.GetExecutingAssembly();
            using (Stream stream = asm.GetManifestResourceStream("RippleEffect.Shaders.RippleEffect.ps")) {
                _pixelShader.SetStreamSource(stream);
            }
        }

        public RippleEffect()
        {
            PixelShader = _pixelShader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(CenterProperty);
            UpdateShaderValue(RadiusProperty);
            UpdateShaderValue(BrightnessProperty);
            UpdateShaderValue(AspectRatioProperty);
        }
    }
}
