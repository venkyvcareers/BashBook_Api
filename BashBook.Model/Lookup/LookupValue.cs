namespace BashBook.Model.Lookup
{


    public class Lookups
    {
        public enum Parents
        {
            EntityType = 1,
            ContentType,
            EventType,
            ContactStatus,
            GenderOption,
            PollSelectionTypes,
            PollOptionTypes,
            GroupRoles,
            TossWinnerSelection,
            WinningSideType,
            YesOrNo,
            FisrtInningsTotalRange,
            Country,
            PlayerRole,
            MatchStatus,
            PredictionCategoryType,
            QuestionOptionsType,
            LookupDisplayType
        }

        public enum EntityTypes
        {
            User = 101,
            Group,
            Event
        }

        public enum ContentTypes
        {
            Image = 201,
            Audio,
            Video,
            Text
        }

        public enum EventTypes
        {
            Birthday = 301,
            Anniversary,
            Invitation,
            Party
        }

        public enum ContactStatuses
        {
            Accepted = 401,
            Rejected,
            Ignored,
            Requested
        }

        public enum GenderOptions
        {
            Male = 501,
            Female,
            NotSpecified
        }

        public enum PollSelectionTypes
        {
            Single = 601,
            Multiple
        }

        public enum PollOptionTypes
        {
            Text = 701,
            Image,
            ImageWithText
        }

        public enum GroupRoles
        {
            User = 801,
            Admin,
        }

        public enum TossWinnerSelection
        {
            Bat = 901,
            Bowl,
        }

        public enum WinningSideType
        {
            BatFirst = 1001,
            Chasing,
        }

        public enum YesOrNo
        {
            Yes = 1101,
            No,
        }

        public enum FisrtInningsTotalRange
        {
            User = 1201,
            Admin,
        }

        public enum Country
        {
            India = 1301,
            Australia,
            England,
            Srilanka,
            Bangladesh,
            WestIndies,
            SouthAfrica,
            NewZealand,
            Pakisthan,
            Afghanistan,
            Nepal,
            Ireland,
            Zimbabwe
        }

        public enum PlayerRole
        {
            Batsman = 1401,
            Bowler,
            AllRounder,
            WicketKeeper
        }

        public enum MatchStatus
        {
            YetToStart = 1501,
            InProgress,
            Completed,
            Canceled,
            Inactive
        }

        public enum PredictionCategoryType
        {
            General = 1601,
            Risk,
            Luck
        }

        public enum QuestionOptionsType
        {
            Team = 1701,
            Player,
            Lookup,
            Number
        }

        public enum LookupDisplayType
        {
            YesOrNo = 1801,
            DropDown
        }
    }
}
