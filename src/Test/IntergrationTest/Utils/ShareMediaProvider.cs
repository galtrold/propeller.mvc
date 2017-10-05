using NSubstitute;
using Sitecore.FakeDb.Resources.Media;
using Sitecore.Resources.Media;

namespace IntergrationTest.Utils
{
    public class ShareMediaProvider
    {

        public static MediaProviderSwitcher MediaProvider()
        {
            var mediaProvider = Substitute.For<Sitecore.Resources.Media.MediaProvider>();
            mediaProvider.GetMediaUrl(Arg.Is<Sitecore.Data.Items.MediaItem>(p => true)).Returns(SharedDatabaseDefinition.StaticVehichleData.Photo.Url);
            return new Sitecore.FakeDb.Resources.Media.MediaProviderSwitcher(mediaProvider);
            
        }
    }
}