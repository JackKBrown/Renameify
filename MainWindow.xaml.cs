using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace renameify
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		#region UtilityFunctions

		bool ValidateInput()
		{
			if (!IsValidFilename(PrependTextBox.Text))
			{
				MessageBox.Show("Illegal character in prepend text box");
				return false;
			}
			if (!IsValidFilename(PrependTextBox.Text))
			{
				MessageBox.Show("Illegal character in the append text box");
				return false;
			}
			return true;
		}

		bool IsValidFilename(string testName)
		{
			Console.WriteLine(System.IO.Path.GetInvalidPathChars());
			Regex containsABadCharacter = new Regex("[" + Regex.Escape(new string(System.IO.Path.GetInvalidPathChars())) + "/\\:"+ "]");
			if (containsABadCharacter.IsMatch(testName)) { return false; };

			return true;
		}

		private string RenameItem(string filepath)
		{
			string fDir = Path.GetDirectoryName(filepath);
			string fName = Path.GetFileNameWithoutExtension(filepath);
			string fExt = Path.GetExtension(filepath);
			return Path.Combine(fDir, String.Concat(PrependTextBox.Text, fName, AppendTextBox.Text, fExt));
		}

		private void DirRename(string sDir)
		{
			if (!ValidateInput())
			{
				return;
			}
			//TODO need to make sure this works properly for all characters

			//Currently thrown away may want to return them
			List<String> oldfilenames = new List<String>();
			List<String> newfilenames = new List<String>();
			try
			{
				foreach (string f in Directory.GetFiles(sDir))
				{
					oldfilenames.Add(f);
					File.Move(f, RenameItem(f));
					newfilenames.Add(f);
				}
				foreach (string d in Directory.GetDirectories(sDir))
				{
					//recursive, if I'm going to return filenames need to add this in here
					DirRename(d);
				}
			}
			catch (System.Exception excpt)
			{
				MessageBox.Show(excpt.Message);
			}
		}

		private List<String> DirSearch(string sDir)
		{
			List<String> files = new List<String>();

			if (!ValidateInput())
			{
				return files;
			}

			try
			{
				//add every file in dir
				foreach (string f in Directory.GetFiles(sDir))
				{
					files.Add(RenameItem(f));
				}
				//recursive add every file in subdirs
				foreach (string d in Directory.GetDirectories(sDir))
				{
					files.AddRange(DirSearch(d));
				}
			}
			catch (System.Exception excpt)
			{
				MessageBox.Show(excpt.Message);
			}

			return files;
		}

		#endregion

		#region xamlfunctions
		private void LoadPath_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
			CommonFileDialogResult result = dialog.ShowDialog();
			if (result == CommonFileDialogResult.Ok)
			{
				FilePathTextBox.Text = dialog.FileName;
			}
			else if (result == CommonFileDialogResult.Cancel)
			{
				FilePathTextBox.Text = "";
			}


		}
		private void LoadFiles_Click(object sender, RoutedEventArgs e)
		{
			if (Directory.Exists(FilePathTextBox.Text))
			{
				List<String> files = DirSearch(FilePathTextBox.Text);
				lbFiles.Items.Clear();
				foreach (string filename in files)
					lbFiles.Items.Add(filename);
			}

		}
		private void SaveFiles_Click(object sender, RoutedEventArgs e)
		{
			if (Directory.Exists(FilePathTextBox.Text))
			{
				DirRename(FilePathTextBox.Text);
				MessageBox.Show("Renamed files");
			}
		}
		#endregion

	}
}
