using Model.ViewModels.Common;
using Model.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ProductData
{
    public class ProductData
    {

        //// GET: api/<ProductController>

        public ResultSet<List<ProductViewModel>> Select(ProductViewModel viewModel)
        {
            var resultSet = new ResultSet<List<ProductViewModel>>();
            try
            {
                using (var data = new CatalogDataEntities())
                {
                    var predicate = data.Products.AsQueryable();
                    if (!string.IsNullOrEmpty(viewModel.Name))
                    {
                        predicate = predicate.Where(x => x.Name.Contains(viewModel.Name));
                    }
                    if (!string.IsNullOrEmpty(viewModel.Code))
                    {
                        predicate = predicate.Where(x => x.Code.Contains(viewModel.Code));
                    }
                    if (!string.IsNullOrEmpty(viewModel?.idEstatus))
                    {
                        var status = Convert.ToInt32(viewModel.idEstatus);
                        predicate = predicate.Where(x => x.Estatus == status);
                    }
                    if (predicate.Count() == 0)
                    {
                        throw new Exception("Sin Registros");
                    }
                    resultSet.ObjectResult = predicate.Select(x => new ProductViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Estatus = new EstatusProduct
                        {
                            Id = x.Estatus,
                            Description = x.Estatu.Description
                        },
                        Name = x.Name,
                        Price = x.Price,
                        RegistrationDate = x.RegistrationDate,
                        RegistrationPerson = new ViewModels.Auth.PersonViewModel
                        {
                            UserId = x.RegistrationPerson.ToString(),
                            Name = x.Person.Name,
                            LastName = x.Person.LastName
                        },
                        Type = x.Type
                    }).ToList();
                    resultSet.Success = true;

                }
            }
            catch (Exception ex)
            {
                resultSet.ErrorMessage = ex.Message;
                resultSet.Success = false;
            }
            return resultSet;
        }
        public ResultSet<bool> Save(ProductViewModel model)
        {
            var resultSet = new ResultSet<bool>();
            try
            {
                using (var data = new CatalogDataEntities())
                {
                    var product = data.Products.FirstOrDefault(x => x.Id == model.Id);
                    if (product == null)
                    {
                        if (data.Products.Any(x => x.Code == model.Code || x.Name == model.Name))
                        {
                            throw new Exception("Registro Duplicado");
                        }
                        var add = data.Products.Add(new Product
                        {
                            Code = model.Code,
                            Name = model.Name,
                            Price = model.Price,
                            RegistrationPerson = Convert.ToInt32(model.RegistrationPerson.UserId),
                            RegistrationDate = DateTime.Now,
                            Estatus = 1,
                            Type = model.Type

                        });
                    }
                    else
                    {
                        product.Code = model.Code;
                        product.Name = model.Name;
                        product.Price = model.Price;
                        product.UpdatePerson = Convert.ToInt32(model.RegistrationPerson.UserId);
                        product.UpdateDate = DateTime.Now;
                        product.Type = model.Type;
                    }

                    data.SaveChanges();

                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.ErrorMessage = ex.Message;
                resultSet.Success = false;
            }
            return resultSet;
        }
        public ResultSet<bool> Delete(int id, int UserId)
        {
            var resultSet = new ResultSet<bool>();
            try
            {
                using (var data = new CatalogDataEntities())
                {
                    var product = data.Products.FirstOrDefault(x => x.Id == id);
                    if (product == null)
                    {
                        throw new Exception("No se Encontro Registro");
                    }
                    product.Estatus = 3;
                    product.UpdatePerson = UserId;
                    product.UpdateDate = DateTime.Now;
                    data.SaveChanges();
                    resultSet.ObjectResult = true;
                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.ErrorMessage = ex.Message;
                resultSet.Success = false;
            }
            return resultSet;
        }
        public ResultSet<bool> Recover(int id, int UserId)
        {
            var resultSet = new ResultSet<bool>();
            try
            {
                using (var data = new CatalogDataEntities())
                {
                    var product = data.Products.FirstOrDefault(x => x.Id == id);
                    if (product == null)
                    {
                        throw new Exception("No se Encontro Registro");
                    }
                    product.Estatus = 1;
                    product.UpdatePerson = UserId;
                    product.UpdateDate = DateTime.Now;
                    data.SaveChanges();
                    resultSet.ObjectResult = true;
                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.ErrorMessage = ex.Message;
                resultSet.Success = false;
            }
            return resultSet;
        }
        public ResultSet<ProductViewModel> ViewRecord(int id)
        {
            var resultSet = new ResultSet<ProductViewModel>();
            try
            {
                using (var data = new CatalogDataEntities())
                {
                    var product = data.Products.FirstOrDefault(x => x.Id == id);
                    if (product == null)
                    {
                        throw new Exception("No se Encontro Registro");
                    }
                    resultSet.ObjectResult = new ProductViewModel
                    {
                        Id = product.Id,
                        Code = product.Code,
                        Name = product.Name,
                        Price = product.Price,
                        RegistrationDate = product.RegistrationDate,
                        Type = product.Type,
                        Estatus = new EstatusProduct
                        {
                            Id = product.Estatu.Id,
                            Description = product.Estatu.Description
                        },
                        RegistrationPerson = new ViewModels.Auth.PersonViewModel
                        {
                            Name = product.Person.Name,
                            LastName = product.Person.LastName,
                            UserId = product.Person.Id.ToString()

                        }
                    };
                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.ErrorMessage = ex.Message;
                resultSet.Success = false;
            }
            return resultSet;
        }
        public ResultSet<List<LogProductViewModel>> ViewLog(int id)
        {
            var resultSet = new ResultSet<List<LogProductViewModel>>();
            var lst = new List<LogProductViewModel>();
            try
            {
                using (var data = new CatalogDataEntities())
                {
                    data.LogProducts.Where(x => x.Product == id).ToList().ForEach((LogProduct product) =>
                    {

                        string[] oldValues = product.OldValues?.Split('|');
                        var idEstatusOld = Convert.ToInt32(oldValues?[4] ?? "0");
                        var oldEstatus = data.Estatus.FirstOrDefault(X => X.Id == idEstatusOld);

                        string[] newValues = product.NewValues?.Split('|');
                        var idEstatusNew = Convert.ToInt32(newValues?[4] ?? "0");
                        var newEstatus = data.Estatus.FirstOrDefault(X => X.Id == idEstatusNew);
                        lst.Add(new LogProductViewModel
                        {
                            Type = product.Type,
                            AffectedDate = product.DatetimeAffect,
                            AffectedPerson = product.Person1.Name + " " + product.Person1.LastName,
                            OldCode = oldValues?[0],
                            OldName = oldValues?[1],
                            OldPrice = "$ " + oldValues?[2],
                            OldType = oldValues?[3],
                            OldEstatus = new EstatusProduct
                            {
                                Id = oldEstatus?.Id ?? 0,
                                Description = oldEstatus?.Description
                            },
                            NewCode = newValues?[0],
                            NewName = newValues?[1],
                            NewPrice = "$ " + newValues?[2],
                            NewType = newValues?[3],
                            NewEstatus = new EstatusProduct
                            {
                                Id = newEstatus?.Id ?? 0,
                                Description = newEstatus?.Description
                            }
                        });
                    });
                    resultSet.ObjectResult = lst;
                    resultSet.Success = true;
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
