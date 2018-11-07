namespace ResourceManager
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Media;
    using System.Windows.Forms;

    using ResourceManager.Properties;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            listBox1.DisplayMember = "Key";
            foreach (DictionaryEntry resourceEntry in Resources.ResourceManager.GetResourceSet(
                CultureInfo.CurrentUICulture, true, true))
            {
                listBox1.Items.Add(resourceEntry);
            }
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = listBox1;
            object selectedItem = ((DictionaryEntry)listBox.SelectedItem).Value;
            switch (selectedItem.GetType().Name)
            {
                case "Bitmap":
                    pictureBox1.Image = (Image)((DictionaryEntry)listBox.SelectedItem).Value;
                    break;
                case "Icon":
                    pictureBox1.Image = ((Icon)((DictionaryEntry)listBox.SelectedItem).Value).ToBitmap();
                    break;
                case "String":
                    textBox1.Text = (string)selectedItem;
                    break;
                case "UnmanagedMemoryStream":
                    try
                    {
                        UnmanagedMemoryStream sound = (UnmanagedMemoryStream)selectedItem;
                        SoundPlayer player = new SoundPlayer();
                        sound.Position = 0;
                        player.Stream = null;
                        player.Stream = sound;
                        player.Play();
                    }
                    catch
                    {
                        Console.Write("Wav dosyası çalınırken hata oluştu.");
                    }

                    break;
                default:
                    textBox1.Text = string.Format("Bilinmeyen tür: {0}", selectedItem.GetType().Name);
                    break;
            }
        }
    }
}