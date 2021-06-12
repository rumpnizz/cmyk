using System.Drawing;
using System.Drawing.Imaging;

namespace Cmyk.Web.Helpers
{
    public static class ImageHelpers
    {
        // http://www.graphicsmill.com/docs/gm/P_Aurigma_GraphicsMill_PixelFormat_Format32bppCmyk.htm
        // https://www.winehq.org/pipermail/wine-patches/2012-July/116377.html
        // A PixelFormat representing 32 bits per pixel CMYK color
        private const int _Format32bppCmyk = (15 | 32 << 8);

        public static bool IsCmyk(this Image image)
        {
            var flags = (ImageFlags)image.Flags;
            if (flags.HasFlag(ImageFlags.ColorSpaceCmyk) || flags.HasFlag(ImageFlags.ColorSpaceYcck))
                return true;

            return (int)image.PixelFormat == _Format32bppCmyk;
        }
    }
}