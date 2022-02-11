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
                    if (viewModel.Estatus.Id > 0)
                    {
                        predicate = predicate.Where(x => x.Estatus == viewModel.Estatus.Id);
                    }
                    if (predicate.Count() > 0)
                    {
                        throw new Exception("Sin Registros");
                    }
                    resultSet.ObjectResult = predicate.Select(x => new ProductViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Estatus = new Estatu {Id =  x.Estatus },
                        Name = x.Name,
                        Price = x.Price,
                        RegistrationDate = x.RegistrationDate,
                        RegistrationPerson = x.Person,
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
                        var add = data.Products.Add(new Product
                        {
                            Code = model.Code,
                            Name = model.Name,
                            Price = model.Price,
                            RegistrationDate = Convert.ToDateTime(model.RegistrationDate),
                            Estatus = model.Estatus.Id,
                            RegistrationPerson = Convert.ToInt32(model.RegistrationPerson),
                            Type = model.Type

                        });
                        resultSet.ObjectResult = add.Id > 0;
                    }
                    else
                    {
                        product.Code = model.Code;
                        product.Name = model.Name;
                        product.Price = model.Price;
                        product.RegistrationDate = Convert.ToDateTime(model.RegistrationDate);
                        product.Estatus = model.Estatus.Id;
                        product.RegistrationPerson = Convert.ToInt32(model.RegistrationPerson);
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
        public ResultSet<bool> Delete(int id)
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
                    product.Estatus = 2;
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
    }
}
