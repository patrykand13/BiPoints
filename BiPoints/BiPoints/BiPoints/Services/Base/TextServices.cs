using BiPoints.Interfaces.Base;
using BiPoints.Resx;
using System.Globalization;
using System.Resources;

namespace BiPoints.Services.Base
{
    class TextServices : ITextServices
    {
        ResourceManager myManager = new ResourceManager(typeof(AppResources));
        public string GetString(string name)
        {
            try
            {
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                return myManager.GetString(name, currentCulture);
            }
            catch { return name; }
        }
    }
}
