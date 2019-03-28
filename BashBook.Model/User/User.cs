using System.Collections.Generic;
using BashBook.Model.Global;
using BashBook.Model.Group;

namespace BashBook.Model.User
{
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int GenderId { get; set; }
    }
    public class UserGeneralViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
    public class UserModel
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int GenderId { get; set; }
        public long DateOfBirth { get; set; }
    }

    public class UserPreviewModel
    {
        public int UserId { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserRegisterModel
    {
        //public string UserName { get; set; }
        public string Email { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }
    public class UserRegisterValidationModel
    {
        public int UserId { get; set; }
        //public bool UserName { get; set; }
        public bool Email { get; set; }
        public bool Mobile { get; set; }
    }

    public class UserActivityModel
    {
        public List<EntityPreviewModel> Contacts { get; set; }
        public List<GroupPreviewModel> Groups { get; set; }
        public List<EntityPreviewModel> Events { get; set; }
    }

    public class EmailModel
    {
        public string Email { get; set; }
    }
}
