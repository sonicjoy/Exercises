using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaylistViewer.Controllers;
using PlaylistViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistViewer.Controllers.Tests
{
    [TestClass()]
    public class PlaylistControllerTests
    {
        static string url = "http://harry.radioapi.io/services/nowplaying/utv/fm104/onair";
        PlaylistController playlistCtrl = new PlaylistController();
        List<PlayoutItem> playlist;

        [TestInitialize]
        [TestMethod()]
        public void GetPlayListTest()
        {
            playlist = playlistCtrl.Get(url);
            Assert.IsNotNull(playlist);
            Assert.IsInstanceOfType(playlist, typeof(List<PlayoutItem>));
        }

        [TestMethod]
        public void PlayoutItemMappingTest()
        {
            Assert.IsTrue(playlist.Any());
            Assert.IsInstanceOfType(playlist.FirstOrDefault(), typeof(PlayoutItem));
        }

        [TestMethod]
        public void Times_Of_All_Item_Are_Utc()
        {
            Assert.IsFalse(playlist.Any(i => i.Time.Kind != DateTimeKind.Utc));
        }

        [TestMethod]
        public void There_Shouldnt_Be_Any_Items_That_Are_Playing_But_Their_Finish_Times_Are_In_The_Past()
        {
            Assert.IsFalse(playlist.Any(i => i.Status == "playing" && i.Time.Add(i.Duration) <= DateTime.UtcNow));
        }

        [TestMethod]
        public void Any_Items_With_Future_Time_Shouldnt_Have_Status_Playing_Or_History()
        {
            Assert.IsFalse(playlist.Any(i => i.Time > DateTime.UtcNow && (i.Status == "playing" || i.Status == "history")));
        }

        [TestMethod]
        public void Empty_Image_Urls_Are_Marked_As_Bad_Image()
        {
            playlist[0].ImageUrl = string.Empty;
            Assert.AreEqual(PlayoutItem.BAD_IMAGE, playlist[0].ImageUrl);
        }

        [TestMethod]
        public void Unresolvable_Image_Urls_Are_Marked_As_Bad_Image()
        {
            playlist[0].ImageUrl = "XYZ";
            Assert.AreEqual(PlayoutItem.BAD_IMAGE, playlist[0].ImageUrl);
        }

        [TestMethod]
        public void Uris_That_Are_Not_Image_Urls_Are_Marked_As_Bad_Image()
        {
            playlist[0].ImageUrl = "http://google.com";
            Assert.AreEqual(PlayoutItem.BAD_IMAGE, playlist[0].ImageUrl);
        }
    }
}