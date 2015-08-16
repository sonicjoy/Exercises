using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PlaylistViewer.Models
{
    [XmlRoot("playoutItem")]
    public class PlayoutItem
    {
        static readonly string INVALID = "invalid";
        static readonly string PLAYING = "playing";
        static readonly string HISTORY = "history";
        public static readonly string BAD_IMAGE = "bad-image";

        public string Artist { get; set; }

        [DataType(DataType.Duration)]
        [DisplayFormat(DataFormatString ="{0:t}")]
        public TimeSpan Duration { get; set; }

        private string _imageUrl;
        [Url]
        [DisplayName("Image Url")]
        public string ImageUrl
        {
            get
            {
                if (!Uri.IsWellFormedUriString(_imageUrl, UriKind.Absolute)) _imageUrl = BAD_IMAGE;
                else
                {
                    var url = new Uri(_imageUrl);
                    if (!url.IsImage()) _imageUrl = BAD_IMAGE;
                }
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
            }
        }

        private string _status;
        public string Status
        {
            get
            {
                if ((Time.Add(Duration) <= DateTime.UtcNow || Time > DateTime.UtcNow) && _status == PLAYING
                    || (Time > DateTime.UtcNow && _status == HISTORY))
                    _status = INVALID;

                return _status;
            }
            set
            {
                _status = value;
            }
        }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime Time { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }
    }

    public static class UriExtension
    {
        public static bool IsImage(this Uri uri)
        {
            var uriString = uri.AbsoluteUri;
            if (uriString.EndsWith(".jpg") || uriString.EndsWith(".png") || uriString.EndsWith(".gif") || uriString.EndsWith(".jpeg")
                || uriString.EndsWith(".tif") || uriString.EndsWith(".bmp"))
                return true;
            else
                return false;
        }
    }
}