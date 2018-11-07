# ResourceManager
Proje kaynaklarının listbox içinde listelenip kullanılması için örnek proje.

```cs
foreach (DictionaryEntry resourceEntry in Resources.ResourceManager.GetResourceSet(
                CultureInfo.CurrentUICulture, true, true))
```
Kodu ile projede bulunan tüm kaynaklar döndürülür.
```cs
listBox1.Items.Add(resourceEntry);
```
Komutu ile döndürülen kaynaklar liste kutusuna nesne olarak eklenir
```cs
listBox1.DisplayMember = "Key";
```
DictionaryEntry türünden türetilen nesnelerin isimleri Key özelliğinde bulunduğu için ekranda görünen tür bu şekilde tanımlanır.
```cs
object selectedItem = ((DictionaryEntry)listBox1.SelectedItem).Value;
```
Liste kutusunda seçilen nesnenin değeri gerekli kutudan çıkarma (unboxing, cast) yöntemi ile istenilen şekilde kullanılır.

Örnek proje görüntüsü:

![Proje Goruntusu](Resimler/%C3%96rnek%20Proje%20Resmi.png)

Örnek proje kodları:

```cs
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
    object selectedItem = ((DictionaryEntry)listBox1.SelectedItem).Value;
    switch (selectedItem.GetType().Name)
    {
        case "Bitmap":
            pictureBox1.Image = (Image)((DictionaryEntry)listBox1.SelectedItem).Value;
            break;
        case "Icon":
            pictureBox1.Image = ((Icon)((DictionaryEntry)listBox1.SelectedItem).Value).ToBitmap();
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
```
