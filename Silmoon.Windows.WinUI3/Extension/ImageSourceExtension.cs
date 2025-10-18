using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Silmoon.Windows.WinUI3.Extension
{
    public static class ImageSourceExtension
    {
        public static async Task LoadByteArray(this BitmapImage image, byte[] imageData)
        {
            using var stream = new InMemoryRandomAccessStream();
            using (var writer = new DataWriter(stream.GetOutputStreamAt(0)))
            {
                writer.WriteBytes(imageData);
                await writer.StoreAsync();
            }

            await image.SetSourceAsync(stream);
        }
    }
}
