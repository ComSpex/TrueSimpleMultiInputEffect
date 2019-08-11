using SimpleMultiInputEffectShader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Windows.Markup;
using System.Windows.Interop;

namespace SimpleMultiInputEffectSample {
	/// <summary>
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class Window3:Window {
		protected SimpleMultiInputEffect eff;
		//protected EffectConverter eco = new EffectConverter();
		public Window3() {
			InitializeComponent();
			eff=new SimpleMultiInputEffect();
			eff.Input2=new ImageBrush(new BitmapImage(new Uri(@"J:\Yosuke\Pictures\Folders\My Faces\Vogue Logo.png"))) as Brush;
			Binding bind = new Binding();
			bind.Source=slider1;
			bind.Path=new PropertyPath(Slider.ValueProperty);
			//bind.Converter=eco;
			MyFace.Effect=eff;
			MyFace.SetBinding(SimpleMultiInputEffect.MixInAmountProperty,bind);
			//label1.SetBinding(Label.ContentProperty,bind);
			slider1.ValueChanged+=(sender,e) => {
				//Since Binding does not work, this handler has been added.
				eff.MixInAmount=e.NewValue;
			};
			Update();
		}
		private void Update() {
			int length = eff.CurrentName.Segments.Length;
			nameOf.Text=eff.CurrentName.Segments[length-1];
		}
		private void button_Click(object sender,RoutedEventArgs e) {
			Button bu = sender as Button;
			if(bu.Name=="buttonR") {
				eff.Next();
			} else {
				eff.Prev();
			}
			Update();
		}
		static protected int rotationIndex = 0;
		private void MenuItem_Click(object sender,RoutedEventArgs e) {
			MenuItem mi = sender as MenuItem;
			if((string)mi.Header=="_File") {
				e.Handled=true;
				return;
			}
			if((string)mi.Header=="_Edit") {
				e.Handled=true;
				return;
			}
			string selected = (string)mi.Header;
			string pathMyFace= String.Format(@"{0}\Folders\My Faces",Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
			//MessageBox.Show(String.Format("Hello from {0}",selected));
			OpenFileDialog diag = new OpenFileDialog();
			diag.InitialDirectory=pathMyFace;
			if(selected.Contains("Logo")) {
				diag.Title="Logo";
			} else if(selected.Contains("Face")) {
				diag.Title="Face";
			} else if(selected.Contains("Save")) {
				try {
					string filename = String.Format(@"{0}\MyFace{1:ddMMMyyyyhhmmss}.png",pathMyFace,DateTime.Now);
					double dpiX=96, dpiY=96;
					{
						PresentationSource source = PresentationSource.FromVisual(this);
						if(source != null) {
							dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
							dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
						}
					}
					SaveImage(this,MyFace,(int)dpiX,(int)dpiY,filename);
					MessageBox.Show("Successfuly saved!!",this.Title,MessageBoxButton.OK,MessageBoxImage.Information);
				}catch(Exception ex) {
					MessageBox.Show(ex.Message,this.Title,MessageBoxButton.OK,MessageBoxImage.Warning);
				}
				return;
			} else if(selected.Contains("Edit")) {
				return;
			} else if(selected.Contains("Q")) {
				if(++rotationIndex>3) {
					rotationIndex=0;
				}
				DoRotate();
				return;
			} else if(selected.Contains("P")){
				if(--rotationIndex<0) {
					rotationIndex=3;
				}
				DoRotate();
				return;
			}
			if(diag.ShowDialog()==true) {
				if(diag.Title=="Logo") {
					eff.Input2=new ImageBrush(new BitmapImage(new Uri(diag.FileName))) as Brush;
				} else {
					BitmapImage newbi = new BitmapImage(new Uri(diag.FileName));
					//newbi.CreateOptions=BitmapCreateOptions.IgnoreImageCache;
					MyFace.Source=newbi;
				}
			}
		}
		private void DoRotate() {
			BitmapImage bi = new BitmapImage();
			bi.BeginInit();
			bi.UriSource=new Uri(MyFace.Source.ToString());
			bi.Rotation=(Rotation)rotationIndex;
			bi.EndInit();
			MyFace.Source=bi;
		}
		public static void SaveImage(Window window,Image image,int dpiX,int dpiY,string filename) {
			Size size = new Size(window.Width,window.Height);
			image.Measure(size);
			var rtb = new RenderTargetBitmap(
				(int)window.Width, //width 
				(int)window.Height, //height 
				dpiX, //dpi x 
				dpiY, //dpi y 
				PixelFormats.Pbgra32 // pixelformat 
			);
			rtb.Render(image);
			SaveRTBAsPNG(rtb,filename);
		}
		private static void SaveRTBAsPNG(RenderTargetBitmap bmp,string filename) {
			var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
			enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));
			using(var stm = System.IO.File.Create(filename)) {
				enc.Save(stm);
			}
		}
	}
	public class EffectConverter:IValueConverter {
		object IValueConverter.Convert(object value,Type targetType,object parameter,CultureInfo culture) {
			return value;
		}
		object IValueConverter.ConvertBack(object value,Type targetType,object parameter,CultureInfo culture) {
			return DependencyProperty.UnsetValue;
		}
	}
}
