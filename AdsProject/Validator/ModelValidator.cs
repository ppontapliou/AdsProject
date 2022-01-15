using Models.Models;
using System;

namespace Validator
{
    public static class ModelValidator
    {
        //checking values without if
        public static bool IsValid(this Ad ad)
        {
            return !(!ad.IsValidPameter()||
                string.IsNullOrEmpty(ad.Title) || string.IsNullOrEmpty(ad.Address) || string.IsNullOrEmpty(ad.Image));
        }

        public static bool IsValidPameter(this Ad ad)
        {
            return !(ad is null ||
              string.IsNullOrEmpty(ad.Name) || ad.Name.Length < ValidConst.AdNameMinLenght || ad.Name.Length > ValidConst.AdNameMaxLenght ||
              ad.Category < 1 ||
              !Enum.IsDefined(typeof(Types), ad.Type) ||
              !Enum.IsDefined(typeof(States), ad.State));
        }

        public static bool IsValid(this Parameter parameter)
        {
            return !(parameter is null || string.IsNullOrEmpty(parameter.Name) ||
                   parameter.Name.Length < ValidConst.CategoryNameMinLenght || parameter.Name.Length > ValidConst.CategoryNameMaxLenght);
        }

        public static bool IsValid(this User user)
        {
            return !(user is null ||  user.Mail is null && user.Phone is null ||
                string.IsNullOrEmpty(user.UserName) || user.UserName.Length < ValidConst.UserNameMinLenght || user.UserName.Length > ValidConst.UserNameMaxLenght ||
                string.IsNullOrEmpty(user.Mail) || user.Mail.Length < ValidConst.MailMinLenght || user.Mail.Length > ValidConst.MailMaxLenght ||
                string.IsNullOrEmpty(user.Phone) || user.Phone.Length < ValidConst.PhoneMinLenght || user.Phone.Length > ValidConst.PhoneMaxLenght ||
                !Enum.IsDefined(typeof(Roles), user.Role) ||
                string.IsNullOrEmpty(user.Login) || user.Login.Length < ValidConst.LoginMinLenght || user.Login.Length > ValidConst.LoginMaxLenght );
        }

        public static bool IsValidPassword(this User user)
        {
            return !(user is null ||
                string.IsNullOrEmpty(user.Password) || user.Password.Length < ValidConst.PasswordMinLenght || user.Password.Length > ValidConst.PasswordMaxLenght);
        }
    }
}
