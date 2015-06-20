using System;

namespace Missive.Configuration
{
    public interface ICodec
    {
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class MediaTypeAttribute : Attribute
    {
        public MediaTypeAttribute(string mediaType)
        {
            MediaType = new MediaType(mediaType);
        }

        public MediaType MediaType { get; set; }

    }

    public class MediaType
    {
        public MediaType(string mediaType)
        {
            
        }
    }
}