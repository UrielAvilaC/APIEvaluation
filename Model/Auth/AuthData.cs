using Model.ViewModels.Auth;
using Model.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Auth
{
    public class AuthData
    {
        public ResultSet<PersonViewModel> Auth(UserViewModel model)
        {
            var resultSet = new ResultSet<PersonViewModel>();
            try
            {

                using (var data = new CatalogDataEntities())
                {
                    var user = data.Users.FirstOrDefault(x => x.User1 == model.User && x.Password == model.Password);
                    if (user != null)
                    {

                        resultSet.ObjectResult = new PersonViewModel
                        {
                            Name = user.Person1.Name,
                            LastName = user.Person1.LastName,
                            UserName = user.User1,
                            UserId = user.Person.ToString()
                        };
                        resultSet.Success = true;
                    }
                    else
                    {
                        throw new Exception("Usuario y/o Contraseña no Válidos");
                    }
                }
            }
            catch (Exception ex)
            {
                resultSet.ErrorMessage = ex.Message;
                resultSet.Success = false;
            }
            return resultSet;
        }
    }
}
