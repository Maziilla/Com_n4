using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using System.Drawing;

namespace PlugIn
{
    public class ReverseTransform : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Переворот изображения";
            }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;

            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height / 2; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                    bitmap.SetPixel(i, bitmap.Height - j - 1, color);
                }

            app.Image = bitmap;
        }
    }
    public class RandomTransform : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Случайная трансформация";
            }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Random rand = new Random(DateTime.Now.Millisecond);
            int pixels = (int)(0.1 * bitmap.Width * bitmap.Height);

            for (int i = 0; i < pixels; ++i)
                bitmap.SetPixel(rand.Next(bitmap.Width - 1), rand.Next(bitmap.Height), Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));

            app.Image = bitmap;
        }
    }
    public class Zerkalka : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Зеркалка";
            }
        }

        public string Version
        {
            get
            {
                return "5.0";
            }
        }

        public string Author
        {
            get
            {
                return "Ага";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Bitmap newbitmap = app.Image;
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    newbitmap.SetPixel(bitmap.Width - i - 1, j, color);
                }

            app.Image = bitmap;
        }
    }
    public class ReversZerk : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Другая зеркалка";
            }
        }

        public string Version
        {
            get
            {
                return "3.0";
            }
        }

        public string Author
        {
            get
            {
                return "Ага";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Bitmap newbitmap = app.Image;
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(bitmap.Width - i - 1, j);
                    newbitmap.SetPixel(i, j, color);
                }

            app.Image = bitmap;
        }
    }
    public class PolnoeZerkalnoe : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Зеркальное отражение";
            }
        }

        public string Version
        {
            get
            {
                return "3.0";
            }
        }

        public string Author
        {
            get
            {
                return "Ага";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Bitmap newbitmap =(Bitmap) bitmap.Clone();
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(bitmap.Width - i - 1, j);
                    newbitmap.SetPixel(i, j, color);
                }

            app.Image = newbitmap;
        }
    }
    public class Ungle90 : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Поворот на 90";
            }
        }

        public string Version
        {
            get
            {
                return "3.0";
            }
        }

        public string Author
        {
            get
            {
                return "Ага";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Bitmap newbitmap = new Bitmap(bitmap.Height,bitmap.Width);
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    Color color = bitmap.GetPixel(bitmap.Width - i-1, j);
                    newbitmap.SetPixel(j, i, color);
                }

            app.Image = newbitmap;
        }
    }
}
