using BashBook.Utility;
using log4net;

namespace BashBook.BAL
{
    public class BaseBusinessAccessLayer
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public CloudinaryCdn.CloudinaryImageSaving Cdn => new CloudinaryCdn.CloudinaryImageSaving();

        public CloudinaryCdn.CloudinaryImageSaving Cdn
        {
            get
            {
                return new Utility.CloudinaryCdn.CloudinaryImageSaving();
            }
        }
    }
}
