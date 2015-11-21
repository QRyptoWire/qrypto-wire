using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Storage;

namespace QRyptoWire.App.WPhone.Utilities
{
    public static class PictureLibraryTools
    {
        public static async Task SaveWriteableBitmap(WriteableBitmap wb, string name)
        {
            var storageFolder = KnownFolders.SavedPictures;
            var file = await storageFolder.CreateFileAsync(name, CreationCollisionOption.GenerateUniqueName);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                wb.SaveJpeg(stream, wb.PixelWidth, wb.PixelHeight, 0, 100);
            }
        }
    }
}
