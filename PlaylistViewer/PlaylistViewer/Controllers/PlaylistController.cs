using PlaylistViewer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PlaylistViewer.Controllers
{
    public class PlaylistController : ApiController
    {
        [ResponseType(typeof(List<PlayoutItem>))]
        public List<PlayoutItem> Get(string url)
        {
            var playoutItemList = new List<PlayoutItem>();
            try
            {
                var xmlData = XDocument.Load(@url);
                foreach (var playoutItemXml in xmlData.Descendants("playoutItem"))
                {
                    var playoutItem = new PlayoutItem();
                    playoutItem.Artist = playoutItemXml.Attribute("artist").Value;
                    var duration = new TimeSpan();
                    TimeSpan.TryParse(playoutItemXml.Attribute("duration").Value, CultureInfo.InvariantCulture, out duration);
                    playoutItem.Duration = duration;
                    playoutItem.ImageUrl = playoutItemXml.Attribute("imageUrl").Value;
                    playoutItem.Status = playoutItemXml.Attribute("status").Value;
                    var time = new DateTime();
                    DateTime.TryParse(playoutItemXml.Attribute("time").Value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out time);
                    playoutItem.Time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                    playoutItem.Title = playoutItemXml.Attribute("title").Value;
                    playoutItem.Type = playoutItemXml.Attribute("type").Value;
                    playoutItemList.Add(playoutItem);
                }
            }
            catch(FileNotFoundException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Invalid url: {0}", url)),
                    ReasonPhrase = "Invalid Url"
                };
                throw new HttpResponseException(resp);
            }
            catch (NullReferenceException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Xml document is invalid at the url: {0}", url)),
                    ReasonPhrase = "Invalid Xml"
                };
                throw new HttpResponseException(resp);
            }
            return playoutItemList;
        }

        /// <summary>
        /// For UI test Get with default url and modified playout items
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(List<PlayoutItem>))]
        public List<PlayoutItem> Get()
        {
            var url = "http://harry.radioapi.io/services/nowplaying/utv/fm104/onair";
            var playoutItemList = new List<PlayoutItem>();
            var xmlData = XDocument.Load(@url);
            try
            {
                foreach (var playoutItemXml in xmlData.Descendants("playoutItem"))
                {
                    var playoutItem = new PlayoutItem();
                    playoutItem.Artist = playoutItemXml.Attribute("artist").Value;
                    var duration = new TimeSpan();
                    TimeSpan.TryParse(playoutItemXml.Attribute("duration").Value, CultureInfo.InvariantCulture, out duration);
                    playoutItem.Duration = duration;
                    playoutItem.ImageUrl = playoutItemXml.Attribute("imageUrl").Value;
                    playoutItem.Status = playoutItemXml.Attribute("status").Value;
                    var time = new DateTime();
                    DateTime.TryParse(playoutItemXml.Attribute("time").Value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out time);
                    playoutItem.Time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                    playoutItem.Title = playoutItemXml.Attribute("title").Value;
                    playoutItem.Type = playoutItemXml.Attribute("type").Value;
                    playoutItemList.Add(playoutItem);
                }

                playoutItemList[1].ImageUrl = "http://google.com";
            }
            catch (NullReferenceException)
            {
                throw;
            }
            return playoutItemList;
        }
    }
}
