using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BashBook.Utility
{
    public class CloudinaryCdn
    {
        public class CloudinaryImageSaving
        {
            static readonly Account Account = new Account()
            {
                //venky.email.work

                //Cloud = "cdnprofilemaker",
                //ApiKey = "372528166817614",
                //ApiSecret = "05Yej01457NBBzDZ0uSBcQ7Md98"

                //vbashbook
                Cloud = "bashbook",
                ApiKey = "394476853548153",
                ApiSecret = "2cNheq4yE2IM9DIZQ66Ojw9Bpt0"

            };

            readonly Cloudinary _cloudinary = new Cloudinary(Account);

            public string Base64ToImageUrl(string base64Str)
            {
                if (string.IsNullOrEmpty(base64Str) || base64Str.ToLower().StartsWith("http"))
                {
                    return base64Str;
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(base64Str)
                };
                var uploadResult = _cloudinary.Upload(uploadParams);
                return uploadResult.SecureUri.OriginalString;
            }

            public string LocalPathToVideoUrl(string localPath)
            {
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(localPath)
                };
                var uploadResult = _cloudinary.Upload(uploadParams);
                var videoUrl = uploadResult.SecureUri.OriginalString;

                return videoUrl;
            }
        }
    }
}
