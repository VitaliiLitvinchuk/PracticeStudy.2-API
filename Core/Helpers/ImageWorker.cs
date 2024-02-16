using System.Drawing;

namespace Core.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImageWorker
    {
        /// <summary>
        /// Додатковий метод для string, який перетворює base64 в байти
        /// </summary>
        public static Bitmap FromBase64StringToImage(this string base64String)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);

            try
            {
                using MemoryStream memoryStream = new MemoryStream(byteBuffer);

                memoryStream.Position = 0;
                Image imgReturn;
                imgReturn = Image.FromStream(memoryStream);
                memoryStream.Close();
                byteBuffer = null;

                return new Bitmap(imgReturn);
            }
            catch { return null; }
        }
        /// <summary>
        /// Додатковий метод для string, який перетворює картинку за шляхом у base64
        /// </summary>
        public static string ImagePathToBase64(this string path)
        {
            try
            {
                return Convert.ToBase64String(File.ReadAllBytes(path));
            }
            catch (Exception)
            {
                return null; 
            }
        }
    }
}
